using System;

namespace Calculate
{
    public class Compute
    {
        // Takes in two integer values that are the altitudes of the plane that is shooting and the target plane, and the ammo type being fired.
        public static int damageOutput(int selfAltitude, byte selfObjectType,  byte ammoType, int range, int targetAltitude, byte targetObjectType)
        {
            int altitudeModifier = 0;
            int rangeModifier = 0;
            int ammoModifier = 0;
            float objectDamageModifier = 0;

            // Check for altitude and determine altitude modifier.
            if (selfAltitude != targetAltitude)
            {
                altitudeModifier = selfAltitude - targetAltitude;
            }

            // Check the range and determine rangeModifier
            if (range == 1) // One tile away from source.
            {
                rangeModifier = 3;
            }
            else if (range == 2) // Two tiles away from source.
            {
                rangeModifier = 2;
            }
            else if (range == 3) // Three tiles away from source.
            {
                rangeModifier = 1;
            }

            // Check ammo type shot from source plane and determine modifier.
            if (ammoType == 2) // Light rounds
            {
                ammoModifier = 1;
            }
            else if (ammoType == 3) // Heavy rounds
            {
                ammoModifier = 2;
            }

            // Check object type being shot at.
            if (targetObjectType == 2) // Fighter plane
            {
                objectDamageModifier = 1f;
            }
            else if(targetObjectType == 3) // Bomber plane
            {
                objectDamageModifier = 0.75f;
            }
            else if(targetObjectType == 4 && selfObjectType == 3) // Dam - Only can damage dam if the source plane is a bomber
            {
                if (selfAltitude == 1)
                {
                    return 100; // Hit
                }
                return 0; // Miss
            }

            if (altitudeModifier == -4) // Misses due to target plane being too high above
            {
                return 0;
            }

            // Round number to nearest integer - https://stackoverflow.com/questions/8844674/how-to-round-to-the-nearest-whole-number-in-c-sharp - 10/02/2022
            int damagePotential = (int)Math.Round((altitudeModifier + rangeModifier) * (1/(Math.Pow(ammoModifier, objectDamageModifier))));

            // Randomise number in range - https://stackoverflow.com/questions/3975290/produce-a-random-number-in-a-range-using-c-sharp - 10/02/2022
            Random random = new Random();
            int damageOutput = random.Next(0, damagePotential);

            return damageOutput;
        }
    }
}