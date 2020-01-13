using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //public ObservableCollection<Category> Categories => ReadCategories("wallpaperscraftInfo.xml");

        //public ObservableCollection<Resolution> Resolutions => ReadResolutions("wallpaperscraftInfo.xml");

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

        public Resolution ReadSelectedResolution(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode_polygon in xDoc.DocumentElement)
            {
                if (xmlNode_polygon.Name == "selectedResolution")
                {
                    return new Resolution(){Value = xmlNode_polygon.Attributes["value"].Value };
                }
            }

            return null;
        }

        public ObservableCollection<Category> ReadCategories(string path)
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode_polygon in xDoc.DocumentElement)
            {
                if (xmlNode_polygon.Name == "categories")
                {
                    foreach (XmlElement xmlElement_point in xmlNode_polygon.ChildNodes)
                    {
                        categories.Add(new Category(){
                            Name = xmlElement_point.Attributes["name"].Value, 
                            Tag = xmlElement_point.Attributes["tag"].Value,
                            Checked = Convert.ToBoolean(xmlElement_point.Attributes["checked"].Value)
                        });
                    }

                }
            }

            return categories;
        }

        public string ReadInterval(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode_polygon in xDoc.DocumentElement)
            {
                if (xmlNode_polygon.Name == "interval")
                {
                    return xmlNode_polygon.Attributes["value"].Value;
                }
            }

            return null;
        }

        public ObservableCollection<Resolution> ReadResolutions(string path)
        {
            ObservableCollection<Resolution> resolutions = new ObservableCollection<Resolution>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode_polygon in xDoc.DocumentElement)
            {
                if (xmlNode_polygon.Name == "resolutions")
                {
                    foreach (XmlElement xmlElement_point in xmlNode_polygon.ChildNodes)
                    {
                        resolutions.Add(new Resolution(){
                            Value = xmlElement_point.Attributes["value"].Value,
                            Checked = Convert.ToBoolean(xmlElement_point.Attributes["checked"].Value)
                        });
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
