using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO_Client.Adapter;

namespace UNO_Client.Interpreter
{
    class DrawExpression : Expression
    {
        private readonly IConnection connection;

        public DrawExpression(IConnection connection)
        {
            this.connection = connection;
        }

        public override void Interpret(Context context)
        {
            string[] parts = context.Input.Split(' ');
            if (parts[0] != "DRAW")
            {
                return;
            }
            var resopnse = connection.SendDrawCardAsync();
            context.Output = resopnse.Result.Message;
        }
    }
}
