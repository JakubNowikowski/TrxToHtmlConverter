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
        public HtmlNode cellNode;
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string StyleClass { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public HtmlNode CellNode { get => cellNode; set => throw new NotImplementedException(); }

        public ShowHideTableBody(Row headRow, ICell containerContent)
        {
            this.headRow = headRow;
            this.containerContent = containerContent;
            cellNode = CreateCellNode();
        }

        private HtmlNode CreateCellNode()
        {
            HtmlNode newTableBodyNode = HtmlNode.CreateNode("<tbody></tbody>");
            Row containerRow = new Row("hiddenRow", (headRow.id + "Container"));
            Cell button = new Cell("ex", "", false, $"<div class=\"OpenMoreButton\" onclick=\"ShowHide('{containerRow.id}', '{(headRow.id + "Button")}', 'Show Tests', 'Hide Tests'); \">" +
                $"<div class=\"MoreButtonText\" id=\"{(headRow.id + "Button")}\">Show Tests</div></div>");
            Cell containerCell = new Cell("<div id=\"exceptionArrow\">↳</div>", "8");

            headRow.Add(button);
            containerCell.Add(containerContent);
            containerRow.Add(containerCell);
            newTableBodyNode.AppendChild(headRow.cellNode);
            newTableBodyNode.AppendChild(containerRow.cellNode);

            return newTableBodyNode;
        }

        public void Add(ICell cell)
        {
            throw new NotImplementedException();
        }

        public void Add(ICell[] cells)
        {
            throw new NotImplementedException();
        }
    }
}
