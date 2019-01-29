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

        public NewTableCreator(params string[] headerValues)
        {
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

        public static Row[] openHideRow;

        public string styleClass;


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
            var content = new HtmlNode(targetId);
            return new Cell(content);
        }
    }

    public interface IStyled
    {
        string styleClass { get; set; }
    }
}
