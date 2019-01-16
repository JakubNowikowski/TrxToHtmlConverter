using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrxToHtmlConverter
{
	public class Test
	{
		public string MethodName { get; set; }
		public string ClassName { get; set; }
		public string ID { get; set; }
		public string Result { get; set; }

		public override string ToString()
		{
			return $"{MethodName} - {ClassName.Split('.').Last()} - Result: {Result}";
		}

        public override bool Equals(object obj)
        {
            if (!(obj is Test))
                return false;

            Test o = (Test)obj;

            return MethodName == o.MethodName && ClassName == o.ClassName && ID == o.ID && Result == o.Result;
        }
    }
}
