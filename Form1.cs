using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Calculate; // Use the namespace that is in the DLL

namespace CS_GridGame_Team5
{
    public partial class Form_Game : Form
    {
        public Form_Game()
        {
            InitializeComponent();
            String name = "Spitfire MK2";
            PlaneType type = PlaneType.fighter;
            int altitude = 3;
            AmmoType ammo = AmmoType.light;
            Plane spitFireMK2 = new Plane(ref name, ref type, ref altitude, ref ammo); // Use this for every instance of the plane being used on board
            Console.WriteLine(spitFireMK2.Name);
            Console.WriteLine(Compute.damageOutput(spitFireMK2.Altitude, 3, (byte)spitFireMK2.AmmoType));
        }

        private void Form_Game_Load(object sender, EventArgs e)
        {

        }
    }
}