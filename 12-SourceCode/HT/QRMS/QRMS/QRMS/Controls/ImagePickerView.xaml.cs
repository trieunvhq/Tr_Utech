using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRMS.Controls
{
    public partial class ImagePickerView : Grid
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(byte[]), typeof(ImagePickerView), null, BindingMode.TwoWay);

        public byte[] Source
        {
            get { return (byte[])GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public bool IsImageCaptured { get; set; }
        public bool IsShowButton => !IsImageCaptured;

        public string Title
        {
            set
            {
                labelTitle.Text = value;
            }
        }

        public ImagePickerView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            {
                if (propertyName == SourceProperty.PropertyName)
                {
                    if (Source != null)
                    {
                        image.Source = ImageSource.FromStream(() => new MemoryStream(Source));
                        IsImageCaptured = true;
                    }
                    else
                    {
                        image.Source = null;
                        IsImageCaptured = false;
                    }
                }
            }
        }

        private async void OnViewTapped(object sender, EventArgs e)
        {
            if (await GetCameraPermisson())
            {
                await CrossMedia.Current.Initialize();

                var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    MaxWidthHeight = 500,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    CompressionQuality = 50,
                    CustomPhotoSize = 50,
                    Directory = "Anh",
                    Name = "Image.jpg"
                });

                // Image taken
                if (mediaFile != null)
                {
                    // Convert image to stream to upload
                    using (MemoryStream ms = new MemoryStream())
                    {
                        mediaFile.GetStream().CopyTo(ms);
                        Source = ms.ToArray();
                    }
                }
            }
        }

        private async Task<bool> GetCameraPermisson()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }
            return status == PermissionStatus.Granted;
        }

        private async Task<bool> GetPhotoPermisson()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Photos>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Photos>();
            }
            return status == PermissionStatus.Granted;
        }
    }
}