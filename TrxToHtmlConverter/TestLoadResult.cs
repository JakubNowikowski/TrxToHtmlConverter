using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter
{
	public class TestLoadResult
	{
		public IEnumerable<Test> tests { get; set; }
		public TotalTestsProperties totalTestsProp { get; set; }

	}
}
