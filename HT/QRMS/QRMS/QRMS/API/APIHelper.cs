using Acr.UserDialogs;
using Flurl.Http.Configuration;
using LIB;
using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.API
{
    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            };
        }
    }
    public class APIHelper
    {
        public static T PostObjectToAPI<T>(string url, string method, object data)
        {
            try
            {
                var _result = APICaller.PostObjectToAPImodelAsync<T>(url, method, data).Result;
                if (_result.RespondCode == 200)
                {
                    return _result.data;
                }
                else if (_result.RespondCode == 600)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerConnectionLost, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerConnectionLost, "OK");
                    return default(T);
                }
                else if (_result.RespondCode == 700)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode < 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode >= 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerAPIError, "OK");
                    return default(T);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<T> PostObjectToAPIAsync<T>(string url, string method, object data)
        {
            try
            {
                var _result = await APICaller.PostObjectToAPImodelAsync<T>(url, method, data).ConfigureAwait(false);
                if (_result.RespondCode == 200)
                {
                    return _result.data;
                }
                else if (_result.RespondCode == 600)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //await UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerConnectionLost, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerConnectionLost, "OK");
                    return default(T);
                }
                else if (_result.RespondCode == 700)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //await UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode < 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //await UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode >= 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //await UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerAPIError, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerAPIError, "OK");
                    return default(T);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public static T GetObjectFromAPI<T>(string url, string path, object paramaters)
        {
            try
            {
                var _result = APICaller.GetObjectFromAPImodelAsync<T>(url, path, paramaters).Result;
                if (_result.RespondCode == 200)
                {
                    return _result.data;
                }
                else if (_result.RespondCode == 600)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerConnectionLost, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerConnectionLost, "OK");
                    return default(T);
                }
                else if (_result.RespondCode == 700)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode < 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode >= 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerAPIError, "OK");
                    return default(T);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.Message);
                throw ex;
            }
        }

        public static async Task<T> GetObjectFromAPIAsync<T>(string url, string path, object paramaters)
        {
            try
            {
                var _result = await APICaller.GetObjectFromAPImodelAsync<T>(url, path, paramaters).ConfigureAwait(false);
                if (_result.RespondCode == 200)
                {
                    return _result.data;
                }
                else if (_result.RespondCode == 600)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //await UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerConnectionLost, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerConnectionLost, "OK");
                    return default(T);
                }
                else if (_result.RespondCode == 700)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    string mess = QRMS.Resources.AppResources.ClientAPIError + Environment.NewLine + _result.Message;
                    //await UserDialogs.Instance.AlertAsync(mess, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", mess, "OK");
                    return default(T);
                }
                else if (_result.RespondCode < 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //await UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode >= 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //await UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerAPIError, "Warning", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerAPIError, "OK");
                    return default(T);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static T PostToAPI<T>(string url, string method)
        {
            try
            {
                var _result = APICaller.PostObjectToAPImodelAsync<T>(url, method,"").Result;
                if (_result.RespondCode == 200)
                {
                    return _result.data;
                }
                else if (_result.RespondCode == 600)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerConnectionLost, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerConnectionLost, "OK");
                    return default(T);
                }
                else if (_result.RespondCode == 700)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode < 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ClientAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ClientAPIError, "OK");
                    return default(T);
                }
                else if (_result.RespondCode >= 500)
                {
                    //DependencyService.Get<ILogger>().Log(_result.Message);
                    //UserDialogs.Instance.AlertAsync(QRMS.Resources.AppResources.ServerAPIError, "Warning", "OK");
                    //Application.Current.MainPage.DisplayAlert("Warning", QRMS.Resources.AppResources.ServerAPIError, "OK");
                    return default(T);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
}
