﻿using System;

namespace Calculate
{
    public class Compute
    {
        // Takes in two integer values that are the altitudes of the plane that is shooting and the target plane, and the ammo type being fired.
        public static int damageOutput(int ownAltitude, int targetAltitude, byte ammoType)
        {
            int altitudeModifier = 0;
            int ammoTypeModifier = 1;

            if (ammoType == 0) { ammoTypeModifier = 1; }
            else if (ammoType == 1) { ammoTypeModifier = 2; }

            if (ownAltitude > targetAltitude || ownAltitude < targetAltitude)
            {
                altitudeModifier = ownAltitude - targetAltitude;
            }

            // IF potential damage output <= 0 - MISSES SHOTS
            if ((int)Math.Round(((altitudeModifier + ammoTypeModifier) / 0.5)) <= 0)
            {
                return 0;
            }

            // OTHERWISE randomsize the damage output
            // IF output is 0 - MISSES SHOTS
            Random rand = new Random(); // Random number from a range of numbers. - https://stackoverflow.com/questions/3975290/produce-a-random-number-in-a-range-using-c-sharp - 01/02/2022
            int randInt = rand.Next(1, (int)Math.Round(((altitudeModifier + ammoTypeModifier) / 0.5))); // Can potentially do damage however can still miss.
            Console.WriteLine("Randomising..");
            return randInt;
        }
    }
}