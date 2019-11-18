﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class GameState
    {
        int zeroCounter;
        int wildCounter;
        int discardPileCount;
        int drawPileCount;
        int activePlayer;
        int index;
        int scoreboardIndex;

        GamePhase gamePhase;
        Card activeCard;
        List<Player> players;
        List<Card> hand;
        WinnerInfo[] winners;

        public int ZeroCounter { get => zeroCounter; set => zeroCounter = value; }
        public int WildCounter { get => wildCounter; set => wildCounter = value; }
        public int DiscardPile { get => discardPileCount; set => discardPileCount = value; }
        public int DrawPile { get => drawPileCount; set => drawPileCount = value; }
        public int ActivePlayer { get => activePlayer; set => activePlayer = value; }
        public Card ActiveCard { get => activeCard; set => activeCard = value; }

        public List<Player> Players { get => players; set => players = value; }
        public List<Card> Hand { get => hand; set => hand = value; }
        public int Index { get => index; set => index = value; }
        internal WinnerInfo[] Winners { get => winners; set => winners = value; }
        public GamePhase GamePhase { get => gamePhase; set => gamePhase = value; }
        public int ScoreboardIndex { get => scoreboardIndex; set => scoreboardIndex = value; }
    }
}
