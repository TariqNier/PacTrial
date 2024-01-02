using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Pacman_2
{
    public class Inky : Enemy
    {
        public Inky(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(25, 29);
            type = GhostType.Inky;

            rectsDown[0] = new Rectangle(16, 622, 32, 32);
            rectsDown[1] = new Rectangle(52, 622, 32, 32);
            rectsDown[2] = new Rectangle(87, 622, 32, 32);
            rectsDown[3] = new Rectangle(122, 622, 32, 32);

            rectsUp[0] = new Rectangle(16, 582, 32, 32);
            rectsUp[1] = new Rectangle(52, 582, 32, 32);
            rectsUp[2] = new Rectangle(87,582,32,32);
            rectsUp[3] = new Rectangle(122,582,32,32);

            rectsLeft[0] = new Rectangle(16, 542, 32, 32);
            rectsLeft[1] = new Rectangle(52, 542, 32, 32);
            rectsLeft[2] = new Rectangle(87,542,32,32);
            rectsLeft[3] = new Rectangle(122,542,32,32);

            rectsRight[0] = new Rectangle(16, 506 , 32, 32);
            rectsRight[1] = new Rectangle(52, 506, 32, 32);
            rectsRight[2]= new Rectangle(87,506,32, 32);
            rectsRight[3] = new Rectangle(122, 506, 32, 32);
        }

        public override Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray, Vector2 blinkyPos)
        {
            Dir PlayerDir = playerDir;
            Vector2 PacmanPos = playerTilePos;
            Vector2 BlinkyPos = blinkyPos;

            if (PlayerDir == Dir.None)
            {
                PlayerDir = playerLastDir;
            }

            Vector2 finalTarget = new Vector2(0, 0);

            switch (PlayerDir)
            {
                case Dir.Down:
                    finalTarget.Y += 2;
                    playerLastDir = Dir.Down;
                    break;
                case Dir.Up:
                    finalTarget.Y -= 2;
                    playerLastDir = Dir.Up;
                    break;
                case Dir.Left:
                    finalTarget.X -= 2;
                    playerLastDir = Dir.Left;
                    break;
                case Dir.Right:
                    finalTarget.X += 2;
                    playerLastDir = Dir.Right;
                    break;
            }


            if (PacmanPos.X < BlinkyPos.X)
            {
                finalTarget.X = BlinkyPos.X - PacmanPos.X;
            }
            else
            {
                finalTarget.X = PacmanPos.X - BlinkyPos.X;
            }

            if (PacmanPos.Y < BlinkyPos.Y)
            {
                finalTarget.Y = BlinkyPos.Y - PacmanPos.Y;
            }
            else
            {
                finalTarget.Y = PacmanPos.Y - BlinkyPos.Y;
            }

            finalTarget *= 2;

            finalTarget.X += currentTile.X;
            finalTarget.Y += currentTile.Y;

            if (finalTarget.X < 0 || finalTarget.Y < 0 || finalTarget.X > Controller.numberOfTilesX - 1 || finalTarget.Y > Controller.numberOfTilesY - 1)
            {
                return playerTilePos;
            }
            if (tileArray[(int)finalTarget.X, (int)finalTarget.Y].tileType == Tile.TileType.Wall)
            {
                return playerTilePos;
            }

            return finalTarget;
        }
    }
}