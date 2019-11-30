using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Interpreter
{
    public class OneExpression : NumberExpression
	{
        protected override string One() { return "I"; }
		protected override string Four() { return "IV"; }
		protected override string Five() { return "V"; }
		protected override string Nine() { return "IX"; }
		protected override int Multiplier() { return 1; }
    }
}
