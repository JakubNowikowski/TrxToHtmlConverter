using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class ShowHideTableBody : CellBase
    {

        private Row headRow;
        private CellBase containerContent;

        public ShowHideTableBody(Row headRow, CellBase containerContent) : base()
        {
            headRow.Parent = this;
            containerContent.Parent = this;
            this.headRow = headRow;
            this.containerContent = containerContent;
        }

        protected override HtmlNode CreateCellNode()
        {
            HtmlNode newTableBodyNode = HtmlNode.CreateNode("<tbody></tbody>");
            Row containerRow = new Row("hiddenRow", (headRow.id + "Container"));
            Cell button = new Cell($"<div class=\"OpenMoreButton\" onclick=\"ShowHide('{containerRow.id}', '{(headRow.id + "Button")}', 'Show Tests', 'Hide Tests'); \">" +
                $"<div class=\"ButtonText\" id=\"{(headRow.id + "Button")}\">Show Tests</div></div>", "", "ex", false);
			Cell containerCell = new Cell("<div></div>", colSpan: "11");

			headRow.Add(button);
            containerCell.Add(containerContent);
            containerRow.Add(containerCell);
            newTableBodyNode.AppendChild(headRow.Export());
            newTableBodyNode.AppendChild(containerRow.Export());

            return newTableBodyNode;
        }
    }
}
