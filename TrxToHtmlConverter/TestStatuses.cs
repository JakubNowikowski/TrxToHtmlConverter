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

			return table.cellNode;
		}

		private static HtmlNode CreateRunTimeSummary(HtmlDocument doc, TestLoadResult testLoadResult)
		{
			Table table = new Table("RunTimeSummaryTable", "rightTable", "Run Time Summary");
			Row[] rows = new Row[]
			{
				new Row("", "startTime"),
				new Row("", "endTime"),
				new Row("", "duration")
			};
			foreach (Row row in rows)
			{
				string value = "", rowName = "";
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
			
			return table.cellNode;
		}

		private static string TestsDuration(DateTime startTime, DateTime stopTime)
		{
			return string.Format("{0:hh\\:mm\\:ss\\.fff}", (stopTime - startTime));
		}
	}
}

