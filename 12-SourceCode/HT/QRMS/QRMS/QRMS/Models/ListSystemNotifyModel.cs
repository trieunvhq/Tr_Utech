using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Models
{
    public class ListSystemNotifyModel : INotifyPropertyChanged
    {
        private decimal _ID;
        public decimal ID
        {
            get => _ID;
            set
            {
                _ID = value;
                OnPropertyChanged();
            }
        }

        private decimal _NOTIFY_ID;
        public decimal NOTIFY_ID
        {
            get => _NOTIFY_ID;
            set
            {
                _NOTIFY_ID = value;
                OnPropertyChanged();
            }
        }

        private decimal _ACCOUNT_ID;
        public decimal ACCOUNT_ID { get => _ACCOUNT_ID; set { _ACCOUNT_ID = value; OnPropertyChanged(); } }

        private decimal _NOTIFY_TYPE;
        public decimal NOTIFY_TYPE { get => _NOTIFY_TYPE; set { _NOTIFY_TYPE = value; OnPropertyChanged(); } }

        private Nullable<decimal> _NEWS_ID;
        public Nullable<decimal> NEWS_ID { get => _NEWS_ID; set { _NEWS_ID = value; OnPropertyChanged(); } }

        private string _NEWS_CODE;
        public string NEWS_CODE { get => _NEWS_CODE; set { _NEWS_CODE = value; OnPropertyChanged(); } }

        private string _NEWS_NAME;
        public string NEWS_NAME { get => _NEWS_NAME; set { _NEWS_NAME = value; OnPropertyChanged(); } }

        private Nullable<decimal> _CONTRACT_ID;
        public Nullable<decimal> CONTRACT_ID { get => _CONTRACT_ID; set { _CONTRACT_ID = value; OnPropertyChanged(); } }

        private string _CONTRACT_CODE;
        public string CONTRACT_CODE { get => _CONTRACT_CODE; set { _CONTRACT_CODE = value; OnPropertyChanged(); } }

        private string _CONTRACT_TYPE;
        public string CONTRACT_TYPE { get => _CONTRACT_TYPE; set { _CONTRACT_TYPE = value; OnPropertyChanged(); } }

        private string _STATUS_CONTRACT;
        public string STATUS_CONTRACT { get => _STATUS_CONTRACT; set { _STATUS_CONTRACT = value; OnPropertyChanged(); } }

        private string _IMAGE;
        public string IMAGE { get => _IMAGE; set { _IMAGE = value; OnPropertyChanged(); } }

        private string _PUBLIC_DATE;
        public string PUBLIC_DATE { get => _PUBLIC_DATE; set { _PUBLIC_DATE = value; OnPropertyChanged(); } }

        private string _SEND;
        public string SEND { get => _SEND; set { _SEND = value; OnPropertyChanged(); } }

        private string _VIEWED;
        public string VIEWED { get => _VIEWED; set { _VIEWED = value; OnPropertyChanged(); } }

        private string _REMARK;
        public string REMARK { get => _REMARK; set { _REMARK = value; OnPropertyChanged(); } }

        private string _STATUS_RECORD;
        public string STATUS_RECORD { get => _STATUS_RECORD; set { _STATUS_RECORD = value; OnPropertyChanged(); } }

        private Nullable<decimal> _CREATE_USER_ID;
        public Nullable<decimal> CREATE_USER_ID { get => _CREATE_USER_ID; set { _CREATE_USER_ID = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _CREATE_DATE;
        public Nullable<System.DateTime> CREATE_DATE { get => _CREATE_DATE; set { _CREATE_DATE = value; OnPropertyChanged(); } }

        private Nullable<decimal> _UPDATE_USER_ID;
        public Nullable<decimal> UPDATE_USER_ID { get => _UPDATE_USER_ID; set { _UPDATE_USER_ID = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _UPDATE_DATE;
        public Nullable<System.DateTime> UPDATE_DATE { get => _UPDATE_DATE; set { _UPDATE_DATE = value; OnPropertyChanged(); } }

        private string _CONTRACT_ISSUE_TYPE;
        public string CONTRACT_ISSUE_TYPE { get => _CONTRACT_ISSUE_TYPE; set { _CONTRACT_ISSUE_TYPE = value; OnPropertyChanged(); } }

        private string _INSUR_PRODUCT_CODE;
        public string INSUR_PRODUCT_CODE { get => _INSUR_PRODUCT_CODE; set { _INSUR_PRODUCT_CODE = value; OnPropertyChanged(); } }

        private string _NOTIFY;
        public string NOTIFY
        {
            get => _NOTIFY;
            set
            {
                _NOTIFY = value;
                OnPropertyChanged();
            }
        }

        private Color _NOTIFY_COLOR = Color.Default;
        public Color NOTIFY_COLOR
        {
            get => _NOTIFY_COLOR;
            set
            {
                _NOTIFY_COLOR = value;
                OnPropertyChanged();
            }
        }

        private bool _NOTSEEN_ICON;
        public bool NOTSEEN_ICON
        {
            get => _NOTSEEN_ICON;
            set
            {
                _NOTSEEN_ICON = value;
                OnPropertyChanged();
            }
        }

        private bool _WATCHED_ICON;
        public bool WATCHED_ICON
        {
            get => _WATCHED_ICON;
            set
            {
                _WATCHED_ICON = value;
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
