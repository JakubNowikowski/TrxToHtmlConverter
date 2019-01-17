using System;
using System.Collections.Generic;
using NUnit.Framework;
using TrxToHtmlConverter;

namespace UnitTestTrxToHtmlConverter
{
    [TestFixture]
    public class XmlReaderTests
    {
        [Test]
        public void AllTestsResults_ValidFileAsInput_IEnumarableCreatedCorrectly()
        {
            XmlReader reader = new XmlReader(@"C:\Users\OptiNav\Source\Repos\JakubNowikowski\TrxToHtmlConverter\TrxToHtmlConverter\bin\Debug\shortreport.trx");
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
            XmlReader reader = new XmlReader(@"C:\Users\OptiNav\Source\Repos\JakubNowikowski\TrxToHtmlConverter\TrxToHtmlConverter\bin\Debug\fullreport.trx");
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
        public void LoadTotalTestsProperties_ValidFileAsInput_TotalTestPropertiesCorrectlyLoaded()
        {
            XmlReader reader = new XmlReader(@"C:\Users\OptiNav\Source\Repos\JakubNowikowski\TrxToHtmlConverter\TrxToHtmlConverter\bin\Debug\shortreport.trx");
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
                StartTime = "2019-01-15T12:06:36.8892587+01:00",
                FinishTime = "2019-01-15T12:06:41.3405171+01:00",
                TestCategory = "DxfExportingTests"
            };

            var actual = reader.LoadTotalTestsProperties();

            Assert.IsTrue(expected.Equals(actual));
        }
    }
}
