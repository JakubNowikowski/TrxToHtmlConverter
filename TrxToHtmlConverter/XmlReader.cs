using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TrxToHtmlConverter
{
	public class XmlReader
	{
		XDocument doc;
		string xmlns;

		public XmlReader(string file)
		{
			doc = XDocument.Load(file);
			xmlns = doc.Root.Name.Namespace.NamespaceName;
		}

		public TestLoadResult CreateTestLoadResult()
		{
			return new TestLoadResult()
			{
				tests = AllTestsResults(),
				totalTestsProp = LoadTotalTestsProperties(),
				AllTestedClasses = LoadAllTestedClasses()
			};
		}



		public IEnumerable<Test> AllTestsResults()
		{
			var allTests = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Results", xmlns)).Elements().ToList();
			var allTestDefinitions = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("TestDefinitions", xmlns)).Elements();

			IEnumerable<Test> joinedList = allTests.Join(allTestDefinitions,
				e => e.Attribute("testId").Value,
				e => e.Attribute("id").Value,
				(XElement e, XElement e2) => new Test()
				{
					MethodName = e.Attribute("testName").Value,
					ID = e.Attribute("testId").Value,
					ClassName = e2.Element(XName.Get("TestMethod", xmlns)).Attribute("className").Value.ToString().Split('.').Last().Split('+').First(),
					Result = e.Attribute("outcome").Value,
					StartTime = e.Attribute("startTime").Value,
					Duration = e.Attribute("duration").Value
				});
			return joinedList;
		}

		public List<string> LoadAllTestedClasses()
		{
			List<string> AllTestedClasses = new List<string>();
			var allTestDefinitions = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("TestDefinitions", xmlns)).Elements();
			foreach (var e in allTestDefinitions)
			{
				AllTestedClasses.Add(e.Element(XName.Get("TestMethod", xmlns)).Attribute("className").Value);
			}
			AllTestedClasses.Sort();
			return RemoveDuplicates(AllTestedClasses);
		}

		public static List<string> RemoveDuplicates(List<string> list)
		{
			list = list.Select(s => s.Split('.').Last().Split('+').First()).ToList();

			for (int i = 0; i < list.Count; i++)
			{
				list[i] = list[i].Split('.').Last();
				list[i] = list[i].Split('+').First();
			}
			return list.Distinct().ToList();
		}

		public TotalTestsProperties LoadTotalTestsProperties()
		{
			var total = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("ResultSummary", xmlns)).Element(XName.Get("Counters", xmlns));
			var startTime = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Times", xmlns)).Attribute("start");
			var finishTime = doc.Element(XName.Get("TestRun", xmlns)).Element(XName.Get("Times", xmlns)).Attribute("finish");
			XAttribute testCategory;

			if (
			doc.Element(XName.Get("TestRun", xmlns))
			.Element(XName.Get("TestDefinitions", xmlns))
			.Element(XName.Get("UnitTest", xmlns))
			.Element(XName.Get("TestCategory", xmlns)) != null)
			{
				testCategory = doc.Element(XName.Get("TestRun", xmlns))
					.Element(XName.Get("TestDefinitions", xmlns))
					.Element(XName.Get("UnitTest", xmlns))
					.Element(XName.Get("TestCategory", xmlns))
					.Element(XName.Get("TestCategoryItem", xmlns))
					.Attribute("TestCategory");
			}
			else
				testCategory = doc.Element(XName.Get("TestRun", xmlns)).Attribute("name");

			return new TotalTestsProperties()
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
				StartTime = DateTime.Parse(startTime.Value.ToString()),
				FinishTime = DateTime.Parse(finishTime.Value.ToString()),
				TestCategory = testCategory.Value.ToString()
			};
		}
	}

}