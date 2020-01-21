using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using AngleSharp.Dom;

namespace SetWallpapers.Model
{
    public class WallpaperCraftParser : IWallpaperParser
    {
        public string WebsiteName => "https://wallpaperscraft.com";

        public string ParseImage(Category category, Resolution resolution)
        {
            int page = GetRandPage(0, GetMaxPage(ToLink(WebsiteName, category, resolution)));
            var parser = new HtmlParser();
            var document = parser.ParseDocument(LoadDocument(ToLink(WebsiteName, category, resolution,$"page{page}")));
            List<string> images = new List<string>();

            foreach (IElement element in document.QuerySelectorAll("img"))
            {
                string res = element.GetAttribute("src");
                var words = res.Split('.');

                if (words[words.Length - 1] == "jpg")
                {
                   words = res.Split('_');
                   string image = "";
                   for (int i = 0; i < words.Count()-1; i++)
                   {
                       image += words[i];
                       image += "_";
                   }

                   image += $"{resolution.Value}.jpg";
                   images.Add(image);
                }

            }

            Random a = new Random();
            return images[a.Next(0,images.Count-1)];
        }
        public string ParseImage(List<string> path, Resolution resolution)
        {
            return "";
        }

        public string ToLink(string website, Category category, Resolution resolution,string page="")
        {
            return $"{website}{category.Tag}/{resolution.Value}/{page}/";
        }

        private int GetMaxPage(string path)
        {
            int maxPage = 1;
            var parser = new HtmlParser();
            var document = parser.ParseDocument(LoadDocument(path));

            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                if ("pager__link" == element.GetAttribute("class"))
                {
                    var res = element.GetAttribute("href").Split('/');
                    string num = "";
                    for (int i = 4; i < res[res.Count() - 1].Length; i++)
                    {
                        num += res[res.Count()-1][i];
                    }

                    maxPage = Convert.ToInt32(num);
                }

            }
            return maxPage;
        }
        private int GetRandPage(int from, int to)
        {
            Random r = new Random();
            return r.Next(from, to);
        }
        private string LoadDocument(string path)
        {
            string htmlDoc;

            using (WebClient client = new WebClient())
            {
                htmlDoc = client.DownloadString(path);
            }

            return htmlDoc;
        }

    }
}
