using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TrxToHtmlConverter;

namespace UnitTestTrxToHtmlConverter
{
	[TestFixture]
	public class XmlReaderTests
	{
		public string filePathShortReport = @"..\..\shortreport.trx";
		public string filePathFullReport = @"..\..\fullreport.trx";

		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			var dir = Path.GetDirectoryName(typeof(XmlReaderTests).Assembly.Location);
			if (dir != null)
			{
				Environment.CurrentDirectory = dir;
				Directory.SetCurrentDirectory(dir);
			}
			else
				throw new Exception("Path.GetDirectoryName(typeof(TestingWithReferencedFiles).Assembly.Location) returned null");
		}

		[Test]
		public void AllTestsResults_ValidFileAsInput_IEnumarableCreatedCorrectly()
		{
			XmlReader reader = new XmlReader(filePathShortReport);
			IEnumerable<Test> expected = new List<Test>()
			{
				new Test()
				{
					MethodName = "Export_OneLine_CorrectlyExported",
					ID = "42d6fd3c-0bed-e5c0-25f8-dbd17516e7ec",
					ClassName = "Zeiss.IMT.NG.Optics.Tests.DxfExportTests",
					Result = "Passed"
				},
				new Test()
				{
					MethodName = "Export_OneCircle_CorrectlyExported",
					ID = "87ab6a24-2350-c6bb-d941-ec72a0698c69",
					ClassName = "Zeiss.IMT.NG.Optics.Tests.DxfExportTests",
					Result = "Passed"
				}
			};

			var actual = reader.AllTestsResults();

			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void LoadAllTestedClasses_ValidFileAsInput_ListCreatedCorrectly()
		{
			XmlReader reader = new XmlReader(filePathFullReport);
			List<string> expected = new List<string>()
			{
				"DxfExportTests",
				"DxfItemDescriptorTests",
				"DxfItemTests",
				"DxfValueTests",
				"DxfBuilderTests"
			};

			var actual = reader.LoadAllTestedClasses();

			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void RemoveDuplicates_ListWithDuplicatesAsInput_ListModifiedCorrectly()
		{
			List<string> actual = new List<string>()
			{
				"ZeissNamespace.DxfExportTests+DxfCircle",
				"ZeissNamespace.DxfExportTests+DxfCircle",
				"ZeissNamespace.DxfItemDescriptorTests+DxfLine",
				"ZeissNamespace.DxfItemDescriptorTests+DxfLine",
				"ZeissNamespace.DxfItemTests+DxfLine",
				"ZeissNamespace.DxfItemTests+DxfLine",
				"ZeissNamespace.DxfValueTests+DxfLine",
				"ZeissNamespace.DxfValueTests+DxfLine",
				"ZeissNamespace.DxfBuilderTests+DxfLine",
				"ZeissNamespace.DxfBuilderTests+DxfLine"
			};

			List<string> expected = new List<string>()
			{
				"DxfExportTests",
				"DxfItemDescriptorTests",
				"DxfItemTests",
				"DxfValueTests",
				"DxfBuilderTests"
			};

			actual = XmlReader.RemoveDuplicates(actual);

			CollectionAssert.AreEqual(expected, actual);


		}

        [Test]
        public void LoadTotalTestsProperties_ValidFileAsInput_TotalTestPropertiesCorrectlyLoaded()
        {
			DateTime startDate = DateTime.Parse("2019-01-15T12:06:36.8892587+01:00");
			DateTime finishDate = DateTime.Parse("2019-01-15T12:06:36.8892587+01:00");

			XmlReader reader = new XmlReader(filePathShortReport);
            TotalTestsProperties expected = new TotalTestsProperties()
            {
                Total = "97",
                Executed = "97",
                Passed = "97",
                Failed = "0",
                Error = "0",
                Timeout = "0",
                Aborted = "0",
                Inconclusive = "0",
                PassedButRunAborted = "0",
                NotRunnable = "0",
                NotExecuted = "0",
                Disconnected = "0",
                Warning = "0",
                Completed = "0",
                InProgress = "0",
                Pending = "0",
                StartTime = startDate,
                FinishTime = finishDate,
                TestCategory = "DxfExportingTests"
            };

            var actual = reader.LoadTotalTestsProperties();

            Assert.IsTrue(expected.Equals(actual));
        }
    }
}
