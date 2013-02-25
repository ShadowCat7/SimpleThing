using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.IO;

namespace SimpleThing
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;

        private Room currentRoom;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Screen.X = 800;
            Screen.Y = 480;

            oldKeyboardState = new KeyboardState();
            oldMouseState = new MouseState();
        }
        protected override void Initialize()
        { base.Initialize(); }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ImageHandler.playerStand = new SpriteSheet(Content.Load<Texture2D>("playerStand"), 4);
            ImageHandler.playerShoot = new SpriteSheet(Content.Load<Texture2D>("playerShoot"), 4);
            ImageHandler.playerUp = new SpriteSheet(Content.Load<Texture2D>("playerUp"), 4);
            ImageHandler.playerUpShoot = new SpriteSheet(Content.Load<Texture2D>("playerUpShoot"), 4);
            ImageHandler.playerWalk = new SpriteSheet(Content.Load<Texture2D>("playerWalk"), 8);
            ImageHandler.playerWalkShoot = new SpriteSheet(Content.Load<Texture2D>("playerWalkShoot"), 8);
            ImageHandler.playerWalkUp = new SpriteSheet(Content.Load<Texture2D>("playerWalkUp"), 8);
            ImageHandler.playerWalkUpShoot = new SpriteSheet(Content.Load<Texture2D>("playerWalkUpShoot"), 8);
            ImageHandler.playerAir = new SpriteSheet(Content.Load<Texture2D>("playerAir"), 4);
            ImageHandler.playerAirShoot = new SpriteSheet(Content.Load<Texture2D>("playerAirShoot"), 4);
            ImageHandler.playerAirUp = new SpriteSheet(Content.Load<Texture2D>("playerAirUp"), 4);
            ImageHandler.playerAirUpShoot = new SpriteSheet(Content.Load<Texture2D>("playerAirUpShoot"), 4);
            ImageHandler.playerAirDown = new SpriteSheet(Content.Load<Texture2D>("playerAirDown"), 4);
            ImageHandler.playerAirDownShoot = new SpriteSheet(Content.Load<Texture2D>("playerAirDownShoot"), 4);

            ImageHandler.wall = Content.Load<Texture2D>("wall");
            ImageHandler.bullet = Content.Load<Texture2D>("bullet");
            ImageHandler.ball = Content.Load<Texture2D>("ball");

            currentRoom = new GameRoom(1600, 960);
        }
        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            currentRoom.update(gameTime, newKeyboardState, oldKeyboardState, newMouseState, oldMouseState);

            oldKeyboardState = newKeyboardState;
            oldMouseState = newMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            currentRoom.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
