using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_GridGame_Team5
{
    public partial class Form_Game : Form
    {
        Panel[,] plane = new Panel[5, 5];
        Panel container = new Panel(); //container panel for planes
        Panel rulesPanel = new Panel(); //container panel for game rules
        public Form_Game()
        {
            InitializeComponent();

            int i = 50;

            for (int x = 0; x < plane.GetLength(1); x++)
            {
                for (int y = 0; y < plane.GetLength(1); y++)
                {
                    plane[x, y] = new Panel();
                    plane[x, y].SetBounds(x + (x * i), y + (y * i), i, i); // Dynamic bounds scaling based on the 'i' factor

                    plane[x, y].BorderStyle = BorderStyle.None; // Disable border

                    plane[x, y].BackColor = Color.Transparent; // Helps with the transparency of the image

                    plane[x, y].BackgroundImage = Properties.Resources.SpitfireMK2; // The image name accessed from the resources section
                    plane[x, y].BackgroundImageLayout = ImageLayout.Stretch; // Proper image scaling proportional to the object size.

                    container.Controls.Add(plane[x, y]);
                }
            }

          

            //AutoSizes container & rulesPanel
            container.AutoSize = true;
            rulesPanel.AutoSize = true;



            //Docks container to the Bottom
            container.Dock = DockStyle.Fill;

            //Docks rulesPanel to the right
            rulesPanel.Dock = DockStyle.Right;
            rulesPanel.Visible = false;

            //adds container to form
            this.Controls.Add(container);
            this.Controls.Add(rulesPanel);

            //Creates new menu strip
            MenuStrip mainMenu = new MenuStrip();

            //Creates new toolStripMenuItems
            ToolStripMenuItem about = new ToolStripMenuItem("About");
            ToolStripMenuItem rules = new ToolStripMenuItem("Game Rules");

            //Adds onClick event listener to about menu item
            about.Click += new System.EventHandler(this.onAboutClick);

            //Adds onClick event listener to rules menu item
            rules.Click += new System.EventHandler(this.onRulesClick);

            //Adds them to mainMenu
            mainMenu.Items.Add(about);
            mainMenu.Items.Add(rules);

            mainMenu.Dock = DockStyle.Top;

            //Adds mainMenu to form
            this.Controls.Add(mainMenu);
        }

        private void Form_Game_Load(object sender, EventArgs e)
        {

        }

        /**
         * EventHandler for about menu item, shows a message box on click.
         * */
        private void onAboutClick(object sender, EventArgs e)
        {
            
            //Shows info box.
            MessageBox.Show("Dambusters, based off Warhammer Aeronautica, implemented by Caitlin Ridge-Sykes, Kamil Krauze, and Euan West", "About");

        }

        /**
         * EventHandler for rules menuItem, shows the gameRules panel on click.
         */
        private void onRulesClick(object sender, EventArgs e)
        {

          
    

            //Tries opening the file
            try

            { 

                //Creates textBox
                RichTextBox textBox = new RichTextBox();

                //Settings for textBox.
                textBox.Multiline = true;
                textBox.ReadOnly = true;
                textBox.AutoSize = true;
                textBox.Height = 400;
                textBox.Width = 200;
                textBox.WordWrap = true;

                /**
                * Code adapted from https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file
                * on 2/2/22 at 21:00
                */

                //Reads all the lines in file and stores them in string array lines.
                string[] lines = System.IO.File.ReadAllLines("../../Assets/Text Files/rules.txt");



                //For every line in string array lines
                foreach (string line in lines)
               {
                
                    //Prints line to console
                    System.Diagnostics.Debug.WriteLine("\n" + line);

                    //writes line to text box.
                    textBox.AppendText(line);
                
               
              
                }

                //Adapted code ends here.

                rulesPanel.Controls.Add(textBox);

                //Toggles visibility of rulesPanel
                rulesPanel.Visible = !rulesPanel.Visible;


            }

            catch (Exception)
            {

                //Shows error if file is missing/any other error.
                MessageBox.Show("Error when opening rules file. Is it no longer located in Assets/TextFiles?", "Error!");
            }



        }
    }
}
