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
        private int RAFScore = 0;
        private int LuftwaffeScore = 0;

        private int rafPlaneCount = 4;
        private bool isBomberPlaneAlive = true;
        private bool isDamAlive = true;
        private int luftwaffePlaneCount = 5;

        private int selectedTileX;
        private int selectedTileY;

        public List<int> sourceX = new List<int>();
        public List<int> sourceY = new List<int>();
        public List<int> targetX = new List<int>();
        public List<int> targetY = new List<int>();

        Tile[,] tiles = new Tile[10, 10]; //2D array to hold tile.
        Panel container = new Panel(); //container panel for tiles
        Panel rulesPanel = new Panel(); //container panel for game rules
        Panel controlPanel = new Panel(); //container panel for controls

        Panel infoPanel = new Panel(); //Panel to hold info about planes
        RichTextBox infoTxtBox = new RichTextBox(); //text box
        RichTextBox moveCount = new RichTextBox(); //move count text box
        Label TeamTurnNotifier = new Label();

        public string planeDataStoredWhenShowingGameRules = "";
        public bool showRules = false;
        public bool turn = true; // boolean to control who's turn it is. If true, brit, if false, axis.

        // Shorthand getter and setter
        public int SelectedTileX { get => selectedTileX; set => selectedTileX = value; }
        public int SelectedTileY { get => selectedTileY; set => selectedTileY = value; }
        public int RAFPlaneCount { get => rafPlaneCount; set => rafPlaneCount = value; }
        public bool IsBomberPlaneAlive { get => isBomberPlaneAlive; set => isBomberPlaneAlive = value; }
        public bool IsDamAlive { get => isDamAlive; set => isDamAlive = value; }
        public int LuftwaffePlaneCount { get => luftwaffePlaneCount; set => luftwaffePlaneCount = value; }
        public bool Turn { get => turn; set => turn = value; }

        public Form_Game()
        {
            InitializeComponent();

            initForm();
            MenuStrip();

            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.FromArgb(255, 55, 98, 72);

            container.BackColor = Color.FromArgb(0, 0, 0, 0);
            container.SetBounds(5, 27, 760, 760);

            int i = 75;

            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    //British 1,0 ; 2,1 ; 1,2; heavy: 1,1

                    tiles[x, y] = new Tile();
                    tiles[x, y].btnTile.SetBounds(x + (x * i), y + (y * i), i, i);

                    //7,6 ; 8,7 ; 9,8 ; 6,7 ; 5,8
                    //tiles[x, y].btnTile.BackColor = Color.Transparent;

                    tiles[x, y].btnTile.BackgroundImageLayout = ImageLayout.Stretch;
                    tiles[x, y].btnTile.Click += new EventHandler(onTileClick);
                    tiles[x, y].btnTile.Text = x + "," + y;

                    container.AutoScroll = true;
                    container.Controls.Add(tiles[x, y].btnTile);
                }
            }

            setupGameBoard();

            container.AutoSize = true;
            this.Controls.Add(container);
            infoPanel_setup();
            controlsUI();
        }

        private void setupGameBoard()
        {
            // Brittish plane setup
            tiles[0, 0].createSpitFire();
            tiles[0, 0].Rotation = 90;
            tiles[1, 1].createSpitFire();
            tiles[1, 1].Rotation = 90;
            tiles[0, 2].createSpitFire();
            tiles[0, 2].Rotation = 90;
            tiles[0, 1].createDamBuster();
            tiles[0, 1].Rotation = 90;

            // Luftwaffe planes setup
            tiles[5, 9].createMeBF109();
            tiles[6, 8].createMeBF109();
            tiles[9, 9].createMeBF109();
            tiles[7, 7].createMeBF109();
            tiles[8, 8].createMeBF109();

            // Create dam
            tiles[9, 2].createDam();

            // Update images according to rotation
            tiles[0, 0].rotateTile();
            tiles[1, 1].rotateTile();
            tiles[0, 2].rotateTile();
            tiles[0, 1].rotateTile();
            tiles[5, 9].rotateTile();
            tiles[6, 8].rotateTile();
            tiles[9, 9].rotateTile();
            tiles[7, 7].rotateTile();
            tiles[8 ,8].rotateTile();
        }

        /**
         * All of then menu strip code packaged in one spot
         * */
        private void MenuStrip()
        {
            //Docks rulesPanel to the right
            rulesPanel.Anchor = AnchorStyles.Right;
            rulesPanel.Visible = false;
            rulesPanel.Height = 40;
            rulesPanel.Width = 20;
            //Docks planeInfoPanel to the right;
            infoPanel.Anchor = AnchorStyles.Left;
            infoPanel.Visible = false;
            infoPanel.Height = 40;
            infoPanel.Width = 20;

            //adds container to form
            this.Controls.Add(rulesPanel);
            this.Controls.Add(infoPanel);

            //Creates new menu strip
            MenuStrip mainMenu = new MenuStrip();

            //MenuStrip settings
            mainMenu.BackColor = Color.FromArgb(255, 55, 98, 72);

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
            infoTxtBox.SetBounds(7, 5, 400, 245);
            infoTxtBox.BackColor = Color.DarkSeaGreen;
            infoTxtBox.ForeColor = Color.White;

            infoPanel.SetBounds(765, 27, 415, 258);
            infoPanel.BackColor = Color.FromArgb(100, 55, 98, 72);

            infoPanel.Controls.Add(infoTxtBox);
            infoPanel.Visible = true;

            Controls.Add(infoPanel);
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
            showRules = false;
            System.Diagnostics.Debug.WriteLine(((Button)sender).Text);

            // Split string by delimeter - https://docs.microsoft.com/en-us/dotnet/api/system.string.split?view=net-6.0 - 07/02/2022
            string coords = ((Button)sender).Text;
            string[] subString = coords.Split(',');

            int x, y;
            x = int.Parse(subString[0]);
            y = int.Parse(subString[1]);

            SelectedTileX = x;
            SelectedTileY = y;

  

            moveCount.Text = (tiles[SelectedTileX, SelectedTileY].Moves).ToString();

            infoTxtBox.Text = "Name: " + tiles[x, y].Name + "\n\nType: " + tiles[x, y].Type + "\n\nHP: " + tiles[x, y].Health + "\n\nAltitude: " + tiles[x, y].Altitude;
        }

        /**
         * EventHandler for rules menuItem, shows the game rules on the right side panel on click.
         */
        private void onRulesClick(object sender, EventArgs e)
        {
            if (showRules == false)
            {
                //Tries opening the file
                try
                {
                    /**
                    * Code adapted from https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file
                    * on 2/2/22 at 21:00
                    */

                    //Reads all the lines in file and stores them in string array lines.
                    string[] lines = System.IO.File.ReadAllLines(@"..\..\Assets\Text Files\rules.txt");
                    infoTxtBox.Clear();
                    //For every line in string array lines
                    foreach (string line in lines)
                    {

                        //Prints line to console
                        System.Diagnostics.Debug.WriteLine("\n" + line);

                        //writes line to text box.
                        planeDataStoredWhenShowingGameRules = infoTxtBox.Text;
                       
                        infoTxtBox.AppendText(line);
                    }
                    // Adapted code ends here
                }

                catch (Exception)
                {
                    //Shows error if file is missing/any other error.
                    MessageBox.Show("Error when opening rules file. Is it no longer located in Assets/TextFiles?", "Error!");
                }
            }
            else
            {
                infoTxtBox.Clear();
                infoTxtBox.Text = planeDataStoredWhenShowingGameRules;
            }
            showRules = !showRules;
        }

        private void Form_Game_Load(object sender, EventArgs e)
        {

        }

        /**
         * Method to control the controls section of the UI
         */
        private void controlsUI()
        {
            Button upButton = new Button();
            Button downButton = new Button();
            Button leftButton = new Button();
            Button rightButton = new Button();
            Button rotateL = new Button();
            Button rotateR = new Button();
            Button aviateButton = new Button();
            Button deviateButton = new Button();

            Button confirmButton = new Button();

            moveCount.ReadOnly = true;
            moveCount.BorderStyle = BorderStyle.FixedSingle;
            moveCount.Font = new Font("Calibri", 25);

            // Text alignment in a rich text box - https://stackoverflow.com/questions/6243350/how-to-align-text-in-richtextbox-c - 09/02/2022
            moveCount.SelectAll();
            moveCount.SelectionAlignment = HorizontalAlignment.Center;
            moveCount.DeselectAll();

            upButton.SetBounds(60, 80, 55, 55);
            downButton.SetBounds(60, 200, 55, 55);
            leftButton.SetBounds(0, 140, 55, 55);
            rightButton.SetBounds(120, 140, 55, 55);
            moveCount.SetBounds(60, 140, 55, 55);
            rotateL.SetBounds(0, 80, 55, 55);
            rotateR.SetBounds(120, 80, 55, 55);
            aviateButton.SetBounds(200, 80, 55, 55);
            deviateButton.SetBounds(200, 140, 55, 55);

            confirmButton.SetBounds(200, 200, 55, 55);

            // Turn Label
            TeamTurnNotifier.Text = "RAF";
            TeamTurnNotifier.Font = new Font("Calibri", 27);
            TeamTurnNotifier.AutoSize = true;
            TeamTurnNotifier.SetBounds(90, 0, 300, 150);

            controlPanel.Controls.Add(TeamTurnNotifier);

            //EventHandlers
            rotateL.Click += new EventHandler(rotateLClick);
            rotateR.Click += new EventHandler(rotateRClick);
            upButton.Click += new EventHandler(upButtonClick);
            downButton.Click += new EventHandler(downButtonClick);
            leftButton.Click += new EventHandler(leftButtonClick);
            rightButton.Click += new EventHandler(rightButtonClick);
            aviateButton.Click += new EventHandler(aviateButtonClick);
            deviateButton.Click += new EventHandler(deviateButtonClick);
            confirmButton.Click += new EventHandler(confirmClick);

            //Directional Keys Arrow Images
            upButton.BackgroundImage = Properties.Resources.upArrow;
            upButton.BackgroundImageLayout = ImageLayout.Stretch;
            
            downButton.BackgroundImage = Properties.Resources.downArrow;
            downButton.BackgroundImageLayout = ImageLayout.Stretch;

            leftButton.BackgroundImage = Properties.Resources.leftArrow;
            leftButton.BackgroundImageLayout = ImageLayout.Stretch;

            rightButton.BackgroundImage = Properties.Resources.rightArrow;
            rightButton.BackgroundImageLayout = ImageLayout.Stretch;

            aviateButton.BackgroundImage = Properties.Resources.elevateArrow;
            aviateButton.BackgroundImageLayout = ImageLayout.Stretch;

            deviateButton.BackgroundImage = Properties.Resources.deviateArrow;
            deviateButton.BackgroundImageLayout = ImageLayout.Stretch;

            rotateL.BackgroundImage = Properties.Resources.rotateLeft;
            rotateL.BackgroundImageLayout = ImageLayout.Stretch;

            rotateR.BackgroundImage = Properties.Resources.rotateRight;
            rotateR.BackgroundImageLayout = ImageLayout.Stretch;

            confirmButton.BackColor = Color.LimeGreen;
            confirmButton.ForeColor = Color.White;
            confirmButton.Text = "Confirm";

            //Adds button to panel
            controlPanel.Controls.Add(rotateL);
            controlPanel.Controls.Add(upButton);
            controlPanel.Controls.Add(downButton);
            controlPanel.Controls.Add(rotateR);
            controlPanel.Controls.Add(leftButton);
            controlPanel.Controls.Add(moveCount);
            controlPanel.Controls.Add(rightButton);
            controlPanel.Controls.Add(aviateButton);
            controlPanel.Controls.Add(deviateButton);
            controlPanel.Controls.Add(confirmButton);

            //Sets controlPanel
            controlPanel.SetBounds(845, 400, 365, 400);
            controlPanel.BackColor = Color.FromArgb(100, 55, 98, 72);


            //Control Panel is visible
            controlPanel.Visible = true;

            //Adds to main form
            Controls.Add(controlPanel);

        }

        /**
         * Event handlers for movement
         */
        private void confirmClick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Turn is " + Turn);
            if (haveAllPlanesHaveMoved())
            {
                Turn = !turn;

                // Reset moves for each plane
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    for (int y = 0; y < tiles.GetLength(1); y++)
                    {
                        tiles[x, y].ResetMoves();
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Not all planes have moved!");
            }

            if (Turn == true)
            {
                if (checkBoard() == true) // Do damage calculation step
                {
                    for (int i = 0; i < sourceX.Count(); i++)
                    {
                        int damage = Compute.damageOutput
                        (
                            tiles[sourceX[i], sourceY[i]].Altitude, // Self altitude
                            (byte)tiles[sourceX[i], sourceY[i]].Type, // Self object type
                            (byte)tiles[sourceX[i], sourceY[i]].AmmoType, // Self ammo type
                            Compute.getDistance(sourceX[i], sourceY[i], targetX[i], targetY[i]), // distance between the two tiles
                            tiles[targetX[i], targetY[i]].Altitude, // Target altitude
                            (byte)tiles[targetX[i], targetY[i]].Type // Target object type
                        );

                        tiles[targetX[i], targetY[i]].Health -= damage; // DAMAGE
                        
                        if (tiles[targetX[i], targetY[i]].Health <= 0) // Destroy target if health reaches 0 or less
                        {
                            tiles[targetX[i], targetY[i]].createEmptyTile();
                        }
                    }
                }

                checkIfPlanesHaveBeenDestroyed();

                // Check if either team has won
                if (checkForWinCondition() != WinCondition.None)
                {
                    Console.WriteLine("Win Condition: " + checkForWinCondition());
                    gameOver gameOverScreen = new gameOver();
                    if (checkForWinCondition() == WinCondition.RAF)
                    {
                        gameOverScreen.gameWon(checkForWinCondition(), RAFScore);
                    }
                    else
                    {
                        gameOverScreen.gameWon(checkForWinCondition(), LuftwaffeScore);
                    }
                }
            }

            if (Turn == true) // Update label
            {
                TeamTurnNotifier.Text = "RAF";
            }
            else
            {
                TeamTurnNotifier.Text = "Luftwaffe";
            }
        }
        
        private void rotateLClick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(SelectedTileX + ", " + SelectedTileY);

            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe)) { 
           
            System.Diagnostics.Debug.WriteLine("\n" + tiles[SelectedTileX, SelectedTileY].Team);
                
            System.Diagnostics.Debug.WriteLine(Turn);



            // Decrement move from tile. Clamp to 0
            if (tiles[SelectedTileX, SelectedTileY].Moves != 0)
            {
                tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                updateMoveCount_txtBox();

                // Subtract from rotation paramater by 90 deg. If 0 then set to 270.
                if (tiles[SelectedTileX, SelectedTileY].Rotation == 0)
                {
                    tiles[SelectedTileX, SelectedTileY].Rotation = 270;
                }
                else
                {
                    tiles[SelectedTileX, SelectedTileY].Rotation -= 90;
                }

                //Apply correct rotation function call
                tiles[SelectedTileX, SelectedTileY].rotateTile();
            }
            else { return; }

            infoTxtBox.Text = "Name: " + tiles[SelectedTileX, SelectedTileY].Name + "\n\nType: " + tiles[SelectedTileX, SelectedTileY].Type + "\n\nHP: " + tiles[SelectedTileX, SelectedTileY].Health + "\n\nAltitude: " + tiles[SelectedTileX, SelectedTileY].Altitude;
        }}
        
        private void rotateRClick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(SelectedTileX + ", " + SelectedTileY);

            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe)) { 

            // Decrement move from tile. Clamp to 0
            if (tiles[SelectedTileX, SelectedTileY].Moves != 0) 
            {
                tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                updateMoveCount_txtBox();
                
                // Add to rotation paramater by 90 deg. If 360 then set to 0.
                if (tiles[SelectedTileX, SelectedTileY].Rotation == 270)
                {
                    tiles[SelectedTileX, SelectedTileY].Rotation = 0;
                }
                else
                {
                    tiles[SelectedTileX, SelectedTileY].Rotation += 90;
                }

                //Apply correct rotation function call
                tiles[SelectedTileX, SelectedTileY].rotateTile();
            }
            else { return; }


            infoTxtBox.Text = "Name: " + tiles[SelectedTileX, SelectedTileY].Name + "\n\nType: " + tiles[SelectedTileX, SelectedTileY].Type + "\n\nHP: " + tiles[SelectedTileX, SelectedTileY].Health + "\n\nAltitude: " + tiles[SelectedTileX, SelectedTileY].Altitude;
        }
            }

        private void upButtonClick(object sender, EventArgs e)
        {

            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe))
            {
                if (selectedTileY != 0)
                {
                    //New instance of tile
                    Tile newTile = new Tile();

                    //New tile is set to current tile minus one (so going up)
                    newTile = tiles[SelectedTileX, (SelectedTileY - 1)];

                    //Decrement move from tile. Clamp to 0
                    if (tiles[SelectedTileX, SelectedTileY].Moves != 0 && SelectedTileY != 0 && tiles[SelectedTileX, SelectedTileY].Rotation == 0 && (newTile.Type == ObjectType.Empty || newTile.Type == ObjectType.Dam))
                    {

                        // Check if plane being piloted is the bomber plane and if its going into the target tile.
                        if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type == ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            tiles[SelectedTileX, SelectedTileY].PutBomberPlaneOnTarget(ref newTile);
                            SelectedTileY--;
                        }
                        // Don't let move to be executed
                        else if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type != ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            return;
                        }
                        else // Otherwise move as normal
                        {
                            //Decrement moves
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            //Calls swap tiles from Tile.
                            tiles[SelectedTileX, SelectedTileY].SwapTiles(ref newTile);
                            SelectedTileY--;
                        }
                    }
                }
            }
        }

        private void downButtonClick(object sender, EventArgs e)
        {
            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe))
            {

                if (selectedTileY != 9)
                {
                    //New instance of tile
                    Tile newTile = new Tile();

                    //New tile is set to current tile minus one (so going up)
                    newTile = tiles[SelectedTileX, (SelectedTileY + 1)];

                    //Decrement move from tile. Clamp to 0
                    if (tiles[SelectedTileX, SelectedTileY].Moves != 0 && SelectedTileY != 9 && tiles[SelectedTileX, SelectedTileY].Rotation == 180 && (newTile.Type == ObjectType.Empty || newTile.Type == ObjectType.Dam))
                    {

                        // Check if plane being piloted is the bomber plane and if its going into the target tile.
                        if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type == ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            tiles[SelectedTileX, SelectedTileY].PutBomberPlaneOnTarget(ref newTile);
                            SelectedTileY++;
                        }
                        // Don't let move to be executed
                        else if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type != ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            return;
                        }
                        else // Otherwise move as normal
                        {
                            //Decrement moves
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            //Calls swap tiles from Tile.
                            tiles[SelectedTileX, SelectedTileY].SwapTiles(ref newTile);
                            SelectedTileY++;
                        }
                    }
                }
            }
        }

        private void leftButtonClick(object sender, EventArgs e)
        {

            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe))
            {

                //Checks if X axis is 0
                if (SelectedTileX != 0)
                {
                    //New instance of tile
                    Tile newTile = new Tile();

                    //New tile is set to current tile minus one (so going up)
                    newTile = tiles[(SelectedTileX - 1), SelectedTileY];


                    //Decrement move from tile. Clamp to 0
                    if (tiles[SelectedTileX, SelectedTileY].Moves != 0 && SelectedTileX != 0 && tiles[SelectedTileX, SelectedTileY].Rotation == 270 && (newTile.Type == ObjectType.Empty || newTile.Type == ObjectType.Dam))
                    {

                        // Check if plane being piloted is the bomber plane and if its going into the target tile.
                        if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type == ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            tiles[SelectedTileX, SelectedTileY].PutBomberPlaneOnTarget(ref newTile);
                            SelectedTileX--;

                        }
                        // Don't let move to be executed
                        else if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type != ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            return;
                        }
                        else // Otherwise move as normal
                        {
                            //Decrement moves
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            //Calls swap tiles from Tile.
                            tiles[SelectedTileX, SelectedTileY].SwapTiles(ref newTile);
                            SelectedTileX--;
                        }
                    }
                }
            }
        }

        private void rightButtonClick(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("\n Before IF" + tiles[SelectedTileX, SelectedTileY].Team);

            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe))
            {

                System.Diagnostics.Debug.WriteLine("\n" + tiles[SelectedTileX, SelectedTileY].Team);

                System.Diagnostics.Debug.WriteLine(Turn);

                if (selectedTileX != 9)
                {
                    //New instance of tile
                    Tile newTile = new Tile();

                    //New tile is set to current tile minus one (so going up)
                    newTile = tiles[(SelectedTileX + 1), SelectedTileY];

                    //Decrement move from tile. Clamp to 0
                    if (tiles[SelectedTileX, SelectedTileY].Moves != 0 && SelectedTileX != 9 && tiles[SelectedTileX, SelectedTileY].Rotation == 90 && newTile.Type == ObjectType.Empty)
                    {
                        // Check if plane being piloted is the bomber plane and if its going into the target tile.
                        if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type == ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            tiles[SelectedTileX, SelectedTileY].PutBomberPlaneOnTarget(ref newTile);
                            SelectedTileX++;
                        }
                        // Don't let move to be executed
                        else if (newTile.Type == ObjectType.Dam && tiles[selectedTileX, selectedTileY].Type != ObjectType.Bomber && tiles[selectedTileX, selectedTileY].Altitude == 1)
                        {
                            return;
                        }
                        else // Otherwise move as normal
                        {
                            //Decrement moves
                            tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                            updateMoveCount_txtBox();

                            //Calls swap tiles from Tile.
                            tiles[SelectedTileX, SelectedTileY].SwapTiles(ref newTile);
                            SelectedTileX++;
                        }

                    }
                }
            }
        }

        private void aviateButtonClick(object sender, EventArgs e)
        {
            
            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe)) { 

                //Decrement move from tile. Clamp to 0
                if (tiles[SelectedTileX, SelectedTileY].Altitude != 5 && tiles[SelectedTileX, SelectedTileY].Moves != 0)
                {
                    //Decrement moves
                    tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                    tiles[SelectedTileX, SelectedTileY].Altitude += 1;
                    updateMoveCount_txtBox();
                    infoTxtBox.Text = "Name: " + tiles[SelectedTileX, SelectedTileY].Name + "\n\nType: " + tiles[SelectedTileX, SelectedTileY].Type + "\n\nHP: " + tiles[SelectedTileX, SelectedTileY].Health + "\n\nAltitude: " + tiles[SelectedTileX, SelectedTileY].Altitude;

                }
            }
            }
        
        private void deviateButtonClick(object sender, EventArgs e)
        {
            
            //Checks if the team matches the plane selection.
            if ((tiles[SelectedTileX, SelectedTileY].Team == Team.RAF && gameLoop() == true) || (gameLoop() == false && tiles[SelectedTileX, SelectedTileY].Team == Team.Luftwaffe)) { 
                //Decrement move from tile. Clamp to 0
                if (tiles[SelectedTileX, SelectedTileY].Altitude != 1  && tiles[SelectedTileX, SelectedTileY].Moves != 0)
                {
                    //Decrement moves
                    tiles[SelectedTileX, SelectedTileY].Moves -= 1;
                    tiles[SelectedTileX, SelectedTileY].Altitude -= 1;
                    updateMoveCount_txtBox();
                    infoTxtBox.Text = "Name: " + tiles[SelectedTileX, SelectedTileY].Name + "\n\nType: " + tiles[SelectedTileX, SelectedTileY].Type + "\n\nHP: " + tiles[SelectedTileX, SelectedTileY].Health + "\n\nAltitude: " + tiles[SelectedTileX, SelectedTileY].Altitude;

                }
        }}

        // Initializes the form with specific properties
        private void initForm()
        {
            this.Height = 825;
            this.Width = 1200;
            this.MaximizeBox = false;
            this.AutoScroll = true;
        }

        // Helper function to update the move counter text box - the text box between the controls
        private void updateMoveCount_txtBox()
        {
            moveCount.Clear();
            moveCount.Text = (tiles[selectedTileX, selectedTileY].Moves).ToString();

            moveCount.SelectAll();
            moveCount.SelectionAlignment = HorizontalAlignment.Center;
            moveCount.DeselectAll();
        }

        // Checks the entire board if a win condition has been met.
        private WinCondition checkForWinCondition()
        {
            int RAFPlaneCount = 0;
            int DamCount = 0;
            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x,y].Type == ObjectType.Bomber)
                    {
                        RAFPlaneCount++;
                    }
                    else if (tiles[x,y].Type == ObjectType.Dam)
                    {
                        DamCount++;
                    }
                }
            }

            if (RAFPlaneCount == 0) // The Luftwaffe win
            {
                return WinCondition.Luftwaffe;
            }
            else if (DamCount == 0) // The British win
            {
                return WinCondition.RAF;
            }
            
            return WinCondition.None;
        }

        // Checks the entire baord if planes or the dam has been destroyed and distribute the points accordingly
        private void checkIfPlanesHaveBeenDestroyed()
        {
            // Loop through the board and check if there are less planes than previously
            int rafCount = 0;
            int luftCount = 0;
            bool isBomberPlaneAlive = false;
            bool isDamAlive = false;
            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y].Team == Team.RAF)
                    {
                        rafCount++;
                    }
                    else if (tiles[x,y].Team == Team.Luftwaffe)
                    {
                        luftCount ++;
                    }
                    // Check if the bomber plane is alive. - Useful for score calculation
                    if (tiles[x, y].Type == ObjectType.Bomber)
                    {
                        isBomberPlaneAlive = true;
                    }
                    if (tiles[x, y].Type == ObjectType.Dam)
                    {
                        isDamAlive = true;
                    }
                }
            }

            if (rafCount < RAFPlaneCount) // Add points to the Luftwaffe
            {
                LuftwaffeScore += 500;
                RAFPlaneCount = rafCount;
            }
            else if (isBomberPlaneAlive != IsBomberPlaneAlive) // Add points to the luftwaffe if the bomber has been destroyed
            {
                LuftwaffePlaneCount += 1000;
                IsBomberPlaneAlive = isBomberPlaneAlive;
            }
            else if (luftCount < luftwaffePlaneCount) // Add points to the RAF
            {
                RAFScore += 500;
                LuftwaffePlaneCount = luftCount;
            }
            else if (isDamAlive != IsDamAlive)
            {
                RAFScore += 1000;
                IsDamAlive = isDamAlive;
            }
        }

        /*
         * Checks board if there will be a damage step needed
         */
        private bool checkBoard()
        {
            int possibleAttacks = 0;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (tiles[x,y].Type != ObjectType.Empty)
                    {
                        if (isPotentialTargetInRange(x, y) == true)
                        {
                            possibleAttacks++;
                        }
                    }
                }
            }

            if (possibleAttacks > 0)
            {
                return true;
            }
            return false;
        }

        private bool haveAllPlanesHaveMoved()
        {
            int notMoved = 0;
            // True - Check for all RAF planes have moved
            if (Turn == true)
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    for (int y = 0; y < tiles.GetLength(1); y++)
                    {
                        if (tiles[x,y].Team == Team.RAF)
                        {
                            if (tiles[x,y].Moves != 0)
                            {
                                notMoved++;
                            }
                        }
                    }
                }
            }
            else // False - Check for all luftwaffe planes have moved
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    for (int y = 0; y < tiles.GetLength(1); y++)
                    {
                        if (tiles[x, y].Team == Team.Luftwaffe)
                        {
                            if (tiles[x, y].Moves != 0)
                            {
                                notMoved++;
                            }
                        }
                    }
                }
            }

            if (notMoved == 0)
            {
                return true;
            }

            return false;
        }

        private bool gameLoop()
        {
            //Checks whose turn it is based on boolean
            if (turn == true)
            {
                //System.Diagnostics.Debug.WriteLine("Team is Britain");
                return true;
            }

            else
            {
                return false;
            }
        }
        // checkBoard helper function
        private bool isPotentialTargetInRange(int x, int y)
        {
            if (tiles[x, y].Rotation == 0)
            {
                return checkY_Up(x, y);
            }
            else if (tiles[x, y].Rotation == 90)
            {
                return checkX_Right(x, y);
            }
            else if (tiles[x, y].Rotation == 180)
            {
                return checkY_Down(x, y);
            }
            else if (tiles[x, y].Rotation == 270)
            {
                return checkX_Left(x, y);
            }
            return false;
        }

        private bool checkY_Up(int x, int y)
        {
            int yUp1 = y - 1;
            int yUp2 = y - 2;
            int yUp3 = y - 3;

            if (yUp1 >= 0)
            {
                if (tiles[x,yUp1].Type != ObjectType.Empty)
                {
                    if (tiles[x, yUp1].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(x);
                        targetY.Add(yUp1);

                        return true;
                    }
                }
            }

            if (yUp3 >= 0)
            {
                if (tiles[x, yUp3].Type != ObjectType.Empty)
                {
                    if (tiles[x, yUp3].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(x);
                        targetY.Add(yUp3);

                        return true;
                    }
                }
            }

            if (tiles[x,y].AmmoType == AmmoType.Heavy)
            {
                if (yUp3 >= 0)
                {
                    if (tiles[x, yUp3].Type != ObjectType.Empty)
                    {
                        if (tiles[x, yUp3].Team != tiles[x, y].Team)
                        {
                            sourceX.Add(x);
                            sourceY.Add(y);
                            targetX.Add(x);
                            targetY.Add(yUp3);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool checkY_Down(int x, int y)
        {
            int yDown1 = y + 1;
            int yDown2 = y + 2;
            int yDown3 = y + 3;

            if (yDown1 <= 9)
            {
                if (tiles[x, yDown1].Type != ObjectType.Empty)
                {
                    if (tiles[x, yDown1].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(x);
                        targetY.Add(yDown1);

                        return true;
                    }
                }
            }

            if (yDown2 <= 9)
            {
                if (tiles[x, yDown2].Type != ObjectType.Empty)
                {
                    if (tiles[x, yDown2].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(x);
                        targetY.Add(yDown2);

                        return true;
                    }
                }
            }

            if (tiles[x, y].AmmoType == AmmoType.Heavy)
            {
                if (yDown3 <= 9)
                {
                    if (tiles[x, yDown3].Type != ObjectType.Empty)
                    {
                        if (tiles[x, yDown3].Team != tiles[x, y].Team)
                        {
                            sourceX.Add(x);
                            sourceY.Add(y);
                            targetX.Add(x);
                            targetY.Add(yDown3);

                            return true;
                        }
                    }
                }
            }
            return false;
        }
    
        private bool checkX_Right(int x, int y)
        {
            int xRight1 = x + 1;
            int xRight2 = x + 2;
            int xRight3 = x + 3;

            if (xRight1 <= 9)
            {
                if (tiles[xRight1, y].Type != ObjectType.Empty)
                {
                    if (tiles[xRight1, y].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(xRight1);
                        targetY.Add(y);

                        return true;
                    }
                }
            }

            if (xRight2 <= 9)
            {
                if (tiles[xRight2, y].Type != ObjectType.Empty)
                {
                    if (tiles[xRight2, y].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(xRight2);
                        targetY.Add(y);

                        return true;
                    }
                }
            }

            if (tiles[x, y].AmmoType == AmmoType.Heavy)
            {
                if (xRight3 <= 9)
                {
                    if (tiles[xRight3, y].Type != ObjectType.Empty)
                    {
                        if (tiles[xRight3, y].Team != tiles[x, y].Team)
                        {
                            sourceX.Add(x);
                            sourceY.Add(y);
                            targetX.Add(xRight3);
                            targetY.Add(y);

                            return true;
                        }
                    }
                }
            }
            return false;
        }
    
        private bool checkX_Left(int x, int y)
        {
            int xLeft1 = x - 1;
            int xLeft2 = x - 2;
            int xLeft3 = x - 3;

            if (xLeft1 <= 0)
            {
                if (tiles[xLeft1, y].Type != ObjectType.Empty)
                {
                    if (tiles[xLeft1, y].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(xLeft1);
                        targetY.Add(y);

                        return true;
                    }
                }
            }

            if (xLeft2 <= 0)
            {
                if (tiles[xLeft2, y].Type != ObjectType.Empty)
                {
                    if (tiles[xLeft2, y].Team != tiles[x, y].Team)
                    {
                        sourceX.Add(x);
                        sourceY.Add(y);
                        targetX.Add(xLeft2);
                        targetY.Add(y);

                        return true;
                    }
                }
            }

            if (tiles[x, y].AmmoType == AmmoType.Heavy)
            {
                if (xLeft3 <= 0)
                {
                    if (tiles[xLeft3, y].Type != ObjectType.Empty)
                    {
                        if (tiles[xLeft3, y].Team != tiles[x, y].Team)
                        {
                            sourceX.Add(x);
                            sourceY.Add(y);
                            targetX.Add(xLeft3);
                            targetY.Add(y);

                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
