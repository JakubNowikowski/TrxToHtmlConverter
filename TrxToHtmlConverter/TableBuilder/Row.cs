using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public class Row : NodeBase
    {
        protected override string tagName { get; set; } = "tr";
        public Row(string styleClass, string id) : base()
        {
            this.styleClass = styleClass;
            this.id = id;
        }
    }
}
