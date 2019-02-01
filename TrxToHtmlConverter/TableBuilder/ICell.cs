using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public abstract class ICell
    {
        public string id;
        protected string styleClass;
        protected string content;
        public HtmlNode cellNode;
        protected string colSpan;
        protected List<ICell> children;

        public void Add(ICell cell)
        {
                cellNode.AppendChild(cell.cellNode); //todo
        }
        public void Add(ICell[] cells)
        {
            foreach (ICell cell in cells)
                cellNode.AppendChild(cell.cellNode);
        }
        public static string ToUpperFirstLetter(string word)
        {
            if (word != "") { word = word[0].ToString().ToUpper() + word.Substring(1); }
            else { }
            return word;
        }
    }
}
