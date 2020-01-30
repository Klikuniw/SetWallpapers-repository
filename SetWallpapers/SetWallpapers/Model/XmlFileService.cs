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
            foreach (XmlNode xmlNodePolygon in xDoc.DocumentElement)
            {
                if (xmlNodePolygon.Name == "categories")
                {
                    foreach (XmlElement xmlNode in xmlNodePolygon.ChildNodes)
                    {
                        categories.Add(new Category()
                        {
                            Name = xmlNode.Attributes["name"].Value,
                            Tag = xmlNode.Attributes["tag"].Value,
                            Checked = Convert.ToBoolean(xmlNode.Attributes["checked"].Value)
                        });
                    }

                }
            }

            return categories;
        }

        public DateTime ReadClosingTime(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            DateTime time = new DateTime();

            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "closingTime")
                {
                    time = Convert.ToDateTime(xmlNode.Attributes["value"].Value);
                }
            }

            return time;
        }

        public string ReadInterval(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "interval")
                {
                    return xmlNode.Attributes["value"].Value;
                }
            }
            return null;
        }

        public TimeSpan ReadRemainsIntervalTime(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);


            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "interval")
                {
                    return TimeSpan.Parse(xmlNode.Attributes["remains"].Value);
                }
            }
            return new TimeSpan(0, 0, 0);
        }

        public ObservableCollection<Resolution> ReadResolutions(string path)
        {
            ObservableCollection<Resolution> resolutions = new ObservableCollection<Resolution>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "resolutions")
                {
                    foreach (XmlElement xmlElement_point in xmlNode.ChildNodes)
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
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "selectedResolution")
                {
                    return new Resolution() { Value = xmlNode.Attributes["value"].Value };
                }
            }

            return null;
        }

        public bool ReadTimerStarted(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "timerStarted")
                {
                    return Convert.ToBoolean(xmlNode.Attributes["value"].Value);
                }
            }
            return false;
        }

        public void SaveSettingChanges(string path, string interval, DateTime closingTime, Resolution selectedResolution)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "interval")
                {
                    xmlNode.Attributes["value"].Value = interval;
                }
                if (xmlNode.Name == "closingTime")
                {
                    xmlNode.Attributes["value"].Value = closingTime.ToString();
                }
                if (xmlNode.Name == "selectedResolution")
                {
                    xmlNode.Attributes["value"].Value = selectedResolution.Value;
                }
            }
            xDoc.Save(path);
        }

        public void SaveUserInfoChanges(string path, List<Category> categories)
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
            }
            xDoc.Save(path);
        }

        public void WriteClosingTime(string path, DateTime time)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "closingTime")
                {
                    xmlNode.Attributes["value"].Value = time.ToString();
                }
            }
            xDoc.Save(path);
        }

        public void WriteRemainsIntervalTime(string path, TimeSpan time)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "interval")
                {
                    xmlNode.Attributes["remains"].Value = time.ToString();
                }
            }
            xDoc.Save(path);
        }

        public void WriteTimerStarted(string path, bool value)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                if (xmlNode.Name == "timerStarted")
                {
                    xmlNode.Attributes["value"].Value = value.ToString();
                }
            }
            xDoc.Save(path);
        }
    }

}
