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
    public class Button
    {
        public int screenX, screenY;
        public Texture2D sprite;
        public int sizeX, sizeY;
        public string name;

        protected Button() { }
        public Button(int argScreenX, int argScreenY, string argName, Texture2D argSprite)
        {
            screenX = argScreenX;
            screenY = argScreenY;
            name = argName;
            sizeX = 64;
            sizeY = 64;
            sprite = argSprite;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        { spriteBatch.Draw(sprite, new Vector2(screenX, screenY), Color.White); }
    }

    public class Entity : Button
    {
        public int roomX, roomY;

        public Dictionary<double, Texture2D> sprites;

        public Entity(int argX, int argY, string argName, Texture2D argSprite)
        {
            roomX = argX;
            roomY = argY;
            name = argName;
            sprite = argSprite;

            sizeX = sprite.Width;
            sizeY = sprite.Height;
        }
    }

    public class Camera
    {
        public int roomX, roomY;

        public Camera(int argX, int argY)
        {
            roomX = argX;
            roomY = argY;
        }

        public void Update(KeyboardState newKeyboardState, int roomSizeX, int roomSizeY)
        {
            if (newKeyboardState.IsKeyDown(Keys.W))
            { roomY -= 5; }
            if (newKeyboardState.IsKeyDown(Keys.A))
            { roomX -= 5; }
            if (newKeyboardState.IsKeyDown(Keys.S))
            { roomY += 5; }
            if (newKeyboardState.IsKeyDown(Keys.D))
            { roomX += 5; }

            if (roomX < 400 - 128)
            { roomX = 400 - 128; }
            if (roomX > roomSizeX - 400)
            { roomX = roomSizeX - 400; }
            if (roomY < 240)
            { roomY = 240; }
            if (roomY > roomSizeY - 240)
            { roomY = roomSizeY - 240; }
        }
    }
}
