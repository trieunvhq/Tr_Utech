using System;
using QRMS.Constants;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public class EntryLengthValidatorBehavior : Behavior<Entry>
    {  
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (e.OldTextValue != null)
            {
                for (int i = 0; i < MySettings._lst.Count; ++i)
                {
                    if (e.OldTextValue.Contains(MySettings._lst[i]))
                    {
                        return;
                    }
                }
            }

            for (int i = 0; i < MySettings._lst.Count; ++i)
            {
                if (e.NewTextValue.Contains(MySettings._lst[i]))
                {
                    if (e.OldTextValue == null)
                        entry.Text = "";
                    else
                        entry.Text = e.OldTextValue;

                    break;
                }
            }
            //// if Entry text is longer then valid length
            //if (entry.Text.Length > this.MaxLength)
            //{
            //    string entryText = entry.Text;

            //    entryText = entryText.Remove(entryText.Length - 1); // remove last char

            //    entry.Text = entryText;
            //}
        }
    }
}
