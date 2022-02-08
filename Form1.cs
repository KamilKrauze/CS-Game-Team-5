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
        Tile[,] tiles = new Tile[10,10]; //2D array to hold tile.
        Panel container = new Panel(); //container panel for tiles
        Panel rulesPanel = new Panel(); //container panel for game rules
        
        Panel infoPanel = new Panel();
        RichTextBox infoTxtBox = new RichTextBox();

        public Form_Game()
        {
            InitializeComponent();

            resizeForm();

            //this.BackgroundImage = Properties.Resources.NightClouds_2048x2048;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            container.BackColor = Color.Transparent;
            //container.AutoSize = true;
            container.SetBounds(5, 25, 760, 760);

            int i = 75;

            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y] = new Tile();
                    tiles[x, y].btnTile.SetBounds(x + (x * i), y + (y * i), i, i);

                    tiles[x, y].btnTile.BackColor = Color.FromArgb(0, 0, 0, 0);
                    //tiles[x, y].btnTile.BackgroundImage = Properties.Resources.SpitfireMK2_512;
                    tiles[x, y].btnTile.BackgroundImageLayout = ImageLayout.Stretch;

                    tiles[x, y].btnTile.Text = x + "," + y;
                    tiles[x, y].btnTile.ForeColor = Color.White;

                    tiles[x, y].btnTile.Click += new EventHandler(onTileClick);

                    container.AutoScroll = true;
                    container.Controls.Add(tiles[x, y].btnTile);
                }
            }

            Controls.Add(container);
            infoPanel.SetBounds(765, 25, 415, 758);
            infoPanel.BackColor = Color.Black;

            infoPanel.Controls.Add(infoTxtBox);
            infoPanel.Visible = true;

            Controls.Add(infoPanel);

            //MenuStrip();
        }

        /**
         * All of then menu strip code packaged in one spot
         * */
        private void MenuStrip()
        {
            //AutoSizes rulesPanel
            //rulesPanel.AutoSize = true;

            //Docks rulesPanel to the right
            rulesPanel.Anchor = AnchorStyles.Right;
            rulesPanel.Visible = false;
            rulesPanel.Height = 400;
            rulesPanel.Width = 200;
            //Docks planeInfoPanel to the right;
            infoPanel.Anchor = AnchorStyles.Left;
            infoPanel.Visible = false;
            infoPanel.Height = 400;
            infoPanel.Width = 200;

            //adds container to form
            this.Controls.Add(rulesPanel);
            this.Controls.Add(infoPanel);

            //Creates new menu strip
            MenuStrip mainMenu = new MenuStrip();

            //MenuStrip settings
            mainMenu.BackColor = Color.Black;

            //Creates new toolStripMenuItems
            ToolStripMenuItem about = new ToolStripMenuItem("About");
            ToolStripMenuItem rules = new ToolStripMenuItem("Game Rules");

            //ToolStripMenuItems settings
            about.ForeColor = Color.White;
            about.Font = new Font("Calibri", 12, FontStyle.Bold);

            rules.ForeColor = Color.White;
            rules.Font = new Font("Calibri", 12, FontStyle.Bold);


            //Adds onClick event listener to about menu item
            about.Click += new System.EventHandler(this.onAboutClick);

            //Adds onClick event listener to rules menu item
            rules.Click += new System.EventHandler(this.onRulesClick);


            //Adds them to mainMenu
            mainMenu.Items.Add(about);
            mainMenu.Items.Add(rules);

            mainMenu.SetBounds(0, 0, 1200, 20);

            //Adds mainMenu to form
            this.Controls.Add(mainMenu);
        }
        
        /**
         * Sets up the info text box properties on function call.
         * */
        private void infoPanel_setup()
        {
            infoTxtBox.Multiline = true;
            infoTxtBox.ReadOnly = true;
            infoTxtBox.WordWrap = true;
            infoTxtBox.BorderStyle = BorderStyle.FixedSingle;

            infoTxtBox.Font = new Font("Calibri", 15);
            infoTxtBox.Clear();
            infoTxtBox.SetBounds(0, 0, 400, 850);
            
            infoPanel.SetBounds(300, 0, 400, 850);
            infoPanel.BackColor = Color.AliceBlue;

            infoPanel.Controls.Add(infoTxtBox);
            infoPanel.Visible = true;

            this.Controls.Add(infoPanel);
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
         * A method to manage on tile click
         */
        private void onTileClick(object sender, EventArgs e)
            {
                System.Diagnostics.Debug.WriteLine(((Button)sender).Text);

                // Split string by delimeter - https://docs.microsoft.com/en-us/dotnet/api/system.string.split?view=net-6.0 - 07/02/2022
                string coords = ((Button)sender).Text;
                string[] subString = coords.Split(',');
            
                int x,  y;
                x = int.Parse(subString[0]);
                y = int.Parse(subString[1]);

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
                //textBox.AutoSize = true;
            
                textBox.WordWrap = true;

                textBox.Font = new Font("Calibri", 12);


                /**
                * Code adapted from https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file
                * on 2/2/22 at 21:00
                */

                //Reads all the lines in file and stores them in string array lines.
                string[] lines = System.IO.File.ReadAllLines(@"..\..\Assets\Text Files\rules.txt");

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

        private void Form_Game_Load(object sender, EventArgs e)
        {

        }

        private void resizeForm()
        {
            this.Height = 825;
            this.Width = 1200;
        }
    }
}
