using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman_2
{
    public class Tile
    {
        public enum TileType { 
            None,
            Wall, 
            Ghost, GhostHouse,
            Player, 
            Snack 
        };

        public TileType tileType = TileType.None;
        public bool isEmpty = true;

        Vector2 position;

        public Vector2 Position // get and set the variable
        {
            get
            {
                return position;
            }
        }

        public static int getDistanceBetweenTiles(Vector2 pos1, Vector2 pos2) // using the math rule for calculating the distance between 2 coordinates
        {
            return (int)Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }

        //constructors

        public Tile(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Tile(Vector2 newPosition, TileType newTileType)
        {
            position = newPosition;
            tileType = newTileType;
        }


    }
}
