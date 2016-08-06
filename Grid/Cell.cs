using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QLike.Foto.Grid
{
    public class Cell
    {
        public int Row
        {
            get;
            set;
        }

        public int Column
        {
            get;
            set;
        }

        public int RowSpan
        {
            get;
            set;
        }

        public int ColumnSpan
        {
            get;
            set;
        }

        public int Left
        {
            get;
            set;
        }

        public int Top
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public Cell(XElement node)
        {
            this.Row = Convert.ToInt32(node.Attribute("Row").Value);
            this.Column = Convert.ToInt32(node.Attribute("Column").Value);
            int rowSpan = 1;
            int columnSpan = 1;
            if (node.Attribute("RowSpan") != null)
            {
                Int32.TryParse(node.Attribute("RowSpan").Value, out rowSpan);
            }
            if (node.Attribute("ColumnSpan") != null)
            {
                Int32.TryParse(node.Attribute("ColumnSpan").Value, out columnSpan);
            }
            this.RowSpan = rowSpan;
            this.ColumnSpan = columnSpan;
        }
    }//end of class
}
