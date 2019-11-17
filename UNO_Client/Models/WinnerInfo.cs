using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class WinnerInfo
    {
        private int index;
        private int score;
        private int turn;

        public int Index { get => index; set => index = value; }
        public int Score { get => score; set => score = value; }
        public int Turn { get => turn; set => turn = value; }
    }
}
