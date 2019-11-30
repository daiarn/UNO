using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Interpreter
{
    public class Parser : Expression
    {
        List<Expression> tree;

        public Parser()
        {
            // Build the 'parse tree'
            tree = new List<Expression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());
        }
        public override void Interpret(Context context)
        {
            // Interpret
            foreach (Expression exp in tree)
            {
                exp.Interpret(context);
            }
        }

        public override string Five()
        {
            throw new NotImplementedException();
        }

        public override string Four()
        {
            throw new NotImplementedException();
        }

        public override int Multiplier()
        {
            throw new NotImplementedException();
        }

        public override string Nine()
        {
            throw new NotImplementedException();
        }

        public override string One()
        {
            throw new NotImplementedException();
        }
    }
}
