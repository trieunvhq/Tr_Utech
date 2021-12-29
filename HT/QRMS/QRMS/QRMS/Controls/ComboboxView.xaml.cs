 
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class ComboboxView : StackLayout
    {

        public static readonly BindableProperty TextProperty =
             BindableProperty.Create(
                 nameof(Text),
                 typeof(string),
                 typeof(ComboboxView), "");
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value); 
        }
         
        public ComboboxView()
        {
            InitializeComponent();
            inputPicker.TextColor = Color.Black; 
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsEnabled))
            {
                if (IsEnabled)
                {
                    container.BackgroundColor = Color.FromHex("#ffffff");
                }
                else
                {
                    container.BackgroundColor = Color.FromHex("#EDEFF1");
                }
            }
            else if (propertyName == nameof(Text))
            {
                inputPicker.Text = Text;

            }
        }
          
    }
}