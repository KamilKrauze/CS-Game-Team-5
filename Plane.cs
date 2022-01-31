using System;

namespace CS_GridGame_Team5
{
    class Plane
    {
        private String name;
        private PlaneType type;
        private int altitude;
        private AmmoType ammoType;

        public Plane()
        {
            this.name = "";
            this.type = PlaneType.fighter;
            this.altitude = 1;
            this.ammoType = AmmoType.light;
        }

        public Plane(ref String name, ref PlaneType type, ref int altitude, ref AmmoType ammoType)
        {
            this.name = name;
            this.type = type;
            this.altitude = altitude;
            this.ammoType = ammoType;

            Console.WriteLine(name + ", " + type + ", " + altitude + ", " + ammoType); // Debug data
        }

        public string Name { get => name; set => name = value; }
        public int Altitude { get => altitude; set => altitude = value; }
        internal PlaneType Type { get => type; set => type = value; }
        internal AmmoType AmmoType { get => ammoType; set => ammoType = value; }
    }
}
