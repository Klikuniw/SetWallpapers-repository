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

        public Resolution ReadSelectedResolution(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode_polygon in xDoc.DocumentElement)
            {
                if (xmlNode_polygon.Name == "selectedResolution")
                {
                    return new Resolution() { Value = xmlNode_polygon.Attributes["value"].Value };
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
                        categories.Add(new Category()
                        {
                            Name = xmlElement_point.Attributes["name"].Value,
                            Tag = xmlElement_point.Attributes["tag"].Value,
                            Checked = Convert.ToBoolean(xmlElement_point.Attributes["checked"].Value)
                        });
                    }

                }
            }

            return categories;
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
                        resolutions.Add(new Resolution()
                        {
                            Value = xmlElement_point.Attributes["value"].Value
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
        public void SaveChanges(string path, List<Category> categories, Resolution resolution, string interval)
        {
            int i = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "categories")
                {
                    foreach (XmlElement xmlElement in xmlNode.ChildNodes)
                    {
                        xmlElement.Attributes["checked"].Value = categories[i].Checked.ToString();
                        i++;
                    }

                }
                if (xmlNode.Name == "interval")
                {
                    xmlNode.Attributes["value"].Value = interval;
                }
                if (xmlNode.Name == "selectedResolution")
                {
                    xmlNode.Attributes["value"].Value = resolution.Value;
                }
            }
            xDoc.Save(path);
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

    }
}
