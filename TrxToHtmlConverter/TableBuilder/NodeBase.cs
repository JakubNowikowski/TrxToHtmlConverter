using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter.TableBuilder
{
    public abstract class NodeBase
    {
        public string id;
        protected string styleClass;
        protected string content;
        protected List<NodeBase> children;
        protected virtual string tagName { get; set; } = "";
        protected string additional { get; set; }

        public NodeBase(string content, string styleClass = "", string id = "") : this()
        {
            this.styleClass = styleClass;
            this.id = id;
            this.content = ToUpperFirstLetter(content);
        }

        public NodeBase()
        {
            children = new List<NodeBase>();
        }

        public HtmlNode Export()
        {
            var cellNode = CreateCellNode();

            if(children.Count != 0)
            foreach(NodeBase cell in children)
            {
                cellNode.AppendChild(cell.Export());
            }

            return cellNode;
        }
        public void Add(NodeBase cell)
        {
            children.Add(cell); //todo
        }
        public void Add(NodeBase[] cells)
        {
            foreach (NodeBase cell in cells)
                children.Add(cell);
        }
        public static string ToUpperFirstLetter(string word)
        {
            if (word != "") { word = word[0].ToString().ToUpper() + word.Substring(1); }
            else { }
            return word;
        }

        protected virtual HtmlNode CreateCellNode()
        {
            var hasId = !string.IsNullOrEmpty(id);
            var hasClass = !string.IsNullOrEmpty(styleClass);
            var idString = hasId ? $"id=\"{id}\"" : "";
            var classString = hasClass ? $"class=\"{styleClass}\"" : "";
            return HtmlNode.CreateNode($"<{tagName} {idString} {classString} {additional}>{content}</{tagName}>");
        }
    }
}
