using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter
{
	public class TotalTestsProperties
	{
		public string Total{ get; set; }
		public string Executed { get; set; }
		public string Passed { get; set; }
		public string Failed { get; set; }
		public string Error { get; set; }
		public string Timeout { get; set; }
		public string Aborted { get; set; }
		public string Inconclusive { get; set; }
		public string PassedButRunAborted { get; set; }
		public string NotRunnable { get; set; }
		public string NotExecuted { get; set; }
		public string Disconnected { get; set; }
		public string Warning { get; set; }
		public string Completed { get; set; }
		public string InProgress { get; set; }
		public string Pending { get; set; }
		public string StartTime { get; set; }
		public string FinishTime { get; set; }
		public string TestCategory { get; set; }
 	}
}
