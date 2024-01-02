using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Net.NetworkInformation;

namespace Pacman_2
{
    public enum Dir
    {
        Down,
        Up,
        Left,
        Right,
        None
    }

    public class Game1 : Game
    {
        public static Controller gameController;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Texture2D GeneralSprites1;
        public static Texture2D GeneralSprites;
        public static Texture2D logo;
        public static Texture2D EMPTY_MAZE;
        public static Texture2D MAZE3;
        public static Texture2D EMPTY_MAZE3;

        public static Texture2D debuggingDot;
        public static Texture2D debugLineX;
        public static Texture2D debugLineY;
        public static Texture2D playerDebugLineX;
        public static Texture2D playerDebugLineY;
        public static Texture2D pathfindingDebugLineX;
        public static Texture2D pathfindingDebugLineY;

        public static SpriteSheet spriteSheet1; // for general sprites1
        public static SpriteSheet spriteSheet2; // for general sprites
        public static SpriteSheet spriteSheet3; // for logo
        public static SpriteSheet spriteSheet4; // for maze

        public static int scoreOffSet = 27;
        public static int windowHeight = 744 + scoreOffSet;
        public static int windowWidth = 672;

        public static float gamePauseTimer;
        public static float gameStartSongLength;

        public static Text text;

        Rectangle backgroundRect = new Rectangle(0, 0, 672, 744); // changes screen

        Inky inky;
        Blinky blinky;
        Clyde clyde;
        Pinky pinky;

        Player Pacman;

        public static SpriteAnimation pacmanDeathAnimation;

        public static bool hasPassedInitialSong = false;
        public static bool hasPauseJustEnded;

        public static int score;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Sounds.credit = Content.Load<SoundEffect>("sounds/credit");
            Sounds.death_1 = Content.Load<SoundEffect>("sounds/death_1");
            Sounds.death_2 = Content.Load<SoundEffect>("sounds/death_2");
            Sounds.eat_fruit = Content.Load<SoundEffect>("sounds/eat_fruit");
            Sounds.eat_ghost = Content.Load<SoundEffect>("sounds/eat_ghost");
            Sounds.extend = Content.Load<SoundEffect>("sounds/extend");
            Sounds.game_start = Content.Load<SoundEffect>("sounds/game_start");
            Sounds.intermission = Content.Load<SoundEffect>("sounds/intermission");

            Sounds.munch = Content.Load<SoundEffect>("sounds/munch");
            Sounds.munchInstance = Sounds.munch.CreateInstance();
            Sounds.munchInstance.Volume = 0.35f;
            Sounds.munchInstance.IsLooped = true;

            Sounds.power_pellet = Content.Load<SoundEffect>("sounds/power_pellet");
            Sounds.power_pellet_instance = Sounds.power_pellet.CreateInstance();
            Sounds.power_pellet_instance.IsLooped = true;

            Sounds.retreating = Content.Load<SoundEffect>("sounds/retreating");
            Sounds.retreatingInstance = Sounds.retreating.CreateInstance();
            Sounds.retreatingInstance.IsLooped = true;

            Sounds.siren_1 = Content.Load<SoundEffect>("sounds/siren_1");
            Sounds.siren_1_instance = Sounds.siren_1.CreateInstance();
            Sounds.siren_1_instance.Volume = 0.8f;
            Sounds.siren_1_instance.IsLooped = true;

            Sounds.siren_2 = Content.Load<SoundEffect>("sounds/siren_2");
            Sounds.siren_3 = Content.Load<SoundEffect>("sounds/siren_3");
            Sounds.siren_4 = Content.Load<SoundEffect>("sounds/siren_4");
            Sounds.siren_5 = Content.Load<SoundEffect>("sounds/siren_5");

            GeneralSprites1 = Content.Load<Texture2D>("GeneralSprites1");
            GeneralSprites = Content.Load<Texture2D>("GeneralSprites");

            EMPTY_MAZE3 = Content.Load<Texture2D>("EMPTY_MAZE 3");
            logo = Content.Load<Texture2D>("logo");
            debuggingDot = Content.Load<Texture2D>("debuggingDot"); // done 
            debugLineX = Content.Load<Texture2D>("debugLineX"); // done
            debugLineY = Content.Load<Texture2D>("debugLineY"); // done
            playerDebugLineX = Content.Load<Texture2D>("playerDebugLineX"); // done
            playerDebugLineY = Content.Load<Texture2D>("playerDebugLineY"); // done
            pathfindingDebugLineX = Content.Load<Texture2D>("pathfindingDebugLineX"); // done
            pathfindingDebugLineY = Content.Load<Texture2D>("pathfindingDebugLineY"); // done

