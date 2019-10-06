using System;

namespace UNO.Models
{
    class Draw2Strategy : ICardStrategy
    {
        public void Action()
        {
            for (int i = 0; i < 2; i++)
            {
                Game.GetInstance().nextPlayer.hand.Add(Game.GetInstance().FromDrawPile());
            }
            Game.GetInstance().SkipAction();
        }
    }
}