using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Pacman_2
{
    public class Clyde:Enemy
    {
        public Clyde(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(2, 29);
            type = GhostType.Clyde;

            rectsDown[0] = new Rectangle(170,506,32,32);
            rectsDown[1] = new Rectangle(206,506,32,32);
            rectsDown[2] = new Rectangle(241,506,32,32);
            rectsDown[3] = new Rectangle(276, 506, 32, 32);

            rectsUp[0] = new Rectangle(170, 582, 32, 32);
            rectsUp[1] = new Rectangle(206, 582, 32, 32);
            rectsUp[2] = new Rectangle(241, 582, 32, 32);
            rectsUp[3] = new Rectangle(276, 582, 32, 32);

            rectsLeft[0] = new Rectangle(170, 542, 32, 32);
            rectsLeft[1] = new Rectangle(206, 542, 32, 32);
            rectsLeft[2] = new Rectangle(241, 542, 32, 32);
            rectsLeft[3] = new Rectangle(276, 542, 32, 32);

            rectsRight[0] = new Rectangle(170, 506, 32, 32);
            rectsRight[1] = new Rectangle(206, 542, 32, 32);
            rectsRight[2] = new Rectangle(241, 542, 32, 32);
            rectsRight[3] = new Rectangle(276, 542, 32, 32);



        }

        public override Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray)
        {
            if (Tile.getDistanceBetweenTiles(playerTilePos, currentTile) > 8)
            {
                return playerTilePos;
            }
            return ScatterTargetTile;
        }
    }
}
