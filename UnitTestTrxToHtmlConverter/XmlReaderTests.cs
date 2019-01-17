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
    }
}
