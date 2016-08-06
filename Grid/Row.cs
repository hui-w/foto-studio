using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QLike.Foto.Grid
{
    public class Row
    {
        /// <summary>
        /// Percentage value
        /// </summary>
        public int Percentage
        {
            get;
            set;
        }

        /// <summary>
        /// Top position
        /// </summary>
        public int Top
        {
            get;
            set;
        }

        /// <summary>
        /// Pixel Value
        /// </summary>
        public int Height
        {
            get;
            set;
        }

        public Row(XElement node)
        {
            this.Percentage = Common.GetIntFromPercentage(node.Attribute("Height").Value);
        }
    }//end of class
}
