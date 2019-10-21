using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Models;

namespace UNO_Server.Utility.BuilderFacade
{
    public class DeckBuilderNumberFacade : DeckBuilderFacade
    {
        public DeckBuilderNumberFacade(Deck deck)
        {
            this.deck = deck;
        }

        public DeckBuilderNumberFacade addNonZeroNumberCards(int num)
        {
            for (int i = 1; i < 10; i++)
                for (int j = 0; j < num; j++)
                    addAllColors(deck, Card.numberCardTypes[i]);

            return this;
        }

        public DeckBuilderNumberFacade addIndividualNumberCards(int card, int num)
        {
            if (card < 0 || card > 9) return this;
            addAllColors(deck, Card.numberCardTypes[num]);
            return this;
        }

    }
}
