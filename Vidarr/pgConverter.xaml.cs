using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Vidarr.Classes;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Vidarr
{

    public sealed partial class pgConverter : Page
    {
        public StorageFile file1;
        public StorageFile file2;
        public StorageFile file3;

        public pgConverter()
        {
            this.InitializeComponent();
            file1 = null;
            file2 = null;
            file3 = null;
            getVideos();
        }

        private void convertSelectedFiles(object sender, RoutedEventArgs e)
        {
            Convert conv = new Convert();
            string selectionBox;
            if (ComboBox.SelectedIndex > -1 && file1 != null || file2 != null || file3 != null)
            {
                selectionBox = ComboBox.SelectionBoxItem.ToString();
                List<StorageFile> files = new List<StorageFile>();
                if (file1 != null)
                {
                    files.Add(file1);
                }
                if (file2 != null)
                {
                    files.Add(file2);
                }
                if (file3 != null)
                {
                    files.Add(file3);
                }
                conv.ConvertChosenMedia(files, selectionBox);
            }
            else
            {
                var dialog = new MessageDialog("Oeps er ging iets mis, heeft u een bestand geselecteerd? Heeft u geselecteerd waar het bestand naar geconverteerd moet worden?");
                dialog.ShowAsync();
            }
        }

        private async void selectFile(object sender, RoutedEventArgs e)
        {
            //Create new FileOpenPicker
            FileOpenPicker openPicker = new FileOpenPicker();

            //Choose Picker view
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            //Default open
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;

            //Accepted file extensions
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".wma");
            openPicker.FileTypeFilter.Add(".mov");
            openPicker.FileTypeFilter.Add(".flv");
            openPicker.FileTypeFilter.Add(".avi");

            //Show type selection, single or multiple
            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                int filenumber = int.Parse((string)((Button)sender).Tag);
                if (filenumber.Equals(1))
                {
                    file1 = file;
                    NameFirstChosenFile.Text = "Gekozen bestand: " + file1.Name;
                }
                else if (filenumber.Equals(2))
                {
                    file2 = file;
                    NameSecondChosenFile.Text = "Gekozen bestand: " + file2.Name;
                }
                else
                {
                    file3 = file;
                    NameThirdChosenFile.Text = "Gekozen bestand: " + file3.Name;
                }
            } 
        }

        public async void getVideos()
        {
            while (true)
            {
                List<Converted> downloadedFileList = new List<Converted>();
                StorageFolder picturesFolder = KnownFolders.VideosLibrary;
                IReadOnlyList<IStorageItem> itemsList = await picturesFolder.GetItemsAsync();
                foreach (var item in itemsList)
                {
                    if (!(item is StorageFolder))
                    {
                        string name = item.Name;
                        var iSize = await item.GetBasicPropertiesAsync();
                        string size = iSize.Size.ToString();
                        string ext = name.Substring(name.Length - 4);
                        downloadedFileList.Add(new Converted {
                            fileTitle = name.Remove(item.Name.Length - 4),
                            fileSize = size,
                            fileExtension = ext.ToLower()
                        });
                    }
                }
                ListConvertedFiles.ItemsSource = downloadedFileList;
                await Task.Delay(10000);
            }
        }

        private void goToDownloaderScreen(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pgDownload));
        }

    }
}
