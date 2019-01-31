using HtmlAgilityPack;
using System.Linq;
using TrxToHtmlConverter.TableBuilder;

namespace TrxToHtmlConverter
{
    public class TestStatuses : TableCreator
    {
        public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("testsStatusesTable", "leftTable","Tests Statuses");
            Row[] rows = new Row[] 
            { 
                new Row("", "total"),
                new Row("", "passed"),
                new Row("", "failed"),
                new Row("", "inconclusive"),
                new Row("", "warning")
            };
            foreach(Row row in rows)
            {
                string value = "";
                if (row.id == "total") value = testLoadResult.totalTestsProp.Total;
                if (row.id == "passed") value = testLoadResult.totalTestsProp.Passed;
                if (row.id == "failed") value = testLoadResult.totalTestsProp.Failed;
                if (row.id == "inconclusive") value = testLoadResult.totalTestsProp.Inconclusive;
                if (row.id == "warning") value = testLoadResult.totalTestsProp.Warning;

                Cell[] cells = new Cell[]
            {
                new Cell("mainColumn", "", true, Table.ToUpperFirstLetter(row.id)),
                new Cell(value)
            };
                row.Add(cells);
            }

            table.Add(rows);

            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(table.cellNode);
            return doc;
        }
    }
}
