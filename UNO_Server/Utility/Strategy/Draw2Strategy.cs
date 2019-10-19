using UNO_Server.Models;

namespace UNO_Server.Utility.Strategy
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