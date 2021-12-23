using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.LoginPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomPopup/* : PopupPage*/
    {
        private readonly Action<bool> setResultAction;

        public void CancelAttendanceClicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(false);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }

        public void ConfirmAttendanceClicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(true);
            this.Navigation.PopPopupAsync().ConfigureAwait(false);
        }

        public CustomPopup(Action<bool> setResultAction)
        {
            InitializeComponent();
            this.setResultAction = setResultAction;
        }

        public static async Task<bool> ConfirmConferenceAttendance()
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            void callback(bool didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var page = new CustomPopup(callback);
            await Application.Current.MainPage.Navigation.PushModalAsync(page);

            //var popup = new CustomPopup(callback);

            //await navigation.PushPopupAsync(popup);

            return await completionSource.Task;
        }
    }
}