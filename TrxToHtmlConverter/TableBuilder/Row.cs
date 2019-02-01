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
        public Row(string styleClass, string id)
        {
            this.styleClass = styleClass;
            this.id = id;
            children = new List<ICell>();
            cellNode = CreateCellNode();
        }
        private HtmlNode CreateCellNode()
        {
            return HtmlNode.CreateNode($"<tr id=\"{id}\" class=\"{styleClass}\"></tr>");
        }
    }
}
