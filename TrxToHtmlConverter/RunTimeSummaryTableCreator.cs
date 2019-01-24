using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter
{
    public class RunTimeSummaryTableCreator
    {
        public static HtmlDocument CreateRunTimeSummaryTable(HtmlDocument doc, TestLoadResult testLoadResult)
        {
            doc = ReplaceOneRunTimeSummaryValue(doc, "startTime", testLoadResult.totalTestsProp.StartTime.ToString());
            doc = ReplaceOneRunTimeSummaryValue(doc, "endTime", testLoadResult.totalTestsProp.FinishTime.ToString());
            doc = ReplaceOneRunTimeSummaryValue(doc, "duration", string.Format("{0:hh\\:mm\\:ss\\.fff}",
                TestsDuration(testLoadResult.totalTestsProp.StartTime, testLoadResult.totalTestsProp.FinishTime)));

            return doc;
        }

        private static HtmlDocument ReplaceOneRunTimeSummaryValue(HtmlDocument doc, string id, string value)
        {
            var totalResultNode = doc.DocumentNode.SelectSingleNode("/html/body")
			.Element("div") // divToRefresh
			.Elements("div")
			.First(d => d.Id == "test")
			.Element("div") //summaryDiv
			.Element("div") //wrap
			.Elements("div").First(d => d.Id == "columnRight")
			.Elements("table").First(d => d.Id == "SummaryTable")
			.Element("tbody")
			.Elements("tr").First(d => d.Id == id)
			;
			var valueNode = totalResultNode.Element("td").InnerText;
            valueNode = valueNode.Replace("VALUE", value);
            totalResultNode.Element("td").InnerHtml = HtmlDocument.HtmlEncode(valueNode);

            return doc;
        }

        private static TimeSpan TestsDuration(DateTime startTime, DateTime stopTime)
        {
            return stopTime - startTime;
        }
    }
}
