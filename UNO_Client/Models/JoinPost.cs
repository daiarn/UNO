using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Models
{
    public class JoinPost
    {
        bool success;
        string id;

        public bool Success { get => success; set => success = value; }
        public string Id { get => id; set => id = value; }
    }
}
