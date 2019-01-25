using System.Collections.Generic;

namespace TrxToHtmlConverter
{
    public class TestLoadResult
	{
		public IEnumerable<Test> tests { get; set; }
		public TotalTestsProperties totalTestsProp { get; set; }
        public List<string> AllTestedClasses { get; set; }
    }
}
