using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Interpreter
{
    public class Context
    {
        public string Input;
        public int Output;

        public Context(string input)
        {
            Input = input;
        }
    }
}
