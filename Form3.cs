using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CS_GridGame_Team5
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            List<string> highScores = new List<string>;
            // Gets contents for highScores file
            StreamReader s = File.OpenText(@"MyFile.txt"); // Set file name
            string read = null;
            while ((read = s.ReadLine()) != null) // Read from file until done
            {
                highScores.Add(read); // Output text to highscores list
            }
            s.Close();

        }

        private 
    }
}
