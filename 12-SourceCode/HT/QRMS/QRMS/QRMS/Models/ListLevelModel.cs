using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace QRMS.Models
{
    public class ListLevelModel : INotifyPropertyChanged
    {
        private string _rdoLevel;
        public string rdoLevel
        {
            get => _rdoLevel;
            set
            {
                _rdoLevel = value;
                OnPropertyChanged();
            }
        }

        private int _ID;
        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
                OnPropertyChanged();
            }
        }

        private decimal? _INS_MONEY_PERSON;
        public decimal? INS_MONEY_PERSON
        {
            get => _INS_MONEY_PERSON;
            set
            {
                _INS_MONEY_PERSON = value;
                OnPropertyChanged();
            }
        }

        private decimal? _INS_MONEY_ASSET;
        public decimal? INS_MONEY_ASSET
        {
            get => _INS_MONEY_ASSET;
            set
            {
                _INS_MONEY_ASSET = value;
                OnPropertyChanged();
            }
        }

        private decimal? _INS_MONEY_MAX;
        public decimal? INS_MONEY_MAX
        {
            get => _INS_MONEY_MAX;
            set
            {
                _INS_MONEY_MAX = value;
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
