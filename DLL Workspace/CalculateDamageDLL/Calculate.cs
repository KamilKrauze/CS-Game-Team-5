using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculate
{
    public class Compute
    {
        public static int damageOutput(int ownAltitude, int targetAltitude, byte ammoType)
        {
            int altitudeModifier = 0;
            int ammoTypeModifier = 0;

            if (ammoType == 0) { ammoTypeModifier = 1; }
            else if (ammoType == 0) { ammoTypeModifier = 2; }

            if (ownAltitude > targetAltitude || ownAltitude < targetAltitude)
            {
                altitudeModifier = ownAltitude - targetAltitude;
            }

            if ( (int)Math.Round(((altitudeModifier + ammoTypeModifier) / 0.5)) >= 0 )
            {
                return (int)Math.Round(((altitudeModifier + ammoTypeModifier) / 0.5));
            }
            else
            {
                return 1;
            }
        }
    }
}