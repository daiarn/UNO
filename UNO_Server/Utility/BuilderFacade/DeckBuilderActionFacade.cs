using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Models;

namespace UNO_Server.Utility.BuilderFacade
{
    public class DeckBuilderActionFacade : DeckBuilderFacade
    {
        public DeckBuilderActionFacade(Deck deck)
        {
            this.deck = deck;
        }

        public DeckBuilderActionFacade addActionCards(int num)
        {
            for (int j = 0; j < num; j++)
                addAllColors(deck, CardType.Skip);

            for (int j = 0; j < num; j++)
                addAllColors(deck, CardType.Reverse);

            for (int j = 0; j < num; j++)
                addAllColors(deck, CardType.Draw2);

            return this;
        }

        public DeckBuilderActionFacade addSkipCards(int num)
        {
            for (int j = 0; j < num; j++)
                addAllColors(deck, CardType.Skip);
            return this;
        }

        public DeckBuilderActionFacade addReverseCards(int num)
        {
            for (int j = 0; j < num; j++)
                addAllColors(deck, CardType.Reverse);
            return this;
        }

        public DeckBuilderActionFacade addDraw2Cards(int num)
        {
            for (int j = 0; j < num; j++)
                addAllColors(deck, CardType.Draw2);

            return this;
        }

    }
}
