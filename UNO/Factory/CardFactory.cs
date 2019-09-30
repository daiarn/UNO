namespace UNO.Factory
{
    public class CardFactory : Factory
    {
        public Card CreateCard(string color, int number, string cardType, string actionType, string wildType)
        {
            if(cardType == "number")
            {
                return new NumberCard(color, number);
            }
            if (cardType == "action")
            {
                return new ActionCard(color, actionType);
            }
            if (cardType == "wild")
            {
                return new WildCard(color, wildType);
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
        string[] wild = ["wild", "draw4"];
        List<var> deckOfCards = new List<var>();  
        factory = new CardFactory();
        for (int j=0; i < len(colors); j++)
        {
            for (int i = 0; i < 10; i++)
            {
                card = factory.CreateCard(colors[j], i, "number", null, null);
                deckOfCards.Add(card);
            }

            for (int a = 0; a < len(actions); a++)
            {       
                card = factory.CreateCard(colors[j], null, "action", actions[a], null);
                deckOfCards.Add(card);
            }
        }
        for (int b = 0; b < len(wild); b++)
        {
                card = factory.CreateCard("", null, "wild", null, wild[b]);
                deckOfCards.Add(card);
        }

    

    

 
*/
