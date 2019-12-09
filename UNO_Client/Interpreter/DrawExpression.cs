using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO_Client.Adapter;
using UNO_Client.Models;

namespace UNO_Client.Interpreter
{
    class DrawExpression : Expression
    {
        private readonly IConnection connection;

        public DrawExpression(IConnection connection)
        {
            this.connection = connection;
        }

        public override async void Interpret(Context context)
        {
            string[] parts = context.Input.Split(' ');
            if (parts[0] != "DRAW")
            {
                return;
            }
            var response = await connection.SendDrawCardAsync();
            context.Output = string.Format("Success: {0} Message: {1}", response.Success, response.Message);
        }
    }
}
