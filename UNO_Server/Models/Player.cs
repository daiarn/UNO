using System;

namespace UNO_Server.Models
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
