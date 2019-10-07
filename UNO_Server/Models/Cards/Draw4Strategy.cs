using System;

namespace UNO.Models
{
    class Draw4Strategy : ICardStrategy
    {
        public void Action()
        {
            for (int i = 0; i < 4; i++)
            {
                Game.GetInstance().nextPlayer.hand.Add(Game.GetInstance().FromDrawPile());
            }
            Game.GetInstance().SkipAction();
        }
    }
}