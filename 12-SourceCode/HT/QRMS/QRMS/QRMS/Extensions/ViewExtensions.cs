using QRMS.Models;
using Xamarin.Forms;

namespace QRMS.Extensions
{
    public static class ViewExtensions
    {
        /// <summary>
        /// Add content to Grid with 2 columns
        /// </summary>
        /// <param name="row"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static Grid AddContent(this Grid grid, int row, string name, string value)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Label
            {
                Text = name,
                TextColor = (Color)Application.Current.Resources["LabelLightColorSecondary"]
            }, 0, row);
            grid.Children.Add(new Label
            {
                Text = value,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HorizontalTextAlignment = TextAlignment.End
            }, 1, row);
            return grid;
        }

        /// <summary>
        /// Add content to Grid with 2 columns
        /// </summary>
        /// <param name="row"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="converter"></param>
        /// <param name="stringFormat"></param>
        public static Grid AddContent(this Grid grid, int row, string name, string value = null, object context = null, IValueConverter converter = null, string stringFormat = null)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(new Label
            {
                Text = name,
                TextColor = (Color)Application.Current.Resources["LabelLightColorSecondary"]
            }, 0, row);

            if (!string.IsNullOrEmpty(value))
            {
                var label = new Label
                {
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HorizontalTextAlignment = TextAlignment.End
                };
                if (context != null)
                    label.BindingContext = context;
                label.SetBinding(Label.TextProperty, value, BindingMode.Default, converter, stringFormat);
                grid.Children.Add(label, 1, row);
            }

            return grid;
        }

        /// <summary>
        /// Add content to Grid with 2 columns
        /// </summary>
        /// <param name="row"></param>
        /// <param name="data"></param>
        public static Grid AddContent(this Grid grid, int row, CartedContractDKBSModel data, IValueConverter converter = null, string stringFormat = null)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Label
            {
                Text = data.DKBS_NAME,
                TextColor = (Color)Application.Current.Resources["LabelLightColorSecondary"]
            }, 0, row);

            var label = new Label
            {
                BindingContext = data,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HorizontalTextAlignment = TextAlignment.End
            };
            label.SetBinding(Label.TextProperty, nameof(CartedContractDKBSModel.FEE_VALUE), BindingMode.Default, converter, stringFormat);

            grid.Children.Add(label, 1, row);
            return grid;
        }
    }
}