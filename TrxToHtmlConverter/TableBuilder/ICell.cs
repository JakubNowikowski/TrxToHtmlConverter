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
        protected HtmlNode cellNode;
        protected string colSpan;
        protected List<ICell> children;
        protected bool isTh;

        public HtmlNode Export()
        {
            if(children.Count != 0)
            foreach(ICell cell in children)
            {
                cellNode.AppendChild(cell.Export());
            }

            return cellNode;
        }
        public void Add(ICell cell)
        {
            children.Add(cell); //todo
        }
        public void Add(ICell[] cells)
        {
            foreach (ICell cell in cells)
                children.Add(cell);
        }
        public static string ToUpperFirstLetter(string word)
        {
            if (word != "") { word = word[0].ToString().ToUpper() + word.Substring(1); }
            else { }
            return word;
        }
    }
}
