using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Other
{
    class SoundAdapter
    {
        public void turnOnSoundEffect()
        {
            using (var soundPlayer = new SoundPlayer("..//..//Sounds//fireworks.wav"))
            {
                soundPlayer.Play();
            }
        }
    }
}
