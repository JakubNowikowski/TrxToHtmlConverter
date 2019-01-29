using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter
{
    class NewTableCreator
    {
        private int columnCount;
        private string[] headers;
        private Row[] rows;
        private string caption;
        public string Id { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }
        public HtmlNode CreatedTable { get; set; }

        public NewTableCreator(string Id, string Class, string Title)
        {
            this.Id = Id;
            this.Class = Class;
            this.Title = Title;

            CreateTable("pierwszy", "drugi");
        }

        private void CreateTable(params string[] headerValues)
        {
            //create table head and table structure
            HtmlNode newTableNode = HtmlNode.CreateNode($"<table id=\"{Id}\" class=\"{Class}\"></table>");
            HtmlNode tableTitleNode = HtmlNode.CreateNode($"<caption>{Title}</caption>");
            newTableNode.AppendChild(tableTitleNode);
            HtmlNode tableBodyNode = HtmlNode.CreateNode("<tbody></tbody>");
            newTableNode.AppendChild(tableBodyNode);

            //create table rows
            foreach (string headerValue in headerValues)
            {
                HtmlNode tableRow = HtmlNode.CreateNode($"<tr id={headerValue.ToLower()}></tr>");
                HtmlNode tableRowHead = HtmlNode.CreateNode($"<th class=\"mainColumn\">{ToUpperFirstLetter(headerValue)}</th>");
                tableRow.AppendChild(tableRowHead);
                HtmlNode tableRowBody = HtmlNode.CreateNode($"<td>value</td>");
                tableRow.AppendChild(tableRowBody);
                newTableNode.LastChild.AppendChild(tableRow);
            }

            Console.WriteLine(newTableNode.WriteTo());
        }

        private string ToUpperFirstLetter(string word)
        {
            if (word != null) { word = word[0].ToString().ToUpper() + word.Substring(1); }
            else { }
            return word;
        }

        public NewTableCreator AddRow(Row newRow)
        {
            throw new NotImplementedException();
        }

        public void CreateOpenHideRows(Row topRowWithoutButton, Row bottomRow)
        {
            string guid = Guid.NewGuid().ToString();
            topRowWithoutButton.AddCell(Cell.Button(guid));
            bottomRow.Id = guid;
        }
    }

    class Row : IStyled
    {

        private Cell[] cells;
        public string Id { get; set; }
        string IStyled.styleClass { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static Row[] openHideRow;


        public Row()
        {

        }

        public Row AddCell(Cell cell)
        {
            throw new NotImplementedException();
        }
    }

    class Cell : IStyled
    {
        private HtmlNode content;
        private int span;
        private bool isTh;

        public static Cell failedTest;

        public string styleClass { get; set; }

        public Cell(HtmlNode content)
        {
            this.content = content;
        }

        public static Cell Button(string targetId)
        {
            HtmlNode content = HtmlNode.CreateNode(targetId);
            return new Cell(content);
        }
    }

    public interface IStyled
    {
        string styleClass { get; set; }
    }
}
