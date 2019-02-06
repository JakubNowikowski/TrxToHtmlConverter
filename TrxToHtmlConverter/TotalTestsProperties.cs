using System;

namespace TrxToHtmlConverter
{
    public class TotalTestsProperties
    {
        public string Total { get; set; }
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
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string TestCategory { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is TotalTestsProperties))
                return false;

            TotalTestsProperties o = (TotalTestsProperties)obj;

            return Total == o.Total && Executed == o.Executed && Passed == o.Passed && Failed == o.Failed &&
                    Error == o.Error && Timeout == o.Timeout && Aborted == o.Aborted && Inconclusive == o.Inconclusive &&
                    PassedButRunAborted == o.PassedButRunAborted && NotRunnable == o.NotRunnable && NotExecuted == o.NotExecuted && Disconnected == o.Disconnected &&
                    Warning == o.Warning && Completed == o.Completed && InProgress == o.InProgress && Pending == o.Pending &&
                    StartTime == o.StartTime && FinishTime == o.FinishTime && InProgress == o.InProgress && TestCategory == o.TestCategory;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
