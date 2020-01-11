using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace SetWallpapers.Model
{
    public class WallpaperCraftParser : IWallpaperParser
    {
        public string WebsiteName => "";

        public List<string> Categories => ReadCategories("");

        public List<string> Resolutions => ReadResolutions("");

        public string ParseImage(string path)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(LoadDocument(path));

            foreach (IElement element in document.QuerySelectorAll("img"))
            {
                string res = element.GetAttribute("src");
                var words = res.Split('.');

                if (words[words.Length - 1] == "jpg")
                {
                    return res;
                }

            }

            return null;
        }

        public List<string> ParseImages(string path)
        {
            List<string> hrefTags = new List<string>();

            var parser = new HtmlParser();
            var document = parser.ParseDocument(LoadDocument(path));

            foreach (IElement element in document.QuerySelectorAll("img"))
            {
                string res = element.GetAttribute("src");
                var words = res.Split('.');

                if (words[words.Length - 1] == "jpg")
                {
                    hrefTags.Add(res);
                }

            }

            return hrefTags;
        }

        public List<string> ReadCategories(string paths)
        {
            throw new NotImplementedException();
        }

        public List<string> ReadResolutions(string paths)
        {
            throw new NotImplementedException();
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
