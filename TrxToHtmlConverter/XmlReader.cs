using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TrxToHtmlConverter
{
	class XmlReader
	{
        XDocument doc;
        string xmlns;

        public XmlReader(string file)
        {
            doc = XDocument.Load(file);
            xmlns = doc.Root.Name.Namespace.NamespaceName;
        }

        public string GetValue(string valueName)
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute(valueName).Value;
        }
    }
}
