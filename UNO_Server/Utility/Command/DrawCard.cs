using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Utility.Command
{
    public class DrawCard : ICommand
    {
        int playerNumber;

        public void Execute()
        {
            playerNumber = Models.Game.GetInstance().activePlayerIndex;
            Models.Game.GetInstance().PlayerDrawsCard();
        }

        public void Undo()
        {
            Models.Game.GetInstance().UndoDrawCard(playerNumber);
            playerNumber = -1;
        }
    }
}
