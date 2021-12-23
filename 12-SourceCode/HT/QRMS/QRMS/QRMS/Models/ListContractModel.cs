using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Models
{
    public class ListContractModel : INotifyPropertyChanged
    {
        private string _Status;
        public string Status
        {
            get => _Status;
            set
            {
                _Status = value;
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

        private string _ContractCode;
        public string ContractCode
        {
            get => _ContractCode;
            set
            {
                _ContractCode = value;
                OnPropertyChanged();
            }
        }

        private string _ExpireDate;
        public string ExpireDate
        {
            get => _ExpireDate;
            set
            {
                _ExpireDate = value;
                OnPropertyChanged();
            }
        }

        private string _CustomerName;
        public string CustomerName
        {
            get => _CustomerName;
            set
            {
                _CustomerName = value;
                OnPropertyChanged();
            }
        }

        private string _IDCard;
        public string IDCard
        {
            get => _IDCard;
            set
            {
                _IDCard = value;
                OnPropertyChanged();
            }
        }

        private string _Phone;
        public string Phone
        {
            get => _Phone;
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }

        private string _ProductType;
        public string ProductType
        {
            get => _ProductType;
            set
            {
                _ProductType = value;
                OnPropertyChanged();
            }
        }

        private string _ContractStatus;
        public string ContractStatus
        {
            get => _ContractStatus;
            set
            {
                _ContractStatus = value;
                OnPropertyChanged();
            }
        }
        
        private string _ContractType;
        public string ContractType
        {
            get => _ContractType;
            set
            {
                _ContractType = value;
                OnPropertyChanged();
            }
        }

        private string _lblInsuranceType;
        public string lblInsuranceType
        {
            get => _lblInsuranceType;
            set
            {
                _lblInsuranceType = value;
                OnPropertyChanged();
            }
        }

        private string _lblInsuranceContract;
        public string lblInsuranceContract
        {
            get => _lblInsuranceContract;
            set
            {
                _lblInsuranceContract = value;
                OnPropertyChanged();
            }
        }

        private string _lblExpireDate;
        public string lblExpireDate
        {
            get => _lblExpireDate;
            set
            {
                _lblExpireDate = value;
                OnPropertyChanged();
            }
        }

        private string _lblCustomer;
        public string lblCustomer
        {
            get => _lblCustomer;
            set
            {
                _lblCustomer = value;
                OnPropertyChanged();
            }
        }

        private string _lblIDCard;
        public string lblIDCard
        {
            get => _lblIDCard;
            set
            {
                _lblIDCard = value;
                OnPropertyChanged();
            }
        }

        private string _lblPhone;
        public string lblPhone
        {
            get => _lblPhone;
            set
            {
                _lblPhone = value;
                OnPropertyChanged();
            }
        }

        private string _lblContractStatus;
        public string lblContractStatus
        {
            get => _lblContractStatus;
            set
            {
                _lblContractStatus = value;
                OnPropertyChanged();
            }
        }

        private string _AlterStatus;
        public string AlterStatus
        {
            get => _AlterStatus;
            set
            {
                _AlterStatus = value;
                OnPropertyChanged();
            }
        }
        
        private string _AlterTime;
        public string AlterTime
        {
            get => _AlterTime;
            set
            {
                _AlterTime = value;
                OnPropertyChanged();
            }
        }

        public int ContractStatusCode { get; set; }

        public int _ContractStatusID { get; set; }
        public string _ContractIssueType { get; set; }

        private string _Code;
        public string Code
        {
            get => _Code;
            set
            {
                _Code = value;
                OnPropertyChanged();
            }
        }

        public int AgentID { get; set; }

        private Color _ContractStatusColor = Color.Default;
        public Color ContractStatusColor
        {
            get => _ContractStatusColor;
            set
            {
                _ContractStatusColor = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
