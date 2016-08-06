using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QLike.Foto.Grid
{
    public class Column
    {
        /// <summary>
        /// Percentage Value
        /// </summary>
        public int Percentage
        {
            get;
            set;
        }

        /// <summary>
        /// Left position
        /// </summary>
        public int Left
        {
            get;
            set;
        }

        /// <summary>
        /// Pixel Value
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        public Column(XElement node)
        {
            this.Percentage = Common.GetIntFromPercentage(node.Attribute("Width").Value);
        }
    }//end of class
}
