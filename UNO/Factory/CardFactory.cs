namespace UNO.Factory
{
    public class CardFactory : Factory
    {
        public Card CreateCard(string color, int number, string cardType, string actionType)
        {
            if(cardType == "number")
            {
                return new NumberCard(color, number);
            }
            if (cardType == "action")
            {
                return new ActionCard(color, actionType);
            }
        }

        public Card[] generateCards()
        {

        }
    }
}


/*
public static Factory factory;
    MAIN METHOD
        string[] colors = ["green", "yellow", "blue", "red"];
        string[] actions = ["reverse", "skip", "draw2"];
        List<var> deckOfCards = new List<var>();  
        factory = new CardFactory();
        for (int j=0; i < len(colors); j++)
        {
            for (int i = 0; i < 10; i++)
            {
                card = factory.CreateCard(colors[j], i, "number", null);
                deckOfCards.Add(card);
            }

            for (int a = 0; a < len(actions); a++)
            {       
                card = factory.CreateCard(colors[j], i, "number", actions[a]);
                deckOfCards.Add(card);
            }
        }

    

    

 
*/
