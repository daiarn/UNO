using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNO_Server.Utility.Command
{
	interface ICommand
	{
		void Execute();

		void Undo();
	}
}
