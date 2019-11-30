using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Interpreter
{
    public class HundredExpression : NumberExpression
    {
		protected override string One() { return "C"; }
		protected override string Four() { return "CD"; }
		protected override string Five() { return "D"; }
		protected override string Nine() { return "CM"; }
		protected override int Multiplier() { return 100; }
    }
}
