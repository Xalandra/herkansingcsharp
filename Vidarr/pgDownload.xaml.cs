using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Vidarr.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YoutubeExtractor;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Vidarr
{

    public sealed partial class pgDownload : Page
    {
        ObservableCollection<DownloadViewModel> downloadList = new ObservableCollection<DownloadViewModel>();
        List<string> toDownload = new List<string>();
        Object locker = new object();
        
        public pgDownload()
        {
            this.InitializeComponent();
        }

        private void goToConverterScreen(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pgConverter));
        }

        private void lstDownload_LayoutUpdated(object sender, object e)
        {
            lstDownload.Width = gridRoot.ActualWidth;
        }

        private async void querySubmittedZoek(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Debug.WriteLine("querysubmittedzoek; " + args.QueryText);
            string input = args.QueryText;
            List<SearchResult> listResults = new List<SearchResult>();

            //Search in Database
            bool availableInDb = false;
            Task<bool> searchInDb = Task<bool>.Factory.StartNew(() => 
            {
                List<string> output = new List<string>();
                bool succeeded = false;

                dbConn dbConnection = new dbConn();

                try
                { 
                    MySqlCommand cmd = dbConnection.selectQuery("SELECT * FROM video WHERE title LIKE '%" + input + "%' ORDER BY id DESC LIMIT 0,4");

                    MySqlDataReader reader = cmd.ExecuteReader();

                    string urlx;
                    string titlex;
                    string descriptionx;
                    string genrex;
                    string thumbx;
                    
                    while (reader.Read())
                    {
                        urlx = (string)reader["url"];
                        titlex = (string)reader["title"];
                        descriptionx = (string)reader["description"];
                        genrex = (string)reader["genre"];
                        thumbx = (string)reader["thumbnail"];
                        urlx += ";"+titlex+";"+thumbx;

                        listResults.Add(new SearchResult { url = urlx, titel = titlex, description = descriptionx, genre = genrex, thumb = thumbx });
                        succeeded = true;
                    }

                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                dbConnection.dbClose();
                return succeeded;
            });
            searchInDb.Wait();
            SearchWordResults.ItemsSource = listResults;
            availableInDb = await searchInDb;
            if (!availableInDb)
            {
                //Cannot be found in database.
                await Search.crawlerSearchterm(input);

                ErrorDialog.showMessage("Crawler is aan het zoeken, probeer het zo weer");
            }
        }

        private void downloadSelectedMPFour(object sender, RoutedEventArgs e)
        {
            //Get URL from selected results
            List<string> selected = new List<string>();
            lock (this.locker)
            {
                selected = toDownload;

                //toDownload list emptying
                toDownload = new List<string>();
            }
            foreach (string url in selected)
            {
                string[] dissect = url.Split(';');
                Debug.WriteLine("URL dissect[0]: " + dissect[0]);
                IEnumerable<VideoInfo> videosInfors = DownloadUrlResolver.GetDownloadUrls(dissect[0]);

                VideoInfo video = videosInfors.First(infor => infor.VideoType == VideoType.Mp4);

                downloadList.Add(new DownloadViewModel(video.DownloadUrl, video.Title, dissect[2], video.VideoExtension));
                lstDownload.ItemsSource = downloadList;
            }            
        }

        private void saveCheckedUrl(object sender, RoutedEventArgs e)
        {
            CheckBox selectedBox = sender as CheckBox;
            string url = "";
            try
            {
                url = selectedBox.Content.ToString();
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            if (!url.Equals(""))
            {
                toDownload.Add(url);
            }
        }
    }
}
