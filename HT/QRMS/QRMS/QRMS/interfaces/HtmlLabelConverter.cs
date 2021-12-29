using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
namespace QRMS.interfaces
{
    public class HtmlLabelConverter
    {
        public HtmlLabelConverter()
        {
        }
        public FormattedString Convert(string value)
        {
            var formatted = new FormattedString();

            foreach (var item in ProcessString(value))
            {
                if (item.Type == "b")
                {
                    formatted.Spans.Add(CreateBoldSpan(item));
                }
                else
                if (item.Type == "i")
                {
                    formatted.Spans.Add(CreateItalicSpan(item));
                }
                else
                {
                    formatted.Spans.Add(CreateAnchorSpan(item));
                }
            }

            return formatted;
        }

        private Span CreateAnchorSpan(StringSection section)
        {
            var span = new Span()
            {
                Text = section.Text,
                FontSize = 14,
                TextColor = Color.FromHex("#6C757D")
            };

            if (!string.IsNullOrEmpty(section.Link))
            {
                span.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = _navigationCommand,
                    CommandParameter = section.Link
                });
                span.TextColor = Color.Blue;
                span.TextDecorations = TextDecorations.Underline;
                // Underline coming soon from https://github.com/xamarin/Xamarin.Forms/pull/2221
                // Currently available in Nightly builds if you wanted to try, it does work :)
                // As of 2018-07-22. But not avail in 3.2.0-pre1.
                // span.TextDecorations = TextDecorations.Underline;
            }

            return span;
        }

        private Span CreateBoldSpan(StringSection section)
        {
            var span = new Span()
            {
                Text = section.Text,
                FontAttributes = FontAttributes.Bold,
                FontSize = 14,
                TextColor = Color.FromHex("#6C757D")
            };

            return span;
        }

        private Span CreateItalicSpan(StringSection section)
        {
            var span = new Span()
            {
                Text = section.Text,
                FontAttributes = FontAttributes.Italic,
                FontSize = 14,
                TextColor = Color.FromHex("#6C757D")
            };

            return span;
        }

        public IList<StringSection> ProcessString(string rawText)
        {
            rawText = rawText.Replace("<br>", "\n");
            rawText = rawText.Replace("<br/>", "\n");
            rawText = rawText.Replace("<br />", "\n");
            rawText = rawText.Replace("<p>", "\n");
            rawText = rawText.Replace("</p>", "\n");
            const string spanPattern = @"(<[abi].*?>.*?</[abi]>)";

            MatchCollection collection = Regex.Matches(rawText, spanPattern, RegexOptions.Singleline);

            var sections = new List<StringSection>();

            var lastIndex = 0;

            foreach (Match item in collection)
            {
                var foundText = item.Value;
                sections.Add(new StringSection() { Text = rawText.Substring(lastIndex, item.Index - lastIndex) });
                lastIndex = item.Index + item.Length;

                var html = new StringSection()
                {
                    Link = Regex.Match(item.Value, "(?<=href=\\\")[\\S]+(?=\\\")").Value,
                    Text = Regex.Replace(item.Value, "<.*?>", string.Empty),
                    Type = item.Value.Substring(1, 1)
                };

                sections.Add(html);
            }

            sections.Add(new StringSection() { Text = rawText.Substring(lastIndex) });

            return sections;
        }

        public class StringSection
        {
            public string Text { get; set; }
            public string Link { get; set; }
            public string Type { get; set; }
        }

        private ICommand _navigationCommand = new Command<string>((url) =>
        {
            Device.OpenUri(new Uri(url));
        });
    }
}
