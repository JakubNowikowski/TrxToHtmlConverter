using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class ShowHideTableBody : ICell
    {

        private Row headRow;
        private ICell containerContent;

        public ShowHideTableBody(Row headRow, ICell containerContent)
        {
            this.headRow = headRow;
            this.containerContent = containerContent;
            children = new List<ICell>();
            cellNode = CreateCellNode();
        }

        private HtmlNode CreateCellNode()
        {
            HtmlNode newTableBodyNode = HtmlNode.CreateNode("<tbody></tbody>");
            Row containerRow = new Row("hiddenRow", (headRow.id + "Container"));
            Cell button = new Cell("ex", "", false, $"<div class=\"OpenMoreButton\" onclick=\"ShowHide('{containerRow.id}', '{(headRow.id + "Button")}', 'Show Tests', 'Hide Tests'); \">" +
                $"<div class=\"ButtonText\" id=\"{(headRow.id + "Button")}\">Show Tests</div></div>");
			Cell containerCell = new Cell("<div></div>", "10");

			headRow.Add(button);
            containerCell.Add(containerContent);
            containerRow.Add(containerCell);
            newTableBodyNode.AppendChild(headRow.Export());
            newTableBodyNode.AppendChild(containerRow.Export());

            return newTableBodyNode;
        }
    }
}
