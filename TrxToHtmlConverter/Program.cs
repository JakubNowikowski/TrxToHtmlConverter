using System;
using TrxToHtmlConverter.TableBuilder;

namespace TrxToHtmlConverter
{
    class Program
	{
		static void Main(string[] args)
		{
            //string filePathFullReport = @"..\..\fullreport.trx";

            //XDocument doc = XDocument.Load("report.trx");
            //string xmlns = doc.Root.Name.Namespace.NamespaceName;
            //var results = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Results", xmlns));
            //var summary = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("total").Value;

            //HtmlGeneration Html = new HtmlGeneration("../../fullreport.trx", "../../nowyplik.html");
            //         Html.InitializeTrxData();
            //         Html.Generation();

            //XmlReader reader = new XmlReader(filePathFullReport);

            //var xd = reader.CreateTestLoadResult();

            //string data = xd.totalTestsProp.StartTime;

            //string str = "2019-01-15T12:06:36.8892587+01:00";

            ////DateTime myDate = DateTime.ParseExact(str, "yyyy-MM-dd HH:mm:ss,fff",
            ////					System.Globalization.CultureInfo.InvariantCulture);

            //DateTimeOffset loadedDate = DateTimeOffset.Parse(str);

            //Console.WriteLine(loadedDate.DateTime);
            Table table = new Table("tableid","tableclass","tabletitle");
            Row row = new Row("rowClass", "rowId");
            Cell cell = new Cell("cellClass","cellId",false,"cellContent");

            row.Add(cell);
            table.Add(row);

            Console.WriteLine(table.cellNode.WriteTo());

            Console.ReadKey();
		}
	}
}
