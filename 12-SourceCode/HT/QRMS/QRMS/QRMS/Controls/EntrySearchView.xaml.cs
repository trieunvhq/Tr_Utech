 
using QRMS.Constants;
using QRMS.Helper;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class EntrySearchView : StackLayout
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntrySearchView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty TTSpecicalCharProperty = BindableProperty.Create(nameof(TTSpecicalChar), typeof(int), typeof(EntryInputView), 0, BindingMode.TwoWay);

        public int TTSpecicalChar
        {
            get { return (int)GetValue(TTSpecicalCharProperty); }
            set { SetValue(TTSpecicalCharProperty, value); }
        }
        public Color BackgroundEntryColor
        {
            set
            {
                inputEntry.BackgroundColor = value;
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
        }
      
       
        public string Placeholder
        {
            set
            {
                inputEntry.Placeholder = value;
            }
        }

        public Keyboard Keyboard
        {
            set
            {
                inputEntry.Keyboard = value;
            }
        }

        public int MaxLength
        {
            set
            {
                inputEntry.MaxLength = value;
            }
        }

        public EventHandler<TextChangedEventArgs> TextChanged
        {
            set
            {
                inputEntry.TextChanged += value;
            }
        }
         
        public bool IsForceDot { get; set; }
         
        public event EventHandler Focused;
        public event EventHandler Unfocused;
        public event EventHandler T_TextChanged;

        public EntrySearchView()
        {
            InitializeComponent();
            inputEntry.TextColor = Color.Black;
            //inputEntry.TextChanged += (o, e) =>
            //{
            //    if (IsForceDot)
            //    {
            //        var text = inputEntry.Text.Replace(',', '.');
            //        if (text != inputEntry.Text) inputEntry.Text = text;
            //    }
            //};
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextProperty.PropertyName)
            { 
                inputEntry.Text = Text;
            } 
            else if (propertyName == nameof(IsEnabled))
            {
                inputEntry.IsEnabled = IsEnabled;
            }
            else if (propertyName == TTSpecicalCharProperty.PropertyName)
            {
                inputEntry.TTSpecicalChar = TTSpecicalChar;
            }
        }

        void OnEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        { 
            Focused?.Invoke(this, EventArgs.Empty);
        }

        void OnEntryUnfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Text = Text?.Trim(); 
            Unfocused?.Invoke(this, EventArgs.Empty);
        }
        private int tt = 0;
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string str = inputEntry.Text;
            if (e.NewTextValue != null && e.NewTextValue.Length > 0)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                {
                    if (!StringUtils.IsCheckGCN(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
                    {
                        str = e.NewTextValue;
                    }
                    else
                    {
                        str = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                    }
                }
                else
                { str = e.NewTextValue; }

            }
            Text = str;
            inputEntry.Text = str; 
            T_TextChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}