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
        private bool isTh;

        public Cell(string styleClass, string id, bool isTh, string content/*, string colSpan = ""*/) //: this(content, colSpan)
        {
            this.styleClass = styleClass;
            this.isTh = isTh;
            this.id = id;
            this.content = ToUpperFirstLetter(content);
            cellNode = CreateCellNode();
        }
        public Cell(string content, string colSpan) //: this(content)
        {
            this.colSpan = colSpan;
            this.content = ToUpperFirstLetter(content);
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
                return HtmlNode.CreateNode($"<th id=\"{id}\" colspan=\"{colSpan}\" class=\"{styleClass}\">{content}</th>");
            return HtmlNode.CreateNode($"<td id=\"{id}\" colspan=\"{colSpan}\" class=\"{styleClass}\">{content}</td>");
        }
    }
}
