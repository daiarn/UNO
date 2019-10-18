using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
    public class Player
	{
		public Guid id;
        public string name { get; set; }
		public Hand hand { get; set; }
		public bool isPlaying = false; // has won or quit

		public Player(string name)
		{
			this.id = Guid.NewGuid();
			this.name = name;
			hand = new Hand();
		}
	}
}