            spriteSheet1 = new SpriteSheet(GeneralSprites1);
            spriteSheet2 = new SpriteSheet(GeneralSprites);
            spriteSheet3 = new SpriteSheet(logo);
            spriteSheet4 = new SpriteSheet(EMPTY_MAZE3);

            Menu.setPacmanLogo = Content.Load<Texture2D>("logo");
            Menu.setBasicFont = Content.Load<SpriteFont>("simpleFont");
            GameOver.setBasicFont = Content.Load<SpriteFont>("simpleFont");

            text = new Text(new SpriteSheet(Content.Load<Texture2D>("TextSprites")));

            gameController = new Controller();
            gameController.CreateGrid();

            inky = new Inky(11, 14, gameController.tileArray);
            blinky = new Blinky(13, 11, gameController.tileArray);
            pinky = new Pinky(13, 14, gameController.tileArray);
            clyde = new Clyde(15, 14, gameController.tileArray);

            Pacman = new Player(13, 23, gameController.tileArray);

            pacmanDeathAnimation = new SpriteAnimation(0.278f, Player.deathAnimRect, 0, false, false);

            gameStartSongLength = 4.23f;
            gamePauseTimer += gameStartSongLength;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (gameController.gameState == Controller.GameState.GameOver)
            {
                GameOver.Update();
                base.Update(gameTime);
                return;
            }

            if (gameController.gameState == Controller.GameState.Menu)
            {
                Menu.Update(gameTime);
                base.Update(gameTime);
                return;
            }

            // checks for game over
            if (Pacman.ExtraLives < 0 && !pacmanDeathAnimation.IsPlaying)
            {
                gameController.GameOver(inky, blinky, pinky, clyde, Pacman);
            }

            // checks if game is paused, if true returns
            if (gamePauseTimer > 0)
            {
                gamePauseTimer -= dt;
                hasPassedInitialSong = true;

                pacmanDeathAnimation.Update(gameTime);

                Sounds.siren_1_instance.Stop();
                hasPauseJustEnded = true;

                base.Update(gameTime);
                return;
            }

            if (hasPauseJustEnded)
            {
                Sounds.siren_1_instance.Play();
                hasPauseJustEnded = false;
            }

            Pacman.updatePlayerTilePosition(gameController.tileArray);
            Pacman.Update(gameTime, gameController.tileArray);
            gameController.updateGhosts(inky, blinky, pinky, clyde, gameTime, Pacman, blinky.CurrentTile);
            if (gameController.startPacmanDeathAnim)
            {
                gameController.startPacmanDeathAnim = false;
                pacmanDeathAnimation.start();
            }

            if (gameController.snackList.Count == 0)
            {
                gameController.Win(inky, blinky, pinky, clyde, Pacman);
                gamePauseTimer = 3f;
                base.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (gameController.gameState == Controller.GameState.Normal)
            {

                spriteSheet4.drawSprite(_spriteBatch, backgroundRect, new Vector2(0, scoreOffSet)); // WHAT DRAWS THE MAZE
                text.draw(_spriteBatch, "score - " + score, new Vector2(3, 3), 24, Text.Color.White);
                if (Pacman.ExtraLives != -1)
                    text.draw(_spriteBatch, "lives " + Pacman.ExtraLives, new Vector2(500, 3), 24, Text.Color.White);
                else
                    text.draw(_spriteBatch, "lives 4", new Vector2(500, 3), 24, Text.Color.White);

                foreach (Snack snack in gameController.snackList)
                {
                    snack.Draw(_spriteBatch);
                }
                if (!pacmanDeathAnimation.IsPlaying)
                    Pacman.Draw(_spriteBatch, spriteSheet2); // what draws the player
                if (hasPassedInitialSong || score == 0)
                    if (!pacmanDeathAnimation.IsPlaying)
                        gameController.drawGhosts(inky, blinky, pinky, clyde, _spriteBatch, spriteSheet2); // what draws the ghosts

                pacmanDeathAnimation.Draw(_spriteBatch, spriteSheet2, gameController.pacmanDeathPosition); // what draws the death animation

                //gameController.drawGridDebugger(_spriteBatch);

                //gameController.drawPathFindingDebugger(_spriteBatch, inky.PathToPacMan);
                //gameController.drawPathFindingDebugger(_spriteBatch, blinky.PathToPacMan);
                // gameController.drawPathFindingDebugger(_spriteBatch, pinky.PathToPacMan);
                //gameController.drawPathFindingDebugger(_spriteBatch, clyde.PathToPacMan);

                // gameController.drawPacmanGridDebugger(_spriteBatch);
                //Pacman.debugPacmanPosition(_spriteBatch);
            }

            else if (gameController.gameState == Controller.GameState.GameOver)
            {
                GameOver.Draw(_spriteBatch, text);
            }

            else if (gameController.gameState == Controller.GameState.Menu)
            {
                Menu.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
