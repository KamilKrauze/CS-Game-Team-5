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

using Calculate; // Use the namespace that is in the DLL - Use the computer class to call functions.
using GameSound; // Sound dll - use the SFX class to call functions.

namespace CS_GridGame_Team5
{
    public partial class Form_Game : Form
    {
        public Form_Game()
        {
            InitializeComponent();
            String name = "Spitfire MK2";
            uint health = 3;
            PlaneType type = PlaneType.Fighter;
            int altitude = 3;
            AmmoType ammo = AmmoType.Light;
            Plane spitFireMK2 = new Plane(ref name, ref health, ref type, ref altitude, ref ammo); // Use this for every instance of the plane being used on board
            Console.WriteLine(spitFireMK2.Name);
            Console.WriteLine(Compute.damageOutput(spitFireMK2.Altitude, 3, (byte)spitFireMK2.AmmoType)); // Damage output dll test - Convert to byte type.
            SFX.playSound("FlyBy.wav"); // Just write in the sound file name with extension. - MUST BE WAV FORMAT
        }

        private void Form_Game_Load(object sender, EventArgs e)
        {

        }
    }
}