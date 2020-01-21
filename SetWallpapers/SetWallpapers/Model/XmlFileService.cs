using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SetWallpapers.Model
{
    public class XmlFileService : IFileService
    {
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

        public TimeSpan ReadClosingTime(string path)
        {
            throw new NotImplementedException();
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
                        resolutions.Add(new Resolution()
                        {
                            Value = xmlElement_point.Attributes["value"].Value
                        });
                    }
                }
            }

            return resolutions;
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
    }

}
