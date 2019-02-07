using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class Cell : CellBase
    {
        protected bool isTh;
        protected string colSpan;
        protected string onClick;
        protected override string tagName { get; set; } = "td";

        public Cell(string content, string styleClass, string id, bool isTh, string colSpan = null, string onClick = null) : base(content, styleClass, id)
        {
            this.isTh = isTh;
            this.colSpan = colSpan;
            this.onClick = onClick;
        }

        public Cell(string content, bool isTh = false, string colSpan = null, string onClick = null) : base(content)
        {
            this.colSpan = colSpan;
            this.isTh = isTh;
            this.onClick = onClick;
        }

        public Cell() : base() { }

        protected override HtmlNode CreateCellNode()
        {
            if (isTh)
                tagName = "th";
            var hasColSpan = !string.IsNullOrWhiteSpace(colSpan);
            var hasOnClick = !string.IsNullOrWhiteSpace(onClick);
            var colSpanString = hasColSpan ? $"colspan=\"{colSpan}\" " : "";
            var onClickString = hasOnClick ? $"onclick=\"{onClick}\" " : "";
            additional = colSpanString + onClickString;
            return base.CreateCellNode();
        }
    }
}
