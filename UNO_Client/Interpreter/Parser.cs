using System.Collections.Generic;

namespace UNO_Client.Interpreter
{
	public class Parser : Expression
    {
        List<NumberExpression> tree;

        public Parser()
        {
            // Build the 'parse tree'
            tree = new List<NumberExpression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());
        }

        public override void Interpret(Context context)
        {
            // Interpret
            foreach (NumberExpression exp in tree)
            {
                exp.Interpret(context);
            }
        }
    }
}
