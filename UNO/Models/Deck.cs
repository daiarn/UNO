using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.Models
{
	public class Deck
	{
		private List<Card> cards { get; set; }

		public void add(Card card)
		{
			cards.Add(card);
		}

		public void shuffle()
		{
			var arr = cards.ToArray();
			var rand = new Random();

			for (int i = arr.Length - 1; i > 0; i--)
			{
				int j = rand.Next(i + 1);

				var temp = arr[j];
				arr[i] = arr[j];
				arr[j] = temp;
			}

			cards = arr.ToList();
		}
    

    public void RemoveActionAndWildCards()
    {
        //remove cards
    }

    public void RemoveWildAndNumberCards()
    {
        //remove cards
    }
	}
}
