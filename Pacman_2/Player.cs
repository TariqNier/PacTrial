using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace Pacman_2
{
    public class Player
    {
        private Vector2 position;
        private Dir direction = Dir.Right;
        private Dir nextDirection = Dir.None;
        private int speed = 150;
        public static int radiusOffSet = 19;
        public static Rectangle[] deathAnimRect = new Rectangle[17];
      
        public static Rectangle[] rectsDown = new Rectangle[6];
        public static Rectangle[] rectsUp = new Rectangle[6];
        public static Rectangle[] rectsLeft = new Rectangle[6];
        public static Rectangle[] rectsRight = new Rectangle[6];

        bool canMove = true;
        float canMoveTimer = 0;
        float TimerThreshold = 0.2f;

        Vector2 previousTile;
        Vector2 currentTile;

        private SpriteAnimation playerAnim;
        private int extraLives = 0;

        public Player(int tileX, int tileY, Tile[,] tileArray)
        {
            position = tileArray[tileX, tileY].Position;
            position.X += 14;
            currentTile = new Vector2(tileX, tileY);
            previousTile = new Vector2(tileX - 1, tileY);

            rectsDown[0] = new Rectangle(487,183,31,31);
            rectsDown[1] = new Rectangle(520,183,31,31);
            rectsDown[2] = new Rectangle(553, 183, 31, 31);
            rectsDown[3] = new Rectangle(584, 183, 31, 31);
            rectsDown[4] = new Rectangle(616, 183, 31, 31);
            rectsDown[5] = new Rectangle(646, 183, 31, 31);

            rectsUp[0] = new Rectangle(487,144,31,31);
            rectsUp[1] = new Rectangle(521,144,31,31);
            rectsUp[2] = new Rectangle(553 ,144,31,31);
            rectsUp[3] = new Rectangle(584, 144, 31, 31 );
            rectsUp[4] = new Rectangle(616, 144, 31, 31 );
            rectsUp[5] = new Rectangle(648, 144, 31, 31 );

            rectsLeft[0] = new Rectangle(487,103,31,31);
            rectsLeft[1] = new Rectangle(523,103,31,31);
            rectsLeft[2] = new Rectangle(556,103,31,31);
            rectsLeft[3] = new Rectangle(558, 103, 31, 31);
            rectsLeft[4] = new Rectangle(619, 103, 31, 31);
            rectsLeft[5] = new Rectangle(649, 103, 31, 31);

            rectsRight[0] = new Rectangle(488,63,31,31);
            rectsRight[1] = new Rectangle(521,63,31,31);
            rectsRight[2] = new Rectangle(554,63,31,31);
            rectsRight[3] = new Rectangle(585, 63, 31, 31);
            rectsRight[4] = new Rectangle(615, 63, 31, 31);
            rectsRight[5] = new Rectangle(646, 63, 31, 31);

            deathAnimRect[0] = new Rectangle(488,246,31,31);
            deathAnimRect[1] = new Rectangle(521,246,31,31);
            deathAnimRect[2] = new Rectangle(555,246,31,31);
            deathAnimRect[3] = new Rectangle(586,246,31,31);
            deathAnimRect[4] = new Rectangle(617,246,31,31);
            deathAnimRect[5] = new Rectangle(646,246,31,31);
            deathAnimRect[6] = new Rectangle(673,246,31,31);
            deathAnimRect[7] = new Rectangle(704,258,31,31);
            deathAnimRect[8] = new Rectangle(725,258,31,31);
            deathAnimRect[9] = new Rectangle(742,258,31,31);
            deathAnimRect[10] = new Rectangle(759,262,31,31);
            deathAnimRect[11] = new Rectangle(772, 262, 31, 31);
            deathAnimRect[12] = new Rectangle(783, 262, 31, 31);
            deathAnimRect[13] = new Rectangle(797, 256, 31, 31);
            deathAnimRect[14] = new Rectangle(816, 250, 31, 31);
            deathAnimRect[15] = new Rectangle(845, 251, 31, 31);
            deathAnimRect[16] = new Rectangle(875, 251, 31, 31);

            playerAnim = new SpriteAnimation(0.08f, rectsRight, 2);
        }

        public int ExtraLives
        {
            get { return extraLives; }
            set { extraLives = value; }
        }

        public Vector2 CurrentTile
        {
            get { return currentTile; }
            set { currentTile = value; }
        }

        public Dir Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public SpriteAnimation PlayerAnim
        {
            get { return playerAnim; }
        }

        public Vector2 PreviousTile
        {
            get { return previousTile; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void setX(float newX)
        {
            position.X = newX;
        }

        public void setY(float newY)
        {
            position.Y = newY;
        }

        public void Update(GameTime gameTime, Tile[,] tileArray)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!canMove)
            {
                canMoveTimer += dt;
                if (canMoveTimer >= TimerThreshold)
                {
                    canMove = true;
                    canMoveTimer = 0;
                }
            }
            playerAnim.Update(gameTime);

            if (kState.IsKeyDown(Keys.D))
            {
                nextDirection = Dir.Right;
            }
            if (kState.IsKeyDown(Keys.A))
            {
                nextDirection = Dir.Left;
            }
            if (kState.IsKeyDown(Keys.W))
            {
                nextDirection = Dir.Up;
            }
            if (kState.IsKeyDown(Keys.S))
            {
                nextDirection = Dir.Down;
            }

            if (canMove)
            {
                switch (nextDirection)
                {
                    case Dir.Right:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            position.Y = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y + 1;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Left:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            position.Y = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.Y + 1;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Down:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            position.X = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X + 2;
                            nextDirection = Dir.None;
                        }
                        break;
                    case Dir.Up:
                        if (Game1.gameController.isNextTileAvailable(nextDirection, currentTile))
                        {
                            canMove = false;
                            direction = nextDirection;
                            position.X = tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X + 2;
                            nextDirection = Dir.None;
                        }
                        break;
                }
            }

            if (!Game1.gameController.isNextTileAvailable(direction, currentTile))
                direction = Dir.None;

            switch (direction)
            {
                case Dir.Right:
                    if (Game1.gameController.isNextTileAvailable(Dir.Right, currentTile))
                    {
                        position.X += speed * dt;
                        playerAnim.setSourceRects(rectsRight);
                    }
                    break;
                case Dir.Left:
                    if (Game1.gameController.isNextTileAvailable(Dir.Left, currentTile))
                    {
                        position.X -= speed * dt;
                        playerAnim.setSourceRects(rectsLeft);
                    }
                    break;
                case Dir.Down:
                    if (Game1.gameController.isNextTileAvailable(Dir.Down, currentTile))
                    {
                        position.Y += speed * dt;
                        playerAnim.setSourceRects(rectsDown);
                    }
                    break;
                case Dir.Up:
                    if (Game1.gameController.isNextTileAvailable(Dir.Up, currentTile))
                    {
                        position.Y -= speed * dt;
                        playerAnim.setSourceRects(rectsUp);
                    }
                    break;
                case Dir.None:
                    Vector2 p = tileArray[(int)currentTile.X, (int)currentTile.Y].Position;
                    position = new Vector2(p.X + 2, p.Y + 1);
                    Sounds.munchInstance.Stop();
                    break;
            }
        }

        public void eatSnack(int listPosition)
        {
            Game1.score += Game1.gameController.snackList[listPosition].scoreGain;

            if (Game1.gameController.snackList[listPosition].snackType == Snack.SnackType.Big)
            {
                Game1.gameController.eatenBigSnack = true;
                Sounds.eat_fruit.Play();
            }

            Game1.gameController.snackList.RemoveAt(listPosition);
            Sounds.munchInstance.Play();
        }

        public void Draw(SpriteBatch spriteBatch, SpriteSheet spriteSheet)
        {
            playerAnim.Draw(spriteBatch, spriteSheet, new Vector2(position.X - radiusOffSet / 2, position.Y - radiusOffSet / 2 + 1));
        }

        public int checkForTeleportPos(Tile[,] tileArray)
        {
            if (new int[2] { (int)currentTile.X, (int)currentTile.Y }.SequenceEqual(new int[2] { 0, 14 }))
            {
                if (position.X < -30)
                {
                    return 1;
                }
            }
            else if (new int[2] { (int)currentTile.X, (int)currentTile.Y }.SequenceEqual(new int[2] { Controller.numberOfTilesX - 1, 14 }))
            {
                if (position.X > tileArray[(int)currentTile.X, (int)currentTile.Y].Position.X + 30)
                {
                    return 2;
                }
            }
            return 0;
        }

        public void teleport(Vector2 pos, Vector2 tilePos)
        {
            position = pos;
            previousTile = currentTile;
            currentTile = tilePos;
        }

        public void updatePlayerTilePosition(Tile[,] tileArray)
        {
            tileArray[(int)previousTile.X, (int)previousTile.Y].tileType = Tile.TileType.None;
            tileArray[(int)currentTile.X, (int)currentTile.Y].tileType = Tile.TileType.Player;

            if (checkForTeleportPos(tileArray) == 1)
            {
                if (direction == Dir.Left)
                    teleport(new Vector2(Game1.windowWidth + 30, position.Y), new Vector2(Controller.numberOfTilesX - 1, 14));
            }
            else if (checkForTeleportPos(tileArray) == 2)
            {
                if (direction == Dir.Right)
                    teleport(new Vector2(-30, position.Y), new Vector2(0, 14));
            }

            for (int x = 0; x < tileArray.GetLength(0); x++)
            {
                for (int y = 0; y < tileArray.GetLength(1); y++)
                {
                    if (Game1.gameController.checkTileType(new Vector2(x, y), Tile.TileType.Player))
                    {
                        int snackListPos = Game1.gameController.findSnackListPosition(tileArray[x, y].Position);
                        if (snackListPos != -1)
                        {
                            eatSnack(snackListPos);
                        }
                    }

                    float tilePosX = tileArray[x, y].Position.X;
                    float tilePosY = tileArray[x, y].Position.Y;

                    float nextTilePosX = tileArray[x, y].Position.X + Controller.tileWidth;
                    float nextTilePosY = tileArray[x, y].Position.Y + Controller.tileHeight;

                    float pacmanPosOffSetX = radiusOffSet / 2;
                    float pacmanPosOffSetY = radiusOffSet / 2;

                    switch (direction)
                    {
                        case Dir.Right:
                            nextTilePosX = tileArray[x, y].Position.X + Controller.tileWidth;
                            break;
                        case Dir.Left:
                            nextTilePosX = tileArray[x, y].Position.X - Controller.tileWidth;
                            pacmanPosOffSetX *= -1;
                            break;
                        case Dir.Down:
                            nextTilePosY = tileArray[x, y].Position.Y + Controller.tileHeight;
                            break;
                        case Dir.Up:
                            nextTilePosY = tileArray[x, y].Position.Y - Controller.tileHeight;
                            pacmanPosOffSetY *= -1;
                            break;
                    }

                    float pacmanPosX = position.X + pacmanPosOffSetX;
                    float pacmanPosY = position.Y + pacmanPosOffSetY;

                    if (direction == Dir.Right || direction == Dir.Down)
                    {
                        if (pacmanPosX >= tilePosX && pacmanPosX < nextTilePosX)
                        {
                            if (pacmanPosY >= tilePosY && pacmanPosY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new Vector2(x, y);
                                if (Game1.gameController.checkTileType(currentTile, Tile.TileType.None))
                                {
                                    Sounds.munchInstance.Stop();
                                }
                            }
                        }
                    }
                    else if (direction == Dir.Left)
                    {
                        if (pacmanPosX <= tilePosX && pacmanPosX > nextTilePosX)
                        {
                            if (pacmanPosY >= tilePosY && pacmanPosY < nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new Vector2(x, y);
                                if (Game1.gameController.checkTileType(currentTile, Tile.TileType.None))
                                {
                                    Sounds.munchInstance.Stop();
                                }
                            }
                        }
                    }
                    else if (direction == Dir.Up)
                    {
                        if (pacmanPosX >= tilePosX && pacmanPosX < nextTilePosX)
                        {
                            if (pacmanPosY <= tilePosY && pacmanPosY > nextTilePosY)
                            {
                                previousTile = currentTile;
                                currentTile = new Vector2(x, y);
                                if (Game1.gameController.checkTileType(currentTile, Tile.TileType.None))
                                {
                                    Sounds.munchInstance.Stop();
                                }
                            }
                        }
                    }
                }
            }
        }

        public void debugPacmanPosition(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.debuggingDot, position, Color.White);
        }
    }
}
