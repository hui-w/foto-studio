using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace QLike.AutoUpdate
{
    public class AppFileInfo
    {
        [XmlAttribute("Path")]
        public string Path
        {
            get;
            set;
        }

        [XmlAttribute("Version")]
        public string Version
        {
            get;
            set;
        }

        [XmlAttribute("Size")]
        public long Size
        {
            get;
            set;
        }

        [XmlAttribute("NeedRestart")]
        public bool NeedRestart
        {
            get;
            set;
        }

        public AppFileInfo()
        {
            this.Path = string.Empty;
            this.Version = string.Empty;
            this.Size = 0;
            this.NeedRestart = false;
        }

        /// <summary>
        /// Load from xml node
        /// </summary>
        /// <param name="element"></param>
        public AppFileInfo(XElement element)
            : this()
        {
            XAttribute attPath = element.Attribute("Path");
            this.Path = attPath != null ? Base64.Decode(attPath.Value) : string.Empty;

            XAttribute attVersion = element.Attribute("Version");
            this.Version = attVersion != null ? Base64.Decode(attVersion.Value) : string.Empty;

            XAttribute attSize = element.Attribute("Size");
            this.Size = attSize != null ? Convert.ToInt64(attSize.Value) : 0;

            XAttribute attNeedRestart = element.Attribute("NeedRestart");
            this.NeedRestart = attNeedRestart != null ? Convert.ToBoolean(attNeedRestart.Value) : false;
        }

        /// <summary>
        /// Entity to xml node
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            XElement element = new XElement("FileInfo");

            element.Add(new XAttribute("Path", Base64.Encode(this.Path)));
            element.Add(new XAttribute("Version", Base64.Encode(this.Version)));
            element.Add(new XAttribute("Size", this.Size));
            element.Add(new XAttribute("NeedRestart", this.NeedRestart));

            return element;
        }
    }//end of class
}
