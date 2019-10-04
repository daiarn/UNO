using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    public class Player
    {
		public int id;
        public string name { get; set; }
        public Hand hand { get; set; }
		public bool isPlaying = false; // has already won or quit

		public Player(int id, string name)
		{
			this.id = id;
			this.name = name;
			hand = new Hand();
		}
    }
}
