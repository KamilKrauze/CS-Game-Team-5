using System;

namespace CS_GridGame_Team5
{
    class Plane
    {
        private String name;
        private uint health;
        private PlaneType type;
        private int altitude;
        private AmmoType ammoType;

        public Plane()
        {
            this.name = "";
            this.health = 0;
            this.type = PlaneType.Fighter;
            this.altitude = 1;
            this.ammoType = AmmoType.Light;
        }

        public Plane(ref String name, ref uint health, ref PlaneType type, ref int altitude, ref AmmoType ammoType)
        {
            this.name = name;
            this.health = health;
            this.type = type;
            this.altitude = altitude;
            this.ammoType = ammoType;

            Console.WriteLine(this.name + ", " + this.health + ", "  + this.type + ", " + this.altitude + ", " + this.ammoType); // Debug data
        }

        public string Name { get => name; set => name = value; }
        public uint Health { get => health; set => health = value; }
        public int Altitude { get => altitude; set => altitude = value; }
        internal PlaneType Type { get => type; set => type = value; }
        internal AmmoType AmmoType { get => ammoType; set => ammoType = value; }
    }
}
