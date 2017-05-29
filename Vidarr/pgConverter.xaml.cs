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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                //conv.PickMedia();
            }
            else
            {
                var dialog = new MessageDialog("Oeps er ging iets mis, heeft u een bestand geselecteerd? Heeft u geselecteerd waar het bestand naar geconverteerd moet worden?");
                dialog.ShowAsync();
            }
        }

        private async void selectFile(object sender, RoutedEventArgs e)
        {
            //Maak een nieuwe FileOpenPicker aan
            FileOpenPicker openPicker = new FileOpenPicker();

            //Kies welke weergave het moet hebben
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            //STandaard openings plek
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;

            //Bestands extensies die toegelaten worden
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".wma");
            openPicker.FileTypeFilter.Add(".mov");
            openPicker.FileTypeFilter.Add(".flv");
            openPicker.FileTypeFilter.Add(".avi");

            //Hier geven we de type selectie weer, single of multiple
            //StorageFile file = await openPicker.PickSingleFileAsync();
            StorageFile file = await openPicker.PickSingleFileAsync();
            //MULTIPLE SELECTIE::: StorageFile file = await openPicker.PickMultipleFilesAsync();

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
                List<Converted> bestandenLijstDownloadsVidarrMap = new List<Converted>();
                StorageFolder picturesFolder = KnownFolders.VideosLibrary;
                IReadOnlyList<IStorageItem> itemsList = await picturesFolder.GetItemsAsync();
                foreach (var item in itemsList)
                {
                    if (item is StorageFolder)
                    {
                        //outputText.Append(item.Name + " folder\n");

                    }
                    else
                    {
                        //outputText.Append(item.Name + "\n");
                        string name = item.Name;
                        var iSize = await item.GetBasicPropertiesAsync();
                        string size = iSize.Size.ToString();
                        string ext = name.Substring(name.Length -4);
                        bestandenLijstDownloadsVidarrMap.Add(new Converted{ titel = name, grootte = size, extensie = ext });
                    }
                }

                //listOfStudents.Add("John");
                ListConvertedFiles.ItemsSource = bestandenLijstDownloadsVidarrMap;
                await Task.Delay(10000);
            }
        }

        private void goToDownloaderScreen(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pgDownload));
        }

        private void SelectFileOne_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
