using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace QLike.AutoUpdate
{
    /// <summary>
    /// Don't change this class
    /// </summary>
    public class ConfigInfo
    {
        public bool Enabled
        {
            get;
            set;
        }

        public string UpdateUrl
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        public List<AppFileInfo> FileList
        {
            get;
            set;
        }

        public ConfigInfo()
        {
            this.Enabled = true;
            this.UpdateUrl = string.Empty;
            this.Version = string.Empty;
            this.FileList = new List<AppFileInfo>();
        }

        /// <summary>
        /// Initialize from XElement
        /// </summary>
        /// <param name="element"></param>
        public ConfigInfo(XElement element)
            : this()
        {
            XElement root = element;// element.Element("ConfigInfo");
            var enabledElement = root.Attribute("Enabled");
            if (enabledElement != null)
            {
                this.Enabled = Convert.ToBoolean(enabledElement.Value);
            }
            var urlElement = root.Attribute("UpdateUrl");
            if (urlElement != null)
            {
                this.UpdateUrl = Base64.Decode(urlElement.Value);
            }
            var versionElement = root.Attribute("Version");
            if (versionElement != null)
            {
                this.Version = Base64.Decode(versionElement.Value);
            }
            foreach (var item in root.Element("FileList").Elements("FileInfo"))
            {
                this.FileList.Add(new AppFileInfo(item));
            }
        }

        public static ConfigInfo LoadClientConfig(string file)
        {
            XElement element = XElement.Load(file);
            return new ConfigInfo(element);

            //ConfigInfo config;
            //XmlSerializer xs = new XmlSerializer(typeof(ConfigInfo));
            //using (StreamReader sr = new StreamReader(file))
            //{
            //    config = xs.Deserialize(sr) as ConfigInfo;
            //    sr.Close();
            //}
            //return config;
        }

        public static ConfigInfo LoadConfigFromXml(string xml)
        {
            XElement element = XElement.Load(new StringReader(xml));
            return new ConfigInfo(element);

            //ConfigInfo config;
            //XmlSerializer xs = new XmlSerializer(typeof(ConfigInfo));
            //using (TextReader reader = new StringReader(xml))
            //{
            //    config = xs.Deserialize(reader) as ConfigInfo;
            //    reader.Close();
            //}
            //return config;
        }

        public void SaveConfigToFile(string file)
        {
            XElement itemsRoot = new XElement("ConfigInfo", 
                new XAttribute("Enabled", this.Enabled),
                new XAttribute("UpdateUrl", Base64.Encode(this.UpdateUrl)),
                new XAttribute("Version", Base64.Encode(this.Version)),
                new XElement("FileList")
                );
            foreach (var appFileInfo in this.FileList)
            {
                itemsRoot.Element("FileList").Add(appFileInfo.ToXElement());
            }

            XDocument xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), itemsRoot);
            xDoc.Save(file);

            //XmlSerializer xs = new XmlSerializer(typeof(ConfigInfo));
            //StreamWriter sw = new StreamWriter(file);
            //xs.Serialize(sw, this);
            //sw.Close();
        }
    }//end of class
}
