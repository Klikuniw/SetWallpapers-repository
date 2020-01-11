using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AngleSharp.Dom;

namespace SetWallpapers.Model
{
    public class WallpaperCraftParser : IWallpaperParser
    {
        public string WebsiteName => "https://wallpaperscraft.com";

        public List<string> Categories => ReadCategories("wallpaperscraftInfo.xaml");

        public List<string> Resolutions => ReadResolutions("wallpaperscraftInfo.xaml");

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

        public List<string> ReadCategories(string path)
        {
            List<string> categories = new List<string>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode_polygon in xDoc.DocumentElement)
            {
                if (xmlNode_polygon.Name == "categories")
                {
                    foreach (XmlElement xmlElement_point in xmlNode_polygon.ChildNodes)
                    {
                        categories.Add(xmlElement_point.Attributes["Name"].Value);
                    }
                }
            }

            return categories;
        }

        public List<string> ReadResolutions(string path)
        {
            List<string> resolutions = new List<string>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode_polygon in xDoc.DocumentElement)
            {
                if (xmlNode_polygon.Name == "resolutions")
                {
                    foreach (XmlElement xmlElement_point in xmlNode_polygon.ChildNodes)
                    {
                        resolutions.Add(xmlElement_point.Attributes["value"].Value);
                    }
                }
            }

            return resolutions;
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
