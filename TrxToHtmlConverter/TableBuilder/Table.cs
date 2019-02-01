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
        private Row tableHeadRow;
        private string title;

        public Table(string id, string styleClass, string title)
        {
            this.id = id;
            this.styleClass = styleClass;
            this.title = title;
            children = new List<ICell>();
            cellNode = CreateCellNode();
        }

        public Table(string id, string styleClass, Row tableHeadRow)
        {
            this.id = id;
            this.styleClass = styleClass;
            this.tableHeadRow = tableHeadRow;
            children = new List<ICell>();
            cellNode = CreateCellNode();
        }

        private HtmlNode CreateCellNode()
        {
            //create table head and table empty table body
            HtmlNode newTableNode = HtmlNode.CreateNode($"<table id=\"{id}\" class=\"{styleClass}\"></table>");
            if (tableHeadRow == null)
            {
                HtmlNode tableTitleNode = HtmlNode.CreateNode($"<caption>{title}</caption>");
                newTableNode.AppendChild(tableTitleNode);
            }
            else
            {
                HtmlNode tableHeadNode = HtmlNode.CreateNode($"<thead>{tableHeadRow.Export().WriteTo()}</thead>");
                newTableNode.AppendChild(tableHeadNode);
            }
            HtmlNode tableBodyNode = HtmlNode.CreateNode("<tbody></tbody>");
            newTableNode.AppendChild(tableBodyNode);

            return newTableNode;
        }
    }
}
