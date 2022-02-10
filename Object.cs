using System;

namespace CS_GridGame_Team5
{
    public class Object
    {
        private String name;
        private uint health;
        private uint moves;
        private ObjectType type;
        private int altitude;
        private AmmoType ammoType;

        public Object()
        {
            this.name = "";
            this.health = 0;
            this.moves = 0;
            this.type = ObjectType.Fighter;
            this.altitude = 1;
            this.ammoType = AmmoType.Light;
        }

        // 'in' keyword. Makes parameter passed in read only - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier - 05/02/2022
        public Object(in string name, in uint health, in uint moves, in ObjectType type, in int altitude, in AmmoType ammoType)
        {
            this.name = name;
            this.health = health;
            this.moves = moves;
            this.type = type;
            this.altitude = altitude;
            this.ammoType = ammoType;

            Console.WriteLine(this.name + ", " + this.health + ", " + this.moves + ", "  + this.type + ", " + this.altitude + ", " + this.ammoType); // Debug data
        }

        // Short hand method of writing getter and setter functions
        // Example: plane.Name = "Spitfire MK2" would set the value of the class member
        // Example: string name = plane.Name would act as a setter
        public string Name { get => name; set => name = value; }
        public uint Health { get => health; set => health = value; }
        public uint Moves { get => moves; set => moves = value; }
        public int Altitude { get => altitude; set => altitude = value; }
        internal ObjectType Type { get => type; set => type = value; }
        internal AmmoType AmmoType { get => ammoType; set => ammoType = value; }
    }
}
