using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public interface ICell
    {
        string Id { get; set; }
        string StyleClass { get; set; }
        HtmlNode CellNode { get; set; }
        void Add(ICell cell);
        void Add(ICell[] cells);
    }
}
