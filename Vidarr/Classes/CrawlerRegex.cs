using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;

namespace Vidarr.Classes
{
    class CrawlerRegex
    {
        //Get results from HTTPResponsebody
        //Body doesn't need a regex because it's already being done by content and URL.
        static public string regexContent(string response)
        {
            //Get body from string
            string body = "";
            string patternBody = "id=[\"']content[\"'](.*?)footer";

            Match match = Regex.Match(response, patternBody, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (match.Success)
            {
                body = match.Value;
            }
            else
            {
                Debug.WriteLine("Geen content kunnen vinden.");
            }
            return body;
        }

        //Get results from HTTPResponsebody
        static public string regexResults(string response)
        {
            //Get body from string
            string body = "";
            string patternBody = "id=[\"']results[\"'](.*?)class=[\"']branded-page-box\\s*";

            Match match = Regex.Match(response, patternBody, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (match.Success)
            {
                body = match.Value;
            }
            else
            {
                Debug.WriteLine("Geen results kunnen vinden.");
            }
            return body;
        }

        //Fetch URL from content
        static public List<string> regexUrls(string response)
        {
            int maxUrls = 50;
            int gevondenUrls = 0;
            Object thisLocker = new object();

            //Fetch URL from body
            List<string> urls = new List<string>();
            string vorigGevondenUrl = "";
            string url = "";
            string patternUrls = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))"; //beste regex

            //Fetch URL
            MatchCollection collection = Regex.Matches(response, patternUrls);
            foreach (Match m in collection)
            {
                //Check for max 10 urls found
                if (gevondenUrls < maxUrls)
                {

                    //Check for valid URL
                    if (m.Success)
                    {
                        url = m.Value;

                        //Remove href=" from string
                        url = url.Remove(0, 6);
                        url = url.Remove(url.Length - 1);
                        if (url.Contains("/watch?"))
                        {
                            url = "https://www.youtube.com" + url;
                        }
                        if (url.Contains("www.youtube.com") && !url.Contains("channel") && !url.Contains("user") && !url.Contains("playlist") && !url.Contains("feed") && !url.Contains("bit.ly") && !url.Contains("accounts.google") && !url.Contains("android-app"))
                        {
                            bool isUri = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
                            //Check correct URL, not 10 urls added, last added url wasn't the same.
                            if (isUri && (gevondenUrls < maxUrls) && !vorigGevondenUrl.Equals(url))
                            {
                                lock (thisLocker)
                                {
                                    urls.Add(url);

                                    //Prevent double URLs
                                    vorigGevondenUrl = url;
                                }
                                gevondenUrls++;
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return urls;
        }

        //Get keywords from content
        static public void regexKeywords(string response)
        {
            //Fetch keywords from body
            string keywords = "";
            string pattern = "(<meta itemprop=\"|<link itemprop=\"thumbnailUrl\")(.*?)\">";

            string videoId = "(<meta itemprop=\"videoId\" content=\")(.*?)\">";
            string title = "(<meta itemprop=\"name\" content=\")(.*?)\">";
            string description = "(<meta itemprop=\"description\" content=\")(.*?)\">";
            string genre = "(<meta itemprop=\"genre\" content=\")(.*?)\">";
            string thumbnail = "(<link itemprop=\"thumbnailUrl\" href=\")(.*?)\">";

            MatchCollection collection;
            MatchCollection collectionUrl;
            MatchCollection collectionTitle;
            MatchCollection collectionDescription;
            MatchCollection collectionGenre;
            MatchCollection collectionThumbnail;

            try
            {
                collection = Regex.Matches(response, pattern);
                string gevondenUrl = "https://www.youtube.com/watch?v=";
                string gevondenTitle = "";
                string gevondenDescription = "";
                string gevondenGenre = "";
                string gevondenThumbnail = "";
                foreach (Match m in collection)
                {
                    //Check matched resulsts
                    keywords = m.Value;

                    collectionUrl = Regex.Matches(m.Value, videoId);
                    foreach (Match m2 in collectionUrl)
                    {
                        gevondenUrl += m2.Groups[2].Value;
                    }
                    collectionTitle = Regex.Matches(m.Value, title);
                    foreach (Match m2 in collectionTitle)
                    {
                        gevondenTitle = m2.Groups[2].Value;
                    }
                    collectionDescription = Regex.Matches(m.Value, description);
                    foreach (Match m2 in collectionDescription)
                    {
                        gevondenDescription = m2.Groups[2].Value;
                    }
                    collectionGenre = Regex.Matches(m.Value, genre);
                    foreach (Match m2 in collectionGenre)
                    {
                        gevondenGenre = m2.Groups[2].Value;                        
                    }
                    collectionThumbnail = Regex.Matches(m.Value, thumbnail);
                    foreach (Match m2 in collectionThumbnail)
                    {
                        gevondenThumbnail = m2.Groups[2].Value;
                    }

                }

                //keywords in database

                dbConn dbConnection = new dbConn();
                dbConnection.insertQuery("INSERT INTO video(Url, Title, Description, Genre, Thumbnail) VALUES('"+ gevondenUrl + "', '" + gevondenTitle + "', '" + gevondenDescription + "', '" + gevondenGenre + "', '" + gevondenThumbnail + "')");
                dbConnection.dbClose();
                //MySqlConnection conn;
                //string myConnectionString;

                //myConnectionString = "Server=127.0.0.1;Database=vidarr;Uid=root;Pwd='';SslMode=None;charset=utf8";

                //try
                //{
                //    conn = new MySqlConnection(myConnectionString);
                //    MySqlCommand cmd = new MySqlCommand();

                //    cmd.CommandText = "INSERT INTO video(Url,Title,Description,Genre,Thumbnail) VALUES('"+ gevondenUrl + "','" + gevondenTitle + "','" + gevondenDescription + "','" + gevondenGenre + "','" + gevondenThumbnail + "')";
                //    conn.Open();
                //    cmd.CommandType = CommandType.Text;
                //    cmd.Connection = conn;
                //    cmd.ExecuteNonQuery();
                //    conn.Close();
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(ex.Message);

                //}
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("getKeywords() geeft NullReferenceException: " + e.Message);
            }
        }
    }
}
