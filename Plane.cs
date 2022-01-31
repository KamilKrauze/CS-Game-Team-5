using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CS_GridGame_Team5
{
    class Plane
    {
        public const string DamageCalcDLL_fp = @"..\..\DLL Workspace\DamageCalDLL_C++\x64\Debug\DamageCalc.dll"; // Define the damage calculation DLL filepath

        [DllImport(DamageCalcDLL_fp, CallingConvention = CallingConvention.Cdecl)] // Import DLL file into class
        public static extern int calculateDamage(string type, int altitude, string ammoType); // Define function from DLL
        
        [DllImport(DamageCalcDLL_fp, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getVAL(int VAL);

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
            Console.WriteLine(getVAL(altitude));
        }

        public string Name { get => name; set => name = value; }
        public int Altitude { get => altitude; set => altitude = value; }
        internal PlaneType Type { get => type; set => type = value; }
        internal AmmoType AmmoType { get => ammoType; set => ammoType = value; }
    }
}
