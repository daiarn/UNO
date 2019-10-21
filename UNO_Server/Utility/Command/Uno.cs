using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Utility.Command
{
    public class Uno : ICommand
    {
        public void Execute()
        {
            Models.Game.GetInstance().PlayerSaysUNO();
        }

        public void Undo()
        {
            Models.Game.GetInstance().UndoUno();
        }
    }
}
