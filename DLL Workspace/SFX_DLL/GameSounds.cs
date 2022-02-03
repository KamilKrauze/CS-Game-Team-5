using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSound
{
    public class SFX
    {
        public static void playSound(string soundFileName)
        {
            // Play sound from filepath - https://stackoverflow.com/questions/3502311/how-to-play-a-sound-in-c-net - 02/02/2022
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\..\Assets\SFX\Plane\"+soundFileName);
            player.Play();
        }
    }
}
