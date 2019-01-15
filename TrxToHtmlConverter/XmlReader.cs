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

        public string GetTotal()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("total").Value;
        }

        public string GetExecuted()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("executed").Value;
        }

        public string GetPassed()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("passed").Value;
        }

        public string GetFailed()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("failed").Value;
        }

        public string GetError()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("error").Value;
        }

        public string GetTimeout()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("timeout").Value;
        }

        public string GetAborted()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("aborted").Value;
        }

        public string GetInconclusive()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("inconclusive").Value;
        }

        public string GetPassedButRunAborted()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("passedButRunAborted").Value;
        }

        public string GetNotRunnable()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("notRunnable").Value;
        }

        public string GetNotExecuted()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("notExecuted").Value;
        }

        public string GetDisconnectedl()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("disconnected").Value;
        }

        public string GetWarning()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("warning").Value;
        }

        public string GetCompleted()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("completed").Value;
        }

        public string GetInProgress()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("inProgress").Value;
        }

        public string GetPending()
        {
            return doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns)).Attribute("pending").Value;
        }
    }
}
