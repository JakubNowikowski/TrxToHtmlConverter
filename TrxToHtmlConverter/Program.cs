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

			XmlReader reader = new XmlReader("report.trx");
			//XDocument doc = XDocument.Load("report.trx");
			// var xmlns = doc.Root.Name.Namespace.NamespaceName;


			Console.WriteLine(reader.TotalTestsProperties.Total);









			Console.ReadKey();

		

		}
	}
}
