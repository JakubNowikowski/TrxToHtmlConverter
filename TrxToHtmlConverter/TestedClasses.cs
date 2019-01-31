using HtmlAgilityPack;
using System;
using System.Linq;

namespace TrxToHtmlConverter
{
	class TestedClasses
	{
		public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
		{
			var tableTestCase = doc.DocumentNode.SelectSingleNode("/html/body")
				.Elements("div")
				.First(d => d.Id == "SummaryTables")
				.Elements("table").First(d => d.Id == "TestedClassesTable")
				.Element("tbody");
			CreateTestedClassesTableRows(tableTestCase, testLoadResult);

			return doc;
		}

		private static void CreateTestedClassesTableRows(HtmlNode tableTestCase, TestLoadResult testLoadResult)
		{
			foreach (string test in testLoadResult.AllTestedClasses)
			{
				HtmlNode xd = HtmlNode.CreateNode($"<tr><td>{test}</td></tr>");

				tableTestCase.AppendChild(xd);
			}
		}
	}
}
