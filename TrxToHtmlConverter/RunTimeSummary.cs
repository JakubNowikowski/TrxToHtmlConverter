using HtmlAgilityPack;
using System;
using System.Linq;
using TrxToHtmlConverter.TableBuilder;

namespace TrxToHtmlConverter
{
    public class RunTimeSummary : TableCreator
    {
        public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("RunTimeSummaryTable", "rightTable", "Run Time Summary");
            Row[] rows = new Row[]
            {
				new Row("", "classCount"),
				new Row("", "startTime"),
                new Row("", "endTime"),
                new Row("", "duration")
			};
            foreach (Row row in rows)
            {
                string value = "", rowName = "";
                if (row.id == "classCount") { value = testLoadResult.totalTestsProp.StartTime.ToString(); rowName = "Number of tested classes"; }
                if (row.id == "startTime") { value = testLoadResult.totalTestsProp.StartTime.ToString(); rowName = "Start Time"; }
                if (row.id == "endTime") { value = testLoadResult.totalTestsProp.FinishTime.ToString(); rowName = "End Time"; }
                if (row.id == "duration") { value = TestsDuration(testLoadResult.totalTestsProp.StartTime, testLoadResult.totalTestsProp.FinishTime); rowName = "Duration"; }

                Cell[] cells = new Cell[]
            {
                new Cell("mainColumn", "", true, rowName),
                new Cell(value)
            };
                row.Add(cells);
            }

            table.Add(rows);

            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(table.cellNode);
            return doc;
        }
        private static string TestsDuration(DateTime startTime, DateTime stopTime)
        {
            return string.Format("{0:hh\\:mm\\:ss\\.fff}",(stopTime - startTime));
        }
    }
}
