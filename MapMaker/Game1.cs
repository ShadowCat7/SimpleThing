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

namespace MapMaker
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Interface> interfaces;
        KeyboardState oldKeyboardState;
        MouseState oldMouseState;

        Texture2D background;

        int sizeX;
        int sizeY;
        int onScreenX;
        int onScreenY;

        string dimension;
        bool buttonClicked;
        bool choosingSize;
        string size;

        Entity player;
        Entity door;
        Entity key;

        List<Entity> entityList;
        Entity entityChosen;
        Camera camera;

        Texture2D grid;
        SpriteFont spriteFont;

        bool saveError;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            oldKeyboardState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();

            Save.saverSetup();

            sizeX = 800;
            sizeY = 480;
            entityList = new List<Entity>();
            interfaces = new List<Interface>();
            camera = new Camera(400, 240);
            buttonClicked = false;
            choosingSize = false;
            size = "";

            saveError = false;
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

            grid = Content.Load<Texture2D>("Menu/grid");
            spriteFont = Content.Load<SpriteFont>("Menu/SpriteFont1");
            background = Content.Load<Texture2D>("Super Kills TF2");

            ImageHandler.playerSprites = new Dictionary<double, Texture2D>();

            ImageHandler.playerSprites.Add(0, Content.Load<Texture2D>("playerRight"));
            ImageHandler.playerSprites.Add(Math.PI, Content.Load<Texture2D>("playerLeft"));
            ImageHandler.key = Content.Load<Texture2D>("key");
            ImageHandler.wall = Content.Load<Texture2D>("wall");
            ImageHandler.spike = Content.Load<Texture2D>("spike");
            ImageHandler.dead = Content.Load<Texture2D>("dead");
            ImageHandler.door = Content.Load<Texture2D>("door");

            ImageHandler.delete = Content.Load<Texture2D>("Menu/delete");

            interfaces.Add(new Interface(0, 0, "player", Content.Load<Texture2D>("Menu/playerbutton")));
            interfaces.Add(new Interface(64, 0, "door", Content.Load<Texture2D>("Menu/doorbutton")));
            interfaces.Add(new Interface(0, 64, "key", Content.Load<Texture2D>("Menu/keybutton")));
            interfaces.Add(new Interface(64, 64, "spike", Content.Load<Texture2D>("Menu/spikebutton")));
            interfaces.Add(new Interface(0, 128, "wall", Content.Load<Texture2D>("Menu/wallbutton")));
            interfaces.Add(new Interface(64, 128, "delete", Content.Load<Texture2D>("Menu/deletebutton")));
            interfaces.Add(new Interface(0, 128 + 64, "xsize", Content.Load<Texture2D>("Menu/xmenu")));
            interfaces.Add(new Interface(0, 128 + 128, "ysize", Content.Load<Texture2D>("Menu/ymenu")));
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
            if (this.IsActive)
            {
                MouseState newMouseState = Mouse.GetState();
                KeyboardState newKeyboardState = Keyboard.GetState();

                if (player == null || door == null || key == null)
                { saveError = true; }
                else
                { saveError = false; }

                if (newKeyboardState.IsKeyDown(Keys.LeftControl) && newKeyboardState.IsKeyDown(Keys.S))
                {
                    if (saveError)
                    { }
                    else
                    {
                        Save.saver.ShowDialog();

                        string filePath = Save.saver.FileName;
                        System.IO.File.Delete(filePath);

                        if (filePath != "")
                        {
                            System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
                            writer.AutoFlush = true;

                            writer.WriteLine(sizeX.ToString());
                            writer.WriteLine(sizeY.ToString());
                            writer.WriteLine(player.roomX.ToString());
                            writer.WriteLine(player.roomY.ToString());
                            writer.WriteLine(door.roomX.ToString());
                            writer.WriteLine(door.roomY.ToString());
                            writer.WriteLine(key.roomX.ToString());
                            writer.WriteLine(key.roomY.ToString());

                            for (int i = 0; i < entityList.Count; i++)
                            {
                                writer.WriteLine(entityList[i].roomX.ToString());
                                writer.WriteLine(entityList[i].roomY.ToString());
                                writer.WriteLine(entityList[i].isDeath.ToString());
                                writer.WriteLine(entityList[i].isSolid.ToString());
                                writer.WriteLine(entityList[i].isStatic.ToString());
                                writer.WriteLine(entityList[i].name.ToString());
                            }

                            writer.WriteLine("*");

                            writer.Close();
                        }
                    }
                }

                camera.Update(newKeyboardState, sizeX, sizeY);
                onScreenX = camera.roomX - 400;
                onScreenY = camera.roomY - 240;

                if (newMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (buttonClicked)
                    {
                        bool isOpen = true;
                        if (newMouseState.X > 0 && newMouseState.X < 800 && newMouseState.Y > 0 && newMouseState.Y < 480)
                        {
                            for (int i = 0; i < interfaces.Count; i++)
                            {
                                if (newMouseState.X > interfaces[i].screenX && newMouseState.X < interfaces[i].screenX + 63)
                                {
                                    if (newMouseState.Y > interfaces[i].screenY && newMouseState.Y < interfaces[i].screenY + 63)
                                    { isOpen = false; }
                                }
                            }
                            if (isOpen)
                            {
                                if (entityChosen.name == "delete")
                                { }
                                else
                                {
                                    entityChosen.roomX = ((newMouseState.X + onScreenX) / 32) * 32;
                                    entityChosen.roomY = ((newMouseState.Y + onScreenY) / 32) * 32;

                                    if (entityChosen.name == "player")
                                    { player = new Entity(entityChosen.roomX, entityChosen.roomY, "player", false, true, true, ImageHandler.playerSprites[0]); }
                                    else if (entityChosen.name == "door")
                                    { door = new Entity(entityChosen.roomX, entityChosen.roomY, "door", false, false, true, ImageHandler.door); }
                                    else if (entityChosen.name == "key")
                                    { key = new Entity(entityChosen.roomX, entityChosen.roomY, "key", false, false, false, ImageHandler.key); }
                                    else
                                    {
                                        Entity tempEntity = new Entity();

                                        if (entityChosen.name == "spike")
                                        { tempEntity = new Entity(entityChosen.roomX, entityChosen.roomY, "spike", true, true, true, ImageHandler.spike); }
                                        if (entityChosen.name == "wall")
                                        { tempEntity = new Entity(entityChosen.roomX, entityChosen.roomY, "wall", false, true, true, ImageHandler.wall); }

                                        entityList.Add(tempEntity);
                                    }
                                }
                                for (int i = 0; i < entityList.Count; i++)
                                {
                                    if (newMouseState.X > entityList[i].screenX && newMouseState.X < entityList[i].screenX + 31)
                                    {
                                        if (newMouseState.Y > entityList[i].screenY && newMouseState.Y < entityList[i].screenY + 31)
                                        { entityList.Remove(entityList[i]); }
                                    }
                                }
                            }
                        }
                    }

                    if (oldMouseState.LeftButton != ButtonState.Pressed)
                    {
                        for (int i = 0; i < interfaces.Count; i++)
                        {
                            if (newMouseState.X > interfaces[i].screenX && newMouseState.X < interfaces[i].screenX + 63)
                            {
                                if (newMouseState.Y > interfaces[i].screenY && newMouseState.Y < interfaces[i].screenY + 63)
                                {
                                    buttonClicked = true;
                                    if (interfaces[i].entityName == "player")
                                    { entityChosen = new Entity(newMouseState.X, newMouseState.Y, "player", false, true, true, ImageHandler.playerSprites[0]); }
                                    if (interfaces[i].entityName == "door")
                                    { entityChosen = new Entity(newMouseState.X, newMouseState.Y, "door", false, false, true, ImageHandler.door); }
                                    if (interfaces[i].entityName == "key")
                                    { entityChosen = new Entity(newMouseState.X, newMouseState.Y, "key", false, false, false, ImageHandler.key); }
                                    if (interfaces[i].entityName == "spike")
                                    { entityChosen = new Entity(newMouseState.X, newMouseState.Y, "spike", true, true, true, ImageHandler.spike); }
                                    if (interfaces[i].entityName == "wall")
                                    { entityChosen = new Entity(newMouseState.X, newMouseState.Y, "wall", false, true, true, ImageHandler.wall); }

                                    if (interfaces[i].entityName == "delete")
                                    { entityChosen = new Entity(newMouseState.X, newMouseState.Y, "delete", false, false, true, ImageHandler.delete); }

                                    if (interfaces[i].entityName == "xsize")
                                    {
                                        choosingSize = true;
                                        dimension = "X";
                                    }
                                    if (interfaces[i].entityName == "ysize")
                                    {
                                        choosingSize = true;
                                        dimension = "Y";
                                    }
                                }
                            }
                        }
                    }
                }

                if (choosingSize)
                {
                    if ((newKeyboardState.IsKeyDown(Keys.D1) || newKeyboardState.IsKeyDown(Keys.NumPad1)) && !(oldKeyboardState.IsKeyDown(Keys.D1)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad1)))
                    { size += '1'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D2) || newKeyboardState.IsKeyDown(Keys.NumPad2)) && !(oldKeyboardState.IsKeyDown(Keys.D2)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad2)))
                    { size += '2'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D3) || newKeyboardState.IsKeyDown(Keys.NumPad3)) && !(oldKeyboardState.IsKeyDown(Keys.D3)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad3)))
                    { size += '3'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D4) || newKeyboardState.IsKeyDown(Keys.NumPad4)) && !(oldKeyboardState.IsKeyDown(Keys.D4)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad4)))
                    { size += '4'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D5) || newKeyboardState.IsKeyDown(Keys.NumPad5)) && !(oldKeyboardState.IsKeyDown(Keys.D5)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad5)))
                    { size += '5'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D6) || newKeyboardState.IsKeyDown(Keys.NumPad6)) && !(oldKeyboardState.IsKeyDown(Keys.D6)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad6)))
                    { size += '6'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D7) || newKeyboardState.IsKeyDown(Keys.NumPad7)) && !(oldKeyboardState.IsKeyDown(Keys.D7)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad7)))
                    { size += '7'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D8) || newKeyboardState.IsKeyDown(Keys.NumPad8)) && !(oldKeyboardState.IsKeyDown(Keys.D8)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad8)))
                    { size += '8'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D9) || newKeyboardState.IsKeyDown(Keys.NumPad9)) && !(oldKeyboardState.IsKeyDown(Keys.D9)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad9)))
                    { size += '9'; }
                    if ((newKeyboardState.IsKeyDown(Keys.D0) || newKeyboardState.IsKeyDown(Keys.NumPad0)) && !(oldKeyboardState.IsKeyDown(Keys.D0)
                        || oldKeyboardState.IsKeyDown(Keys.NumPad0)))
                    { size += '0'; }
                    if (newKeyboardState.IsKeyDown(Keys.Back) && !oldKeyboardState.IsKeyDown(Keys.Back))
                    {
                        string tempString = "";
                        for (int i = 0; i < size.Length - 1; i++)
                        { tempString += size[i]; }
                        size = tempString;
                    }
                    if (newKeyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        if (dimension == "X")
                        { sizeX = Convert.ToInt32(size); }
                        if (dimension == "Y")
                        { sizeY = Convert.ToInt32(size); }
                        choosingSize = false;
                        buttonClicked = false;
                        size = "";
                    }
                }

                if (buttonClicked && !choosingSize)
                {
                    entityChosen.screenX = newMouseState.X - 16;
                    entityChosen.screenY = newMouseState.Y - 16;
                }

                for (int i = 0; i < entityList.Count; i++)
                {
                    entityList[i].screenX = entityList[i].roomX - onScreenX;
                    entityList[i].screenY = entityList[i].roomY - onScreenY;
                }
                if (player != null)
                {
                    player.screenX = player.roomX - onScreenX;
                    player.screenY = player.roomY - onScreenY;
                }
                if (door != null)
                {
                    door.screenX = door.roomX - onScreenX;
                    door.screenY = door.roomY - onScreenY;
                }
                if (key != null)
                {
                    key.screenX = key.roomX - onScreenX;
                    key.screenY = key.roomY - onScreenY;
                }

                oldKeyboardState = newKeyboardState;
                oldMouseState = newMouseState;

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(-onScreenX, -onScreenY), Color.White);

            for (int i = 0; i < sizeY / 32; i++)
            {
                for (int j = 0; j < sizeX / 32; j++)
                { spriteBatch.Draw(grid, new Vector2(32 * j - onScreenX, 32 * i - onScreenY), Color.White); }
            }

            for (int i = 0; i < entityList.Count; i++)
            { entityList[i].Draw(spriteBatch); }
            if (player != null)
            { player.Draw(spriteBatch); }
            if (door != null)
            { door.Draw(spriteBatch); }
            if (key != null)
            { key.Draw(spriteBatch); }

            for (int i = 0; i < interfaces.Count; i++)
            { interfaces[i].Draw(spriteBatch); }

            if (choosingSize)
            {
                if (dimension == "X")
                { spriteBatch.DrawString(spriteFont, size, new Vector2(34, 128 + 64), Color.Black); }
                if (dimension == "Y")
                { spriteBatch.DrawString(spriteFont, size, new Vector2(34, 128 + 128), Color.Black); }
            }

            if (saveError)
            {
                if (player == null)
                { spriteBatch.DrawString(spriteFont, "Missing Player", new Vector2(1, 128 + 128 + 64 + 32), Color.Red); }
                if (door == null)
                { spriteBatch.DrawString(spriteFont, "Missing Door", new Vector2(1, 128 + 128 + 64 + 64), Color.Red); }
                if (key == null)
                { spriteBatch.DrawString(spriteFont, "Missing Key", new Vector2(1, 128 + 128 + 64 + 64 + 32), Color.Red); }
            }

            if (buttonClicked && !choosingSize)
            { entityChosen.Draw(spriteBatch); }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
