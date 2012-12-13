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

namespace SimpleThing
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState oldKeyboardState;
        MouseState oldMouseState;

        Room currentRoom;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Screen.X = 800;
            Screen.Y = 480;

            oldKeyboardState = new KeyboardState();
            oldMouseState = new MouseState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ImageHandler.playerSprites = new Dictionary<double, Texture2D>();

            ImageHandler.playerSprites.Add(0, Content.Load<Texture2D>("playerRight"));
            ImageHandler.playerSprites.Add(Math.PI, Content.Load<Texture2D>("playerLeft"));
            ImageHandler.key = Content.Load<Texture2D>("key");
            ImageHandler.ground = Content.Load<Texture2D>("ground");
            ImageHandler.wall = Content.Load<Texture2D>("wall");
            ImageHandler.spike = Content.Load<Texture2D>("spike");
            ImageHandler.dead = Content.Load<Texture2D>("dead");
            ImageHandler.door = Content.Load<Texture2D>("door");

            currentRoom = new First();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            if (newKeyboardState.IsKeyDown(Keys.R))
            { currentRoom = new First(); }

            if (Collision.Test(currentRoom.key, currentRoom.door))
            { currentRoom = new Second(); }

            currentRoom.Update(newKeyboardState, oldKeyboardState, newMouseState, oldMouseState);

            oldKeyboardState = newKeyboardState;
            oldMouseState = newMouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            currentRoom.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
