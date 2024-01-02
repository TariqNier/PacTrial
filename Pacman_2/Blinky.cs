using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman_2
{
    public class Blinky:Enemy
    {

        public Blinky(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(26, 2);
            type = GhostType.Blinky;

            rectsDown[0] = new Rectangle(16,449,32,32);
            rectsDown[1] = new Rectangle(52,449,32,32);
            rectsDown[0] = new Rectangle(87, 449, 32, 32);
            rectsDown[1] = new Rectangle(122, 449, 32, 32);


            rectsUp[0] = new Rectangle(16,410,32,32);
            rectsUp[1] = new Rectangle(52,410,32,32);
            rectsUp[2] = new Rectangle(87, 409, 32, 32);
            rectsUp[3] = new Rectangle(122, 409, 32, 32);

            rectsLeft[0] = new Rectangle(16,368,32,32);
            rectsLeft[1] = new Rectangle(52,367,32,32);
            rectsLeft[2] = new Rectangle(86, 367, 32, 32);
            rectsLeft[3] = new Rectangle(122, 367, 32, 32);

            rectsRight[0] = new Rectangle(16, 328, 32, 32);
            rectsRight[1] = new Rectangle(52, 328, 32, 32);
            rectsRight[2] = new Rectangle(87, 328, 32, 32);
            rectsRight[3] = new Rectangle(122, 328, 32, 32);






            enemyAnim = new SpriteAnimation(0.08f, rectsLeft);
        }
    }
}
