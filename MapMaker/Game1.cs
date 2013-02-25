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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Button> buttons;
        KeyboardState oldKeyboardState;
        MouseState oldMouseState;

        Texture2D background;

        int sizeX;
        int sizeY;
        int onScreenX;
        int onScreenY;

        bool buttonClicked;

        Entity player;

        List<Entity> entityList;
        Entity entityChosen;
        Camera camera;

        bool saveError;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            oldKeyboardState = new KeyboardState();
            oldMouseState = new MouseState();

            Save.saverSetup();

            sizeX = 800;
            sizeY = 480;
            entityList = new List<Entity>();
            buttons = new List<Button>();
            camera = new Camera(400, 240);
            buttonClicked = false;
        }

        protected override void Initialize()
        { base.Initialize(); }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ImageHandler.player = Content.Load<Texture2D>("player");
            ImageHandler.bullet = Content.Load<Texture2D>("bullet");
            ImageHandler.wall = Content.Load<Texture2D>("wall");
            ImageHandler.ball = Content.Load<Texture2D>("ball");

            buttons.Add(new Button(0, 0, "delete", ImageHandler.bullet));
            buttons.Add(new Button(64, 0, "player", ImageHandler.player));
            buttons.Add(new Button(0, 64, "wall", ImageHandler.wall));
            buttons.Add(new Button(64, 64, "ball", ImageHandler.ball));

            //buttons.Add(new Button(0, 128 + 64, "xsize", Content.Load<Texture2D>("Menu/xmenu")));
            //buttons.Add(new Button(0, 128 + 128, "ysize", Content.Load<Texture2D>("Menu/ymenu")));
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (this.IsActive)
            {
                MouseState newMouseState = Mouse.GetState();
                KeyboardState newKeyboardState = Keyboard.GetState();

                if (player == null)
                { saveError = true; }
                else
                { saveError = false; }

                if (newKeyboardState.IsKeyDown(Keys.LeftControl) && newKeyboardState.IsKeyDown(Keys.S))
                {
                    if (!saveError)
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

                            for (int i = 0; i < entityList.Count; i++)
                            {
                                writer.WriteLine(entityList[i].roomX.ToString());
                                writer.WriteLine(entityList[i].roomY.ToString());
                                writer.WriteLine(entityList[i].name.ToString());
                            }

                            writer.WriteLine("*"); //EOF marker.

                            writer.Close();
                        }
                    }
                }

                camera.Update(newKeyboardState, sizeX, sizeY);
                onScreenX = camera.roomX - 400;
                onScreenY = camera.roomY - 240;

                if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
                {
                    if (newMouseState.X < 128)
                    {
                        for (int i = 0; i < buttons.Count; ++i)
                        {
                            if (Collision.testCoordinate(newMouseState.X, newMouseState.Y, buttons[i]))
                            {
                                if (buttons[i].name == "delete")
                                {
                                    if (buttonClicked)
                                    {
                                        buttonClicked = false;
                                        entityChosen = null;
                                    }
                                }
                                else
                                {
                                    buttonClicked = true;
                                    entityChosen = new Entity(newMouseState.X, newMouseState.Y, buttons[i].name,
                                        ImageHandler.getImage(buttons[i].name));

                                    if (entityChosen.name == "player")
                                    { player = entityChosen; }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (newKeyboardState.IsKeyDown(Keys.F))
                        {
                            for (int i = 0; i < entityList.Count; ++i)
                            {
                                if (Collision.testCoordinate(newMouseState.X, newMouseState.Y, entityList[i]))
                                { entityList.RemoveAt(i); }
                            }
                        }
                        else
                        {
                            if (!buttonClicked)
                            {
                                if (player != null && Collision.testCoordinate(newMouseState.X, newMouseState.Y, player))
                                {
                                    buttonClicked = true;
                                    entityChosen = player;
                                }
                                for (int i = 0; i < entityList.Count; ++i)
                                {
                                    if (Collision.testCoordinate(newMouseState.X, newMouseState.Y, entityList[i]))
                                    {
                                        buttonClicked = true;
                                        entityChosen = entityList[i];
                                        entityList.Remove(entityChosen);
                                    }
                                }
                            }
                            else
                            {
                                bool canPlace = true;
                                for (int i = 0; i < entityList.Count; ++i)
                                {
                                    if (Collision.test(entityChosen, entityList[i]))
                                    { canPlace = false; }
                                }

                                if (canPlace)
                                {
                                    entityChosen.roomX = entityChosen.screenX + onScreenX;
                                    entityChosen.roomY = entityChosen.screenY + onScreenY;
                                    if (entityChosen.name != "player")
                                    { entityList.Add(entityChosen); }
                                    if (newKeyboardState.IsKeyDown(Keys.Z))
                                    {
                                        entityChosen = new Entity(entityChosen.screenX, entityChosen.screenY, entityChosen.name,
                                            entityChosen.sprite);
                                    }
                                    else
                                    {
                                        entityChosen = null;
                                        buttonClicked = false;
                                    }
                                }
                            }
                        }
                    }
                }

                if (entityChosen != null)
                {
                    entityChosen.screenX = newMouseState.X;
                    entityChosen.screenY = newMouseState.Y;
                    if (newKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        entityChosen.screenX /= 32;
                        entityChosen.screenX *= 32;
                        entityChosen.screenY /= 32;
                        entityChosen.screenY *= 32;
                    }
                }

                for (int i = 0; i < entityList.Count; i++)
                {
                    entityList[i].screenX = entityList[i].roomX - onScreenX;
                    entityList[i].screenY = entityList[i].roomY - onScreenY;
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

            for (int i = 0; i < entityList.Count; i++)
            { entityList[i].Draw(spriteBatch); }
            if (player != null)
            { player.Draw(spriteBatch); }

            for (int i = 0; i < buttons.Count; i++)
            { buttons[i].Draw(spriteBatch); }

            if (entityChosen != null)
            { entityChosen.Draw(spriteBatch); }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
