using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Interpreter
{
    public class ThousandExpression : NumberExpression
    {
		protected override string One() { return "M"; }
		protected override string Four() { return " "; }
		protected override string Five() { return " "; }
		protected override string Nine() { return " "; }
		protected override int Multiplier() { return 1000; }
    }
}
