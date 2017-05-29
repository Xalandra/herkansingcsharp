using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Vidarr.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Vidarr
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        public LandingPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(640, 480));


            Crawler crawler = new Crawler();
        }

        private void btnDownloader_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pgDownload));
        }

        private void btnConverter_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pgConverter));
        }

        private void buttonHover(object sender, PointerRoutedEventArgs e)
        {
            Grid btn = (Grid)sender;
            btn.Background = new SolidColorBrush(Colors.AntiqueWhite);
        }

        private void buttonUnhover(object sender, PointerRoutedEventArgs e)
        {
            Grid btn = (Grid)sender;
            btn.Background = new SolidColorBrush(Colors.WhiteSmoke);
        }
    }
}
