using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class Table : ICell
    {
        private string id;
        private string styleClass;
        private Row tableHeadRow;
        public HtmlNode cellNode;
        string ICell.Id { get { return id; } set { id = value; } }
        string ICell.StyleClass { get { return styleClass; } set { styleClass = value; } }

        private int columnCount;
        private string[] headers;
        private Row[] rows;
        public string Title { get; set; }
        public HtmlNode CreatedTable { get; set; }
        HtmlNode ICell.CellNode { get { return cellNode; } set { cellNode = value; } }

        public Table(string id, string styleClass, string Title)
        {
            this.id = id;
            this.styleClass = styleClass;
            this.Title = Title;
            cellNode = CreateCellNode();
        }

        public Table(string id, string styleClass, Row tableHeadRow)
        {
            this.id = id;
            this.styleClass = styleClass;
            this.tableHeadRow = tableHeadRow;
            cellNode = CreateCellNode();
        }

        private HtmlNode CreateCellNode()
        {
            //create table head and table empty table body
            HtmlNode newTableNode = HtmlNode.CreateNode($"<table id=\"{id}\" class=\"{styleClass}\"></table>");
            if (tableHeadRow == null)
            {
                HtmlNode tableTitleNode = HtmlNode.CreateNode($"<caption>{Title}</caption>");
                newTableNode.AppendChild(tableTitleNode);
            }
            else
            {
                HtmlNode tableHeadNode = HtmlNode.CreateNode($"<thead>{tableHeadRow.cellNode.WriteTo()}</thead>");
                newTableNode.AppendChild(tableHeadNode);
            }
            HtmlNode tableBodyNode = HtmlNode.CreateNode("<tbody></tbody>");
            newTableNode.AppendChild(tableBodyNode);

            return newTableNode;
        }

        public void Add(ICell cell)
        {
            cellNode.LastChild.AppendChild(cell.CellNode);
        }

        public void Add(ICell[] cells)
        {
            foreach (ICell cell in cells)
                cellNode.LastChild.AppendChild(cell.CellNode);
        }
        public static string ToUpperFirstLetter(string word)
        {
            if (word != null) { word = word[0].ToString().ToUpper() + word.Substring(1); }
            else { }
            return word;
        }
    }
}
