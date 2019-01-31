using HtmlAgilityPack;
using System;
using System.Linq;
using TrxToHtmlConverter.TableBuilder;

namespace TrxToHtmlConverter
{
	public class TestStatuses : TableCreator
	{
		public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
		{
			HtmlNode div = HtmlNode.CreateNode("<div id=\"SummaryTables\" class=\"wrap\"></div");
			div.AppendChild(CreateTestStatuses(doc, testLoadResult));
			div.AppendChild(CreateRunTimeSummary(doc, testLoadResult));
            div.AppendChild(CreateTestedClasses(doc, testLoadResult));
            doc.DocumentNode.SelectSingleNode("/html/body").AppendChild(div);
			return doc;
		}

		private static HtmlNode CreateTestStatuses(HtmlDocument doc, TestLoadResult testLoadResult)
		{
			Table table = new Table("testsStatusesTable", "leftTable", "Tests Statuses");
			Row[] rows = new Row[]
			{
				new Row("", "total"),
				new Row("", "passed"),
				new Row("", "failed"),
				new Row("", "inconclusive"),
				new Row("", "warning")
			};
			foreach (Row row in rows)
			{
				string value = "", cellClass = "";
                if (row.id == "total") { value = testLoadResult.totalTestsProp.Total; cellClass = "mainColumn"; }
                if (row.id == "passed") { value = testLoadResult.totalTestsProp.Passed; cellClass = "mainColumn"; }
                if (row.id == "failed") { value = testLoadResult.totalTestsProp.Failed; cellClass = "mainColumn"; }
                if (row.id == "inconclusive") { value = testLoadResult.totalTestsProp.Inconclusive; cellClass = "mainColumn"; }
                if (row.id == "warning") { value = testLoadResult.totalTestsProp.Warning; cellClass = "mainColumnLastRow"; }

				Cell[] cells = new Cell[]
			{
				new Cell(cellClass, "", true, Table.ToUpperFirstLetter(row.id)),
				new Cell(value)
			};
				row.Add(cells);
			}

			table.Add(rows);

			return table.cellNode;
		}

		private static HtmlNode CreateRunTimeSummary(HtmlDocument doc, TestLoadResult testLoadResult)
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
				string value = "", rowName = "", cellClass = "";
                if (row.id == "classCount") { value = testLoadResult.AllTestedClasses.Count.ToString(); rowName = "Number of tested classes"; cellClass = "mainColumn"; }
				if (row.id == "startTime") { value = testLoadResult.totalTestsProp.StartTime.ToString(); rowName = "Start Time"; cellClass = "mainColumn"; }
				if (row.id == "endTime") { value = testLoadResult.totalTestsProp.FinishTime.ToString(); rowName = "End Time"; cellClass = "mainColumn"; }
				if (row.id == "duration") { value = TestsDuration(testLoadResult.totalTestsProp.StartTime, testLoadResult.totalTestsProp.FinishTime); rowName = "Duration"; cellClass = "mainColumnLastRow"; }

				Cell[] cells = new Cell[]
			{
				new Cell(cellClass, "", true, rowName),
				new Cell(value)
			};
				row.Add(cells);
			}

			table.Add(rows);
			
			return table.cellNode;
		}

        private static HtmlNode CreateTestedClasses(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            Table table = new Table("TestedClassesTable", "hiddenTable", "Tested Classes");

            foreach (string className in testLoadResult.AllTestedClasses)
            {
                Row row = new Row("", "");
                Cell cell = new Cell(className);
                row.Add(cell);
                table.Add(row);
            }

            return table.cellNode;
        }

        private static string TestsDuration(DateTime startTime, DateTime stopTime)
		{
			return string.Format("{0:hh\\:mm\\:ss\\.fff}", (stopTime - startTime));
		}
	}
}

