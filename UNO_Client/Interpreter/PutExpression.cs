using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Interpreter
{
    class PutExpression : Expression
    {
        public override void Interpret(Context context)
        {
            string[] parts = context.Input.Split(' ');
            if (parts[0] != "PUT")
            {
                return;
            }
        }
    }
}
