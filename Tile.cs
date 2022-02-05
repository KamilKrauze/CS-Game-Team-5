﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_GridGame_Team5
{
    public class Tile : Object
    {
        public Panel panel = new Panel();
        public Tile() // Empty Tile
        {
            this.Name = "Empty";
            this.Health = 0;
            this.Moves = 0;
            this.Altitude = 0;
            this.AmmoType = AmmoType.None;
            this.Type = ObjectType.Empty;
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

            this.Name = tile.Name;
            this.Health = tile.Health;
            this.Moves = tile.Moves;
            this.Altitude = tile.Altitude;
            this.AmmoType = tile.AmmoType;
            this.Type = tile.Type;

            tile.Name = temp.Name;
            tile.Health = temp.Health;
            tile.Moves = temp.Moves;
            tile.Altitude = temp.Altitude;
            tile.AmmoType = temp.AmmoType;
            tile.Type = temp.Type;
        }
    }

}
