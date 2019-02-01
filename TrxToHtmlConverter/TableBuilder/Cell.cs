using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class Cell : NodeBase
    {
        protected bool isTh;
        protected string colSpan;
        protected override string tagName { get; set; } = "td";

        public Cell(string content, string styleClass, string id, bool isTh, string colSpan = "") : base(content, styleClass, id)
        {
            this.isTh = isTh;
            this.colSpan = colSpan;
        }

        public Cell(string content, string colSpan = "", bool isTh = false) : base(content)
        {
            this.colSpan = colSpan;
            this.isTh = isTh;
        }

        public Cell() : base() { }

        protected override HtmlNode CreateCellNode()
        {
            if (isTh)
                tagName = "th";
            additional = $"colspan=\"{colSpan}\"";
            return base.CreateCellNode();
        }
    }
}
