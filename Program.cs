using System;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace pro2
{
    class Program
    {
        static void Main(string[] args)
        {
             List <string> getImgUrlFromPage(string url) {
                Regex httpRgx = new Regex(@"^http(s)?:");
                List <string> imgUrls = new List<string>();
                HtmlWeb web = new HtmlWeb ();
                var htmlDoc = web.Load(url);
                foreach(HtmlNode img in htmlDoc.DocumentNode.SelectNodes("//img")) {
                    imgUrls.Add(httpRgx.IsMatch(img.GetAttributeValue("src", null)) ? img.GetAttributeValue("src", null) : url + img.GetAttributeValue("src", null));
                };
                return imgUrls;
            };
            
            void DownloadImagesFromPage(string pageUrl, string pathForSave) {
                WebClient webClient = new WebClient();
                List <string> imgUrls = new List<string>();
                imgUrls = getImgUrlFromPage(pageUrl);
                Console.WriteLine("Get images from: " + pageUrl);
                foreach(string imgUrl in imgUrls) {
                    string imageName = imgUrl.Substring(imgUrl.LastIndexOf("/"));
                    webClient.DownloadFile(imgUrl, pathForSave + imageName);
                    Console.WriteLine("Image: " + imageName + ". Save in directory: " + pathForSave + ".");
                };
            };
        }
    }
}
