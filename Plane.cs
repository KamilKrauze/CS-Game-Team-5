using System;

namespace CS_GridGame_Team5
{
    class Plane
    {
        private String name;
        private uint health;
        private uint moves;
        private PlaneType type;
        private int altitude;
        private AmmoType ammoType;

        public Plane()
        {
            this.name = "";
            this.health = 0;
            this.moves = 0;
            this.type = PlaneType.Fighter;
            this.altitude = 1;
            this.ammoType = AmmoType.Light;
        }

        public Plane(in String name, in uint health, in uint moves, in PlaneType type, in int altitude, in AmmoType ammoType)
        {
            this.name = name;
            this.health = health;
            this.moves = moves;
            this.type = type;
            this.altitude = altitude;
            this.ammoType = ammoType;

            Console.WriteLine(this.name + ", " + this.health + ", " + this.moves + ", "  + this.type + ", " + this.altitude + ", " + this.ammoType); // Debug data
        }

        public string Name { get => name; set => name = value; }
        public uint Health { get => health; set => health = value; }
        public uint Moves { get => moves; set => moves = value; }
        public int Altitude { get => altitude; set => altitude = value; }
        internal PlaneType Type { get => type; set => type = value; }
        internal AmmoType AmmoType { get => ammoType; set => ammoType = value; }
    }
}
