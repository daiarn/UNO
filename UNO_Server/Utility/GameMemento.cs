using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNO_Server.Models;
using UNO_Server.Models.SendData;

namespace UNO_Server.Utility
{
    public class GameMemento
    {
        public GamePhase phase;

        private bool flowClockWise { get; set; }
        private Deck drawPile { get; set; }
        private Deck discardPile { get; set; }

        public List<Card> hand1;
        public List<Card> hand2;
        public int numPlayers;

        public int activePlayerIndex;

        public GameMemento(Game game)
        {
            this.phase = game.phase;
            this.flowClockWise = game.flowClockWise;
            this.drawPile = game.drawPile.MakeDeepCopy();
            this.discardPile = game.discardPile.MakeDeepCopy();
            this.hand1 = game.players[activePlayerIndex].hand.ConvertAll(card => new Card(card));
            this.hand2 = game.players[game.GetNextPlayerIndexAfter(activePlayerIndex)].hand.ConvertAll(card => new Card(card));
            this.numPlayers = game.numPlayers;
            this.activePlayerIndex = game.activePlayerIndex;

        }

        public void RestoreMemento(Game game)
        {
            game.phase = this.phase;
            game.flowClockWise = this.flowClockWise;
            game.drawPile = this.drawPile;
            game.discardPile = this.discardPile;
            game.activePlayerIndex = this.activePlayerIndex;
            game.players[game.activePlayerIndex].hand = this.hand1;
            game.players[game.GetNextPlayerIndexAfter(game.activePlayerIndex)].hand = this.hand2;
            game.numPlayers = this.numPlayers;
        }
    }
}
