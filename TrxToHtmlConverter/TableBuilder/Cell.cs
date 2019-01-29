using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class Cell : ICell
    {
        private string id;
        private string styleClass;
        private string content;
        private HtmlNode cellNode;
        private bool isTh;

        string ICell.Id { get { return id; } set { id = value; } }
        string ICell.StyleClass { get { return styleClass; } set { styleClass = value; } }
        HtmlNode ICell.CellNode { get { return cellNode; } set { cellNode = value; } }

        //templates of cells, not implemented yet
        public static Cell failedTest;
        public static Cell passedTest;
        public static Cell inconclusiveTest;
        public static Cell WarningTest;
        public static Cell button;

        public Cell(string styleClass, string id, bool isTh, string content)
        {
            this.content = ToUpperFirstLetter(content);
            this.styleClass = styleClass;
            this.isTh = isTh;
            this.id = id;
            cellNode = CreateCellNode();
        }
        public Cell(string content)
        {
            this.content = ToUpperFirstLetter(content);
            cellNode = CreateCellNode();
        }
        private HtmlNode CreateCellNode()
        {
            if (isTh)
                return HtmlNode.CreateNode($"<th id=\"{id}\" class=\"{styleClass}\">{content}</th>");
            return HtmlNode.CreateNode($"<td id=\"{id}\" class=\"{styleClass}\">{content}</td>");
        }
        public string ToUpperFirstLetter(string word)
        {
            if (word != null) { word = word[0].ToString().ToUpper() + word.Substring(1); }
            else { }
            return word;
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
