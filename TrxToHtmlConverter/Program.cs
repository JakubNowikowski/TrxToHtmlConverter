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

            XmlReader doc = new XmlReader("report.trx");

            Console.WriteLine(doc.GetTotal() + "\n");
            Console.WriteLine(doc.GetExecuted() + "\n");
            Console.WriteLine(doc.GetPassed() + "\n");
            Console.WriteLine(doc.GetFailed() + "\n");
            Console.WriteLine(doc.GetError() + "\n");
            Console.WriteLine(doc.GetTimeout() + "\n");
            Console.WriteLine(doc.GetAborted() + "\n");
            Console.WriteLine(doc.GetInconclusive() + "\n");
            Console.WriteLine(doc.GetPassedButRunAborted() + "\n");
            Console.WriteLine(doc.GetNotRunnable() + "\n");
            Console.WriteLine(doc.GetNotExecuted() + "\n");
            Console.WriteLine(doc.GetDisconnectedl() + "\n");
            Console.WriteLine(doc.GetWarning() + "\n");
            Console.WriteLine(doc.GetCompleted() + "\n");
            Console.WriteLine(doc.GetInProgress() + "\n");
            Console.WriteLine(doc.GetPending() + "\n");


            Console.ReadKey();

            //var id =doc.Root.Element(XName.Get("TestRun"));
            //var results = doc.Element("TestRun").Element("Results").Elements();

            //foreach (var child in results.Elements())
            //{
            //	Console.WriteLine(child.Attribute("testName").Value);
            //}


        }
    }
}
