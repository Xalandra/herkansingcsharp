using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Web.Http;

namespace Vidarr.Classes
{
    static class Search
    {

        //Search with user input
        static public async Task<string> crawlerSearchterm(string zoekterm)
        {
            HTTPFactory httpClientRequest = new HTTPFactory();
            string httpResponseBody = await httpClientRequest.YoutubeSearchResults(zoekterm);

            //Get results from response
            httpResponseBody = CrawlerRegex.regexResults(httpResponseBody);
            
            await Task.Factory.StartNew(async() =>
            {
                //Extract urls from response
                List<string> urls = CrawlerRegex.regexUrls(httpResponseBody);

                //Iterate on the extracted results
                foreach (String url in urls)
                {
                    //Extract body from URL
                    string body = "";
                    string response = "";

                    //getResponseBody url
                    httpClientRequest = new HTTPFactory();
                    await Task.Delay(1000);

                    //Crawl on URL
                    response = await httpClientRequest.YoutubeCrawlRequest(url);

                    //Extract content from string
                    body = CrawlerRegex.regexContent(response);

                    //Get keywords from body
                    CrawlerRegex.regexKeywords(body);

                } //End code with URLS found
            });

            //return string from httpResponseBody
            return httpResponseBody;
        }
    }
}
