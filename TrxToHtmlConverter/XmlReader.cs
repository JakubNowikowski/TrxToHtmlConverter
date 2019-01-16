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
		public TestLoadResult TestLoadResult = new TestLoadResult();
		public TotalTestsProperties TotalTestsProperties;
		public List<string> AllTestedClasses = new List<string>();

		public XmlReader(string file)
		{
			doc = XDocument.Load(file);
			xmlns = doc.Root.Name.Namespace.NamespaceName;
			LoadValuesToReader();
		}

		public IEnumerable<Test> AllTestsResults()
		{
			var allTests = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Results", xmlns)).Elements().ToList();
			var allTestDefinitions = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("TestDefinitions", xmlns)).Elements();

			IEnumerable<Test> joinedList = allTests.Join(allTestDefinitions,
				(XElement e) =>
				{
					return e.Attribute("testId").Value;
				},
				(XElement e) =>
				{
					return e.Attribute("id").Value;
				},
				(XElement e, XElement e2) =>
				{
					return new Test()
					{
						MethodName = e.Attribute("testName").Value,
						ID = e.Attribute("testId").Value,
						ClassName = e2.Element(XName.Get("TestMethod", xmlns)).Attribute("className").Value,
						Result = e.Attribute("outcome").Value
					};
				});
			return joinedList;
		}

		public void LoadValuesToReader()
		{
			TestLoadResult.tests = AllTestsResults();
			LoadAllTestedClasses();
			LoadTotalTestsProperties();
		}

		public void LoadAllTestedClasses()
		{
			var allTestDefinitions = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("TestDefinitions", xmlns)).Elements();
			foreach (var e in allTestDefinitions)
			{
				AllTestedClasses.Add(e.Element(XName.Get("TestMethod", xmlns)).Attribute("className").Value);
			}

			for (int i = 0; i < AllTestedClasses.Count; i++)
			{
				AllTestedClasses[i] = AllTestedClasses[i].Split('.').Last();
				AllTestedClasses[i] = AllTestedClasses[i].Split('+').First();
			}

			AllTestedClasses = AllTestedClasses.Distinct().ToList();
		}

		public void LoadTotalTestsProperties()
		{
			var total = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns));
			var startTime = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Times", xmlns)).Attribute("start");
			var finishTime = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Times", xmlns)).Attribute("finish");

			TotalTestsProperties = new TotalTestsProperties()
			{
				Total = total.Attribute("total").Value.ToString(),
				Executed = total.Attribute("executed").Value.ToString(),
				Passed = total.Attribute("passed").Value.ToString(),
				Failed = total.Attribute("failed").Value.ToString(),
				Error = total.Attribute("error").Value.ToString(),
				Timeout = total.Attribute("timeout").Value.ToString(),
				Aborted = total.Attribute("aborted").Value.ToString(),
				Inconclusive = total.Attribute("inconclusive").Value.ToString(),
				PassedButRunAborted = total.Attribute("passedButRunAborted").Value.ToString(),
				NotRunnable = total.Attribute("notRunnable").Value.ToString(),
				NotExecuted = total.Attribute("notExecuted").Value.ToString(),
				Disconnected = total.Attribute("disconnected").Value.ToString(),
				Warning = total.Attribute("warning").Value.ToString(),
				Completed = total.Attribute("completed").Value.ToString(),
				InProgress = total.Attribute("inProgress").Value.ToString(),
				Pending = total.Attribute("pending").Value.ToString(),
				StartTime = startTime.Value.ToString(),
				FinishTime = finishTime.Value.ToString()

			};
		}
	}

}