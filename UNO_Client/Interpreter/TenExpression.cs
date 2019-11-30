using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Interpreter
{
    public class TenExpression : NumberExpression
	{
		protected override string One() { return "X"; }
		protected override string Four() { return "XL"; }
		protected override string Five() { return "L"; }
		protected override string Nine() { return "XC"; }
		protected override int Multiplier() { return 10; }
    }
}
