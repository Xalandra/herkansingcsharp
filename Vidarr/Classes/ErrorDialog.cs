using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace Vidarr.Classes
{
    static class ErrorDialog
    {
        public static async void showMessage(string message) {
            // solution from: https://stackoverflow.com/a/39876514/6232638
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async() =>
            {
                var dialog = new MessageDialog(message);
                await dialog.ShowAsync();
            });
        }
    }
}
