using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace QRMS.Models
{
    public class ListAccountModel : INotifyPropertyChanged
    {
        string _iItemAccount;
        public string ItemAccount
        {
            get => _iItemAccount;
            set
            {
                _iItemAccount = value;
                OnPropertyChanged();
            }
        }
        private string _lblAccount;
        public string lblAccount
        {
            get => _lblAccount;
            set
            {
                _lblAccount = value;
                OnPropertyChanged();
            }
        }

        private string _lblName;
        public string lblName
        {
            get => _lblName;
            set
            {
                _lblName = value;
                OnPropertyChanged();
            }
        }

        private string _lblChangePassword;
        public string lblChangePassword
        {
            get => _lblChangePassword;
            set
            {
                _lblChangePassword = value;
                OnPropertyChanged();
            }
        }

        private string _lblSetting;
        public string lblSetting
        {
            get => _lblSetting;
            set
            {
                _lblSetting = value;
                OnPropertyChanged();
            }
        }

        private string _lblLanguage;
        public string lblLanguage
        {
            get => _lblLanguage;
            set
            {
                _lblLanguage = value;
                OnPropertyChanged();
            }
        }

        private string _lblLogout;
        public string lblLogout
        {
            get => _lblLogout;
            set
            {
                _lblLogout = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
