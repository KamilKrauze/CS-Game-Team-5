using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_GridGame_Team5
{
    public class Tile : Object
    {

        public Button btnTile = new Button();

        private int rotation;
        private Team team;

        public Tile() // Empty Tile
        {
            this.Name = "Empty";
            this.Health = 0;
            this.Moves = 0;
            this.Altitude = 0;
            this.AmmoType = AmmoType.None;
            this.Type = ObjectType.Empty;
            this.rotation = 0;
            this.team = Team.None;
        }

        // Creates a tile that is the Spitfire MK2
        public void createSpitFire()
        {
            this.Name = "Spitfire MK2";
            this.Health = 3;
            this.Moves = 3;
            this.Altitude = 2;
            this.AmmoType = AmmoType.Light;
            this.Type = ObjectType.Fighter;
            this.rotation = 0;
            this.team = Team.RAF;
            this.btnTile.BackgroundImage = Properties.Resources.SpitfireMK2_R0;
        }

        // Creates a tile that is the Dam Buster. aka - Avro Lancaster
        public void createDamBuster()
        {
            this.Name = "Avro Lancaster / Dam Buster";
            this.Health = 5;
            this.Moves = 2;
            this.Altitude = 2;
            this.AmmoType = AmmoType.Light;
            this.Type = ObjectType.Bomber;
            this.rotation = 0;
            this.team = Team.RAF;
            this.btnTile.BackgroundImage = Properties.Resources.SpitfireMK2_R180;
        }

        // Creates a tile that is the Messerschmitt BF 109
        public void createMeBF109()
        {
            this.Name = "Messerschmitt Bf 109";
            this.Health = 3;
            this.Moves = 3;
            this.Altitude = 2;
            this.AmmoType = AmmoType.Light;
            this.Type = ObjectType.Fighter;
            this.rotation = 0;
            this.team = Team.Luftwaffe;
            this.btnTile.BackgroundImage = Properties.Resources.MeBF109_R0;
        }

        // Transfers data to another Tile object
        // Pass in by reference - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref - 05/02/2022
        public void SwapTiles(ref Tile tile)
        {
            Tile temp = new Tile(); //  Temporary variable
            temp.Name = this.Name;
            temp.Health = this.Health;
            temp.Moves = this.Moves;
            temp.Altitude = this.Altitude;
            temp.AmmoType = this.AmmoType;
            temp.Type = this.Type;
            temp.Rotation = this.Rotation;
            temp.btnTile.BackgroundImage = this.btnTile.BackgroundImage;
            
            this.Name = tile.Name;
            this.Health = tile.Health;
            this.Moves = tile.Moves;
            this.Altitude = tile.Altitude;
            this.AmmoType = tile.AmmoType;
            this.Type = tile.Type;
            this.Rotation = tile.Rotation;
            this.btnTile.BackgroundImage = tile.btnTile.BackgroundImage;

            tile.Name = temp.Name;
            tile.Health = temp.Health;
            tile.Moves = temp.Moves;
            tile.Altitude = temp.Altitude;
            tile.AmmoType = temp.AmmoType;
            tile.Type = temp.Type;
            tile.Rotation = temp.Rotation;
            tile.btnTile.BackgroundImage = temp.btnTile.BackgroundImage;
        }

        public int Rotation { get => rotation; set => rotation = value; }
        public Team Team { get => team; set => team = value; }
        
        // Rotates the tile appropriately to the rotation parameter of the tile
        public void rotateTile()
        {
            // Brittish Planes
            if (this.Name == "Spitfire MK2" || this.Name == "Avro Lancaster / Dam Buster")
            {
                if (this.Rotation == 0)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.SpitfireMK2_R0;
                }
                else if (this.Rotation == 90)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.SpitfireMK2_R90;
                }
                else if (this.Rotation == 180)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.SpitfireMK2_R180;
                }
                else if (this.Rotation == 270)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.SpitfireMK2_R270;
                }
            }
            // German Planes
            else if (this.Name == "Messerschmitt Bf 109")
            {
                if (this.Rotation == 0)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.MeBF109_R0;
                }
                else if (this.Rotation == 90)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.MeBF109_R90;
                }
                else if (this.Rotation == 180)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.MeBF109_R180;
                }
                else if (this.Rotation == 270)
                {
                    this.btnTile.BackgroundImage = Properties.Resources.MeBF109_R270;
                }
            }
            else
            {
                return;
            }
        }
    }

}
