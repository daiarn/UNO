﻿using UNO_Server.Models;
using UNO_Server.Utility.Strategy;

namespace UNO_Server.Utility
{
	abstract public class Factory
	{
		public abstract ICardStrategy CreateAction(CardType type);
	}
}