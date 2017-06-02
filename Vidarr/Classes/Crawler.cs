using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System.Threading;
using Windows.UI.Popups;

namespace Vidarr.Classes
{
    class Crawler
    {
        HTTPFactory httpClientRequest;
        List<string> listURL;
        List<string> listResponses;
        List<string> listResponsesKeywords;
        Object locker;
        bool bootingCrawler = false;

        int crawlerAmount;

        static public CancellationTokenSource tokensource;
        static public CancellationToken token;
        static bool finished = false;
        static public void cancelAllTasks() {
            finished = true;
            tokensource.Cancel();
        }

        public Crawler()
        {
            listURL = new List<string>();
            listResponses = new List<string>();
            listResponsesKeywords = new List<string>();
            locker = new Object();

            tokensource = new CancellationTokenSource();
            token = tokensource.Token;

            Task startupCrawling = new Task(() => 
            {
                startCrawling();

                if (token.IsCancellationRequested) {
                    token.ThrowIfCancellationRequested();
                    tokensource.Dispose();
                }
            }, token);
            startupCrawling.Start();
        }

        //startCrawling is a single task to empty DB before the crawler starts.
        public async void startCrawling() {
            //Empty Database.
            emptyDatabase();

            //get body for crawler to work with.
            bootingCrawler = await crawlerStartingPoint();

            //Start from first returned body
            crawlerGO();
        }

        //Function responsible of clearing database of queries before the crawler can start.
        public void emptyDatabase()
        {
            dbConn dbConnection = new dbConn();
            dbConnection.dbTruncate();
            dbConnection.dbClose();
        }

        //This gives the crawler a starting point without user input.
        public async Task<bool> crawlerStartingPoint()
        {
            bool completed = false;
            //Starting point
            try
            {
                //Crawler starting point
                Debug.WriteLine("crawlerStartingPoint gets results");
                httpClientRequest = new HTTPFactory();
                string httpResponseBody = "";
                string url = "https://www.youtube.com/";
                httpResponseBody = await httpClientRequest.YoutubeCrawlRequest(url);

                //Get the body from the HTTP response.
                httpResponseBody = CrawlerRegex.regexContent(httpResponseBody);

                lock (this.locker)
                {
                    listResponses.Add(httpResponseBody);
                    completed = true;
                }
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("crawlerStartingPoint() geeft NullReferenceException: " + e.Message);
            }

            return completed;
        }


        public async void crawlerGO()
        {
            

            crawlerAmount = 0;

            //If the crawler returns TRUE, continue crawling.
            while (!finished)
            {
                //For testing purposees a maximum of 50 results are returned.
                //Only fetch from listResponses if listURL has less than 11 results.
                    if (listURL.Count < 11)
                    {
                        getUrls(fetchFromQueue("responses")); //Fills listURL
                        crawlerAmount++;
                    }

                    //Fetch 10 bodies
                    for (int i = 0; i < 10; i++)
                    {
                        //Fetch first URL and remove body
                        string body = await getResponseBody(fetchFromQueue("urls")); //Fetches keywords and adds it
                        //If body isn't an empty string. Empty string is possible due to wrong URL in HTTPRequest.
                        if (!body.Equals(""))
                        {
                            //Enter body in double Body response.
                            lock (this.locker)
                            {
                                listResponses.Add(body);
                                listResponsesKeywords.Add(body);
                            }
                            crawlerAmount++;
                        }
                    }

                    //Fetch from listResponsesKeywords if there is a body present.
                    while (listResponsesKeywords.Count > 0)
                    {
                        //Fetch keywords from body from listResponseKeywords
                        getKeywords(fetchFromQueue("keywords"));
                    }
                }
        }

        
        public async Task<string> getResponseBody(string url)
        {
            string replyBody = "";

            //getResponseBody url
            httpClientRequest = new HTTPFactory();

            //Which URL to fetch
            Debug.WriteLine("url in getResponseBody() = " + url);

            try
            {
                replyBody = await httpClientRequest.YoutubeCrawlRequest(url); //await = wacht totdat antwoord is
            }
            catch (Exception ex)
            {
                //Give exception if HTTPRequest has bugged.
                Debug.WriteLine("httpError: " + ex.StackTrace);
            }

            return replyBody;
        }

        public void getUrls(string httpResponseBody)
        {
            try
            {
                List<string> urlsFound = CrawlerRegex.regexUrls(httpResponseBody);
                //Add to listURL
                lock (this.locker)
                {
                    foreach (string url in urlsFound)
                    {
                        listURL.Add(url);
                    }
                }
                
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("getUrls() geeft NullReferenceException: " + e.Message);
            }
        }

        public void getKeywords(string httpResponseBody)
        {
            try
            {
                CrawlerRegex.regexKeywords(httpResponseBody);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("getKeywords() geeft NullReferenceException: " + e.Message);
            }
        }

        public string fetchFromQueue(string list)
        {
            string res = "";
            if (list == "responses")
            {
                lock (this.locker)
                {
                    if (listResponses.Count != 0)
                    {
                        string x = listResponses[0];
                        listResponses.Remove(listResponses[0]);
                        res = x;
                    }
                }
            }
            if (list == "urls")
            {
                lock (this.locker)
                {
                    if (listURL.Count != 0)
                    {
                        string x = listURL[0];
                        listURL.Remove(listURL[0]);
                        res = x;
                    }
                    else
                    {
                        return "https://www.youtube.com";
                    }
                }
            }
            if (list == "keywords")
            {
                lock (this.locker)
                {
                    if (listResponsesKeywords.Count != 0)
                    {
                        string x = listResponsesKeywords[0];
                        listResponsesKeywords.Remove(listResponsesKeywords[0]);
                        res = x;
                    }
                }
            }

            return res;
        }
        
    }
}
