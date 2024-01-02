using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman_2
{
    public class Pinky : Enemy
    {
        public Pinky(int tileX, int tileY, Tile[,] tileArray) : base(tileX, tileY, tileArray)
        {
            ScatterTargetTile = new Vector2(1, 2);
            type = GhostType.Pinky;

            rectsDown[0] = new Rectangle(170,449,32,32);
            rectsDown[1] = new Rectangle(206,449,32,32);
            rectsDown[2] = new Rectangle(241,449,32,32);
            rectsDown[3] = new Rectangle(276,449,32,32);

            rectsUp[0] = new Rectangle(170,408,32,32);
            rectsUp[1] = new Rectangle(206,408,32,32);
            rectsUp[2] = new Rectangle(241,408,32,32);
            rectsUp[3] = new Rectangle(276,408,32,32);

            rectsLeft[0] = new Rectangle(170,367,32,32);
            rectsLeft[1] = new Rectangle(206,367,32,32);
            rectsLeft[2] = new Rectangle(241,367,32,32);
            rectsLeft[3] = new Rectangle(276,367,32,32);

            rectsRight[0] = new Rectangle(170,328,32,32);
            rectsRight[1] = new Rectangle(206,328,32,32);
            rectsRight[2] = new Rectangle(241, 328, 32, 32);
            rectsRight[3] = new Rectangle(276,328,32,32);

            enemyAnim = new SpriteAnimation(0.08f, rectsDown);
        }

        public override Vector2 getChaseTargetPosition(Vector2 playerTilePos, Dir playerDir, Tile[,] tileArray)
        {
            Vector2 pos = playerTilePos;
            Dir PlayerDir = playerDir;

            if (PlayerDir == Dir.None)
            {
                PlayerDir = playerLastDir;
            }

            switch (PlayerDir)
            {
                case Dir.Right:
                    pos = new Vector2(playerTilePos.X + 4, playerTilePos.Y);
                    playerLastDir = Dir.Right;
                    break;
                case Dir.Left:
                    pos = new Vector2(playerTilePos.X - 4, playerTilePos.Y);
                    playerLastDir = Dir.Left;
                    break;
                case Dir.Down:
                    pos = new Vector2(playerTilePos.X, playerTilePos.Y + 4);
                    playerLastDir = Dir.Down;
                    break;
                case Dir.Up:
                    pos = new Vector2(playerTilePos.X, playerTilePos.Y - 4);
                    playerLastDir = Dir.Up;
                    break;
            }
            if (pos.X < 0 || pos.Y < 0 || pos.X > Controller.numberOfTilesX - 1 || pos.Y > Controller.numberOfTilesY - 1)
            {
                return playerTilePos;
            }
            if (tileArray[(int)pos.X, (int)pos.Y].tileType == Tile.TileType.Wall)
            {
                return playerTilePos;
            }
            return pos;
        }
    }
}