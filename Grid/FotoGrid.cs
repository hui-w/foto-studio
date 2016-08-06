using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace QLike.Foto.Grid
{
    public class FotoGrid : IDisposable
    {
        int Width
        {
            get;
            set;
        }

        int Height
        {
            get;
            set;
        }

        public int CellSpacing
        {
            get;
            set;
        }

        List<Row> Rows
        {
            get;
            set;
        }

        List<Column> Columns
        {
            get;
            set;
        }

        public List<Cell> Cells
        {
            get;
            set;
        }

        /// <summary>
        /// Load a grid difinition from the file
        /// </summary>
        /// <param name="definitionFile"></param>
        public FotoGrid(string definitionFile, int width, int height)
        {
            this.Width = width;
            this.Height = height;

            XElement xRoot = XElement.Load(definitionFile);

            XAttribute attrCellSpan = xRoot.Attribute("CellSpacing");
            this.CellSpacing = attrCellSpan == null ? 0 : Convert.ToInt32(attrCellSpan.Value);

            //Get rows difinition
            this.Rows = new List<Row>();
            var rowNodes = from n in xRoot.Element("Rows").Elements("Row") select n;
            foreach (var node in rowNodes)
            {
                this.Rows.Add(new Row(node));
            }

            //Get column difinition
            this.Columns = new List<Column>();
            var columnNodes = from n in xRoot.Element("Columns").Elements("Column") select n;
            foreach (var node in columnNodes)
            {
                this.Columns.Add(new Column(node));
            }

            //Get cell difinition
            this.Cells = new List<Cell>();
            var cellNodes = from n in xRoot.Element("Cells").Elements("Cell") select n;
            foreach (var node in cellNodes)
            {
                this.Cells.Add(new Cell(node));
            }

            //Calculate the objects' positions
            this.CalcPosition();
        }

        /// <summary>
        /// Calculate the objects' positions
        /// </summary>
        private void CalcPosition()
        {
            //check the specified row height and column width
            int specifiedRowPercentage = this.Rows.Sum(r => r.Percentage);
            if (specifiedRowPercentage > 100)
            {
                throw new Exception("Sum of row height can't be grater than 100%");
            }

            int specifiedColumnPercentage = this.Columns.Sum(c => c.Percentage);
            if (specifiedColumnPercentage > 100)
            {
                throw new Exception("Sum of column width can't be grater than 100%");
            }

            //non-specified height rows
            var unsizedRows = from r in this.Rows where r.Percentage == 0 select r;
            int unsizedRowCount = unsizedRows.Count();
            if (unsizedRowCount > 0)
            {
                var averagePercentage = (100 - specifiedRowPercentage) / unsizedRowCount;

                foreach (Row row in unsizedRows)
                {
                    row.Percentage = averagePercentage;
                }
            }

            //non-specified width columns
            var unsizedColumns = this.Columns.Where(c => c.Percentage == 0);
            int unsizedColumnCount = unsizedColumns.Count();
            if (unsizedColumnCount > 0)
            {
                var averagePercentage = (100 - specifiedColumnPercentage) / unsizedColumnCount;

                foreach (Column column in unsizedColumns)
                {
                    column.Percentage = averagePercentage;
                }
            }

            //Check cellspacing
            int usableWidth = this.Width - this.CellSpacing * (this.Columns.Count + 1);
            int usableHeight = this.Height - this.CellSpacing * (this.Rows.Count + 1);
            if (usableWidth <= 0 || usableHeight <= 0)
            {
                throw new Exception("Cellspacing too large");
            }

            //Enrich properties of rows, columns and cells
            int top = this.CellSpacing;
            foreach (Row row in this.Rows)
            {
                row.Top = top;
                row.Height = usableHeight * row.Percentage / 100;
                top += row.Height + this.CellSpacing;
            }

            //Adjust hight of the last row
            int totalHeight = this.Rows.Sum(r => r.Height) + this.CellSpacing * (this.Rows.Count + 1);
            if (totalHeight < this.Height)
            {
                this.Rows[this.Rows.Count - 1].Height += this.Height - totalHeight;
            }

            int left = this.CellSpacing;
            foreach (Column column in this.Columns)
            {
                column.Left = left;
                column.Width = usableWidth * column.Percentage / 100;
                left += column.Width + this.CellSpacing;
            }

            //Adjust width of the last column
            int totalWidth = this.Columns.Sum(c => c.Width) + this.CellSpacing * (this.Columns.Count + 1);
            if (totalWidth < this.Width)
            {
                this.Columns[this.Columns.Count - 1].Width += this.Width - totalWidth;
            }

            //Enrich cell properties
            foreach (Cell cell in this.Cells)
            {
                cell.Left = this.Columns[cell.Column].Left;
                cell.Top = this.Rows[cell.Row].Top;
                cell.Width = 0;
                cell.Height = 0;
                for (int i = 0; i < cell.RowSpan; i++)
                {
                    if (i > 0)
                    {
                        cell.Height += this.CellSpacing;
                    }
                    cell.Height += this.Rows[cell.Row + i].Height;
                }
                for (int j = 0; j < cell.ColumnSpan; j++)
                {
                    if (j > 0)
                    {
                        cell.Width += this.CellSpacing;
                    }
                    cell.Width += this.Columns[cell.Column + j].Width;

                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion
    }//end of class
}
