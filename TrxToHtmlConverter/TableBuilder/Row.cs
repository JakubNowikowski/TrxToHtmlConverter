using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class Row : ICell
    {
        private string id;
        private string styleClass;
        HtmlNode cellNode;

        string ICell.Id { get { return id; } set { id = value; } }
        string ICell.StyleClass { get { return styleClass; } set { styleClass = value; } }
        HtmlNode ICell.CellNode { get { return cellNode; } set { cellNode = value; } }

        public Row(string styleClass, string id)
        {
            this.styleClass = styleClass;
            this.id = id;
            cellNode = CreateCellNode();
        }
        private HtmlNode CreateCellNode()
        {
            return HtmlNode.CreateNode($"<tr id=\"{id}\" class=\"{styleClass}\"></tr>");
        }
        public void Add(ICell cell)
        {
            cellNode.AppendChild(cell.CellNode);
        }

        public void Add(ICell[] cells)
        {
            foreach (ICell cell in cells)
                cellNode.AppendChild(cell.CellNode);
        }
    }
}
