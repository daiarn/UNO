﻿using System.Collections.Generic;
using UNO_Client.Adapter;

namespace UNO_Client.Interpreter
{
	public class Parser : Expression
    {
        readonly List<Expression> tree;

        public Parser(IConnection connection)
        {
            // Build the 'parse tree'
            tree = new List<Expression>
            {
                new DrawExpression(connection),
                new PutExpression(connection)
            };
        }

        public override void Interpret(Context context)
        {
            // Interpret
            foreach (Expression exp in tree)
            {
                exp.Interpret(context);
            }
        }
    }
}
