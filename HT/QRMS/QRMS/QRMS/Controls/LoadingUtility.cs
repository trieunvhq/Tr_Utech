using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public class LoadingUtility
    {
        private static LoadingPopupPage loadingPage = new LoadingPopupPage();
        private static bool IsLoading = false;

        public LoadingUtility()
        {
        }

        private static bool IfExist()
        {
            var data = PopupNavigation.Instance.PopupStack.FirstOrDefault(a => a.Equals(loadingPage));
            return data != null;
        }

        public static async void Show()
        {
            if (IfExist()) return;
            await PopupNavigation.Instance.PushAsync(loadingPage).ContinueWith(up=>IsLoading = true);
        }

        public async static void Hide()
        {
            if (!IfExist()) return;
            await PopupNavigation.Instance.RemovePageAsync(loadingPage).ConfigureAwait(false);
            IsLoading = false;
        }
        public static Task HideAsync()
        {
            try
            {
                if (!IfExist()) return Task.Run(() => { });
                return PopupNavigation.Instance.RemovePageAsync(loadingPage).ContinueWith(done => IsLoading = false);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Task ShowAsync()
        {
            if (IfExist()) return Task.Run(()=>{ });
            return PopupNavigation.Instance.PushAsync(loadingPage).ContinueWith(up => IsLoading = true);
        }
    }
}
