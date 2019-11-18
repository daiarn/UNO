using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Client.Forms;

namespace UNO_Client.State
{
    public interface PlayerState
    {
        void WritePlayerStatusInGame(GameForm form, StateContext context);
    }
}
