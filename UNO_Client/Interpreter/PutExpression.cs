using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO_Client.Adapter;
using UNO_Client.Models;

namespace UNO_Client.Interpreter
{
    class PutExpression : Expression
    {
        private readonly IConnection connection;

        public PutExpression(IConnection connection)
        {
            this.connection = connection;
        }
        public override async void Interpret(Context context)
        {
            string[] parts = context.Input.Split(' ');
            if (parts[0] != "PUT")
            {
                return;
            }
            string commandColor = parts[1].Remove(0, 1);
            CardColor color = PickColor(commandColor);
            CardType type = PickType(parts[1]);


            Card card = new Card((int)color, (int)type);
            var response = await connection.SendPlayCardAsync(card, card.Type);
            Output = string.Format("Success: {0} Message: {1}", response.Success, response.Message);
        }
        private CardColor PickColor(string commandColor)
        {
            CardColor color = CardColor.Red;
            switch (commandColor)
            {
                case "R":
                    color = CardColor.Red;
                    break;
                case "Y":
                    color = CardColor.Yellow;
                    break;
                case "G":
                    color = CardColor.Green;
                    break;
                case "B":
                    color = CardColor.Blue;
                    break;
            }
            return color;
        }
        private CardType PickType(string commandType)
        {
            CardType type = CardType.Zero;

            switch (commandType)
            {
                case "0":
                    type = CardType.Zero;
                    break;
                case "1":
                    type = CardType.One;
                    break;
                case "2":
                    type = CardType.Two;
                    break;
                case "3":
                    type = CardType.Three;
                    break;
                case "4":
                    type = CardType.Four;
                    break;
                case "5":
                    type = CardType.Five;
                    break;
                case "6":
                    type = CardType.Six;
                    break;
                case "7":
                    type = CardType.Seven;
                    break;
                case "8":
                    type = CardType.Eight;
                    break;
                case "9":
                    type = CardType.Nine;
                    break;

                case "S": // skip
                    type = CardType.Skip;
                    break;
                case "R": // reverse
                    type = CardType.Reverse;
                    break;
                case "D": // draw2
                    type = CardType.Draw2;
                    break;

                case "W": // wild
                    type = CardType.Wild;
                    break;
                case "D4": // draw4
                    type = CardType.Draw4;
                    break;
            }

            return type;
        }
    }
}
