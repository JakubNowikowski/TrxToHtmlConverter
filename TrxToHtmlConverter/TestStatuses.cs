﻿using HtmlAgilityPack;
using System.Linq;

namespace TrxToHtmlConverter
{
    public class TestStatuses : TableCreator
    {
        public static HtmlDocument CreateTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            doc = ReplaceOneTotalValue(doc, "total", testLoadResult.totalTestsProp.Total);
            doc = ReplaceOneTotalValue(doc, "passed", testLoadResult.totalTestsProp.Passed);
            doc = ReplaceOneTotalValue(doc, "failed", testLoadResult.totalTestsProp.Failed);
            doc = ReplaceOneTotalValue(doc, "inconclusive", testLoadResult.totalTestsProp.Inconclusive);
            doc = ReplaceOneTotalValue(doc, "warning", testLoadResult.totalTestsProp.Warning);

            return doc;
        }

        private static HtmlDocument ReplaceOneTotalValue(HtmlDocument doc, string id, string value)
        {
			var totalResultNode = doc.DocumentNode.SelectSingleNode("/html/body")
			.Elements("div")
			.First(d => d.Id == "SummaryTables")
			.Elements("table").First(d => d.Id == "TestsStatusesTable")
			.Element("tbody")
			.Elements("tr").First(d => d.Id == id);
			var valueNode = totalResultNode.Element("td").InnerText;
            valueNode = valueNode.Replace("VALUE", value);
            totalResultNode.Element("td").InnerHtml = HtmlDocument.HtmlEncode(valueNode);

            return doc;
        }
    }
}