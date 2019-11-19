using System.Media;

namespace UNO_Client.Adapter
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
