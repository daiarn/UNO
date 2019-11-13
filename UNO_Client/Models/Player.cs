using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    class Player
    {
        string name;
        int cardCount;
        bool isPlaying;

        public string Name { get => name; set => name = value; }
        public int CardCount { get => cardCount; set => cardCount = value; }
        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

        public string Id { get; set; }
    }
}
