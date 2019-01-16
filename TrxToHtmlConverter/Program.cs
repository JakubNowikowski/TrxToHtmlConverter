using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TrxToHtmlConverter
{
	class Program
	{

		static void Main(string[] args)
		{

            //XDocument doc = XDocument.Load("report.trx");
            //string xmlns = doc.Root.Name.Namespace.NamespaceName;
            //var results = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Results", xmlns));
            //var summary = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("total").Value;

            HtmlGeneration Html = new HtmlGeneration(@"C:\Users\ivakrupa\Downloads\xd2.html");
            Html.Generation();

            //var id =doc.Root.Element(XName.Get("TestRun"));
            //var results = doc.Element("TestRun").Element("Results").Elements();

            //foreach (var child in results.Elements())
            //{
            //	Console.WriteLine(child.Attribute("testName").Value);
            //}


        }
    }
}
