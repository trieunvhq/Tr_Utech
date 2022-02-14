using QRMS.AppLIB.Common;
using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QRMS.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public bool Loaded_Form { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static event EventHandler<PropertyChangedEventArgs> staticPC
                                                     = delegate { };
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged
        {
            add { staticPC += value; }
            remove { staticPC -= value; }
        }
        protected static void NotifyStaticPropertyChanged(string propertyName)
        {
            if (staticPC != null)
                staticPC(null, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Initialize() { }
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }
        public virtual void AfterLoad()
        {
            Controls.LoadingUtility.HideAsync(); Loaded_Form = true;
        }
    }
    //public class BaseViewModel : Notifiable
    //{
    //    public bool Loaded_Form { get; set; }
    //    public bool IsBusy { get; set; }
    //    public string Title { get; set; }
    //    public string NextPage { get; set; }

    //    #region constructor
    //    public BaseViewModel() { Loaded_Form = false; }
    //    #endregion

    //    public ICommand BackCommand { get; set; } = new Command(() =>
    //    {
    //        // Todo reconsider navigation system??
    //        if (Application.Current.MainPage.Navigation.ModalStack.Count > 0)
    //            Application.Current.MainPage.Navigation.PopModalAsync();
    //        else if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
    //            Application.Current.MainPage.Navigation.PopAsync();
    //    });

    //    public ICommand NextCommand { get; set; }

    //    protected bool SetProperty<T>(ref T backingStore, T value,
    //        [CallerMemberName] string propertyName = "",
    //        Action onChanged = null)
    //    {
    //        if (EqualityComparer<T>.Default.Equals(backingStore, value))
    //            return false;

    //        backingStore = value;
    //        onChanged?.Invoke();
    //        OnPropertyChanged(propertyName);
    //        return true;
    //    }

    //    public bool EmailValidation(string address,out string validText)
    //    {
    //        if (address == null || string.IsNullOrEmpty(address.Trim()))
    //        {
    //            validText = QRMS.Resources.AppResources.CustomerEmailError;
    //            return false;
    //        }
    //        else if (!MobileLib.IsEmail(address))
    //        {
    //            validText = QRMS.Resources.AppResources.EmailFormatError;
    //            return false;
    //        }
    //        else
    //        {
    //            validText = string.Empty;
    //            return true;
    //        }
    //    }
    //    public bool PhoneValidation(string phone, out string validText)
    //    {
    //        if (phone == null || string.IsNullOrEmpty(phone.Trim()))
    //        {
    //            validText = QRMS.Resources.AppResources.CustomerPhoneError;
    //            return false;
    //        }
    //        else if (!MobileLib.IsPhone(phone))
    //        {
    //            validText = QRMS.Resources.AppResources.SDTKhongHopLe;
    //            return false;
    //        }
    //        else
    //        {
    //            validText = string.Empty;
    //            return true;
    //        }
    //    }
    //    public virtual void Initialize() { }
    //    public virtual void OnAppearing() { }
    //    public virtual void OnDisappearing() { }
    //    public virtual void AfterLoad()
    //    {
    //        Controls.LoadingUtility.HideAsync(); Loaded_Form = true;
    //    }
    //}
}
