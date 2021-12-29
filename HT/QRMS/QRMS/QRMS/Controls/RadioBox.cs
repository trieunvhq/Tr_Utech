using System;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public class RadioBox : CheckableImage
    {
        private const string UNCHECKED_IMAGE_SOURCE = "radiobox_unchecked.png";
        private const string CHECKED_IMAGE_SOURCE = "radiobox_checked.png";

        private static event EventHandler<string> _viewTapped;
        private bool _isRendered;

        public static readonly BindableProperty GroupIdProperty = BindableProperty.Create(nameof(GroupId), typeof(string), typeof(RadioBox), string.Empty);

        public string GroupId
        {
            get { return (string)GetValue(GroupIdProperty); }
            set { SetValue(GroupIdProperty, value); }
        }

        public RadioBox() : base(CHECKED_IMAGE_SOURCE, UNCHECKED_IMAGE_SOURCE)
        {
            Size = 18.71;
        }

        protected override void OnTap(CheckableImage checkableImage, object obj)
        {
            _viewTapped?.Invoke(this, GroupId);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if ("Renderer".Equals(propertyName))
            {
                // Renderer property is called in 2 cases:
                // 1. When view is created
                // 2. When view is disposed
                if (_isRendered)
                    _viewTapped -= OnRadioBoxTapped;
                else
                    _viewTapped += OnRadioBoxTapped;
                _isRendered = true;
            }
        }

        private void OnRadioBoxTapped(object sender, string e)
        {
            // Only handle radio box in the same group
            // Set the selected radio box to true and all others to false
            if (GroupId == e)
                IsChecked = sender == this;
        }
    }
}