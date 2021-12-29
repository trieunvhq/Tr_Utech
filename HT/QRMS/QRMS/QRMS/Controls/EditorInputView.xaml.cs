
using QRMS.Constants;
using QRMS.Helper;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class EditorInputView : StackLayout
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EditorInputView), string.Empty, BindingMode.TwoWay);

        public Color BackgroundEditorColor
        {
            set
            {
                inputEditor.BackgroundColor = value;
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public string Title
        {
            set
            {
                if(string.IsNullOrEmpty(value))
                { lblTitle.Text = ""; }
                else
                {
                    var formattedString = new FormattedString();
                    formattedString.Spans.Add(new Span { Text = value });
                    formattedString.Spans.Add(new Span { Text = " *", TextColor = Color.FromHex("#D31A1F") });
                    lblTitle.FormattedText = formattedString;
                }    
            }
        }

        public string ErrorMessage
        {
            set
            {
                lblError.Text = value;
            }
        }

        public string Placeholder
        {
            set
            {
                inputEditor.Placeholder = value;
            }
        }

        public Keyboard Keyboard
        {
            set
            {
                inputEditor.Keyboard = value;
            }
        }

        public int MaxLength
        {
            set
            {
                inputEditor.MaxLength = value;
            }
        }

        public EventHandler<TextChangedEventArgs> TextChanged
        {
            set
            {
                inputEditor.TextChanged += value;
            }
        }
         

        public bool IsError { get; set; }
        public event EventHandler Focused;
        public event EventHandler Unfocused;

        public EditorInputView()
        {
            InitializeComponent();

            inputEditor.TextColor = Color.Black;
            //inputEditor.TextChanged += (o, e) =>
            //{

            //};
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextProperty.PropertyName)
            {
                inputEditor.Text = Text;
            }
            if (propertyName == HeightRequestProperty.PropertyName)
            {
                frame.HeightRequest = HeightRequest;
            }
            else if (propertyName == nameof(IsError))
            {
                lblError.IsVisible = IsError;
                if (IsError)
                {
                    frame.BorderColor = Color.FromHex("#F5323C");
                    frame.BackgroundColor = Color.FromHex("#FEEEEF");
                }
                else
                {
                    frame.BorderColor = inputEditor.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
                    frame.BackgroundColor = Color.FromHex("#FFFFFF");
                }
            }
            else if (propertyName == nameof(IsEnabled))
            {
                inputEditor.IsEnabled = IsEnabled;
                if (!IsEnabled)
                {
                    frame.BackgroundColor = Color.FromHex("#E2E4E5");
                }
                else
                {
                    frame.BackgroundColor = Color.FromHex("#FFFFFF");
                }    
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                IsError = false;
            } 
        }

        private int tt = 0;
        void OnEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //if (TTSpecicalChar == 0) //if (TTSpecicalChar == -1) //
            //{ inputEditor.Behaviors.Add(new SpecialCharactersValidationBehavior_DC(null, this)); }
            IsError = false;
            Focused?.Invoke(this, EventArgs.Empty);
        }
        
        void OnEntryUnfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            inputEditor.TextChanged -= OnEntryTextChanged;
            frame.BorderColor = Color.FromHex("#DCE0E2");
            Text = inputEditor.Text;
            Text = Text?.Trim();
            Unfocused?.Invoke(this, EventArgs.Empty);
            inputEditor.TextChanged += OnEntryTextChanged;
        }
        private string SpecialCharacters = @"'%*‘;$£&#^@|+=<>!”~}{`]\[.)(’:₫“?_¥€•" + "\"";
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            inputEditor.TextChanged -= OnEntryTextChanged;
            string str = "";
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                if (e.NewTextValue.Length > 0)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                    {
                        if (!StringUtils.IsCheckDiaChi(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
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

                    inputEditor.Text = str;
                    Text = str;

                }

                //bool isValid = e.NewTextValue.ToCharArray().All(x => !SpecialCharacters.Contains(x));
                //str = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }

            inputEditor.TextChanged += OnEntryTextChanged;
            //inputEditor.Text = str;
            //Text = str;
        }
    }
}