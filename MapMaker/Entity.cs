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
    public class Interface
    {
        public int screenX, screenY;
        public Texture2D sprite;
        public string entityName;

        public Interface(int argScreenX, int argScreenY, string argName, Texture2D argSprite)
        {
            screenX = argScreenX;
            screenY = argScreenY;
            sprite = argSprite;
            entityName = argName;
        }

        public void Draw(SpriteBatch spriteBatch)
        { spriteBatch.Draw(sprite, new Vector2(screenX, screenY), Color.White); }
    }

    public class Entity
    {
        public int roomX, roomY;
        public int screenX, screenY;
        public Texture2D sprite;
        public string name;
        public bool isKey;
        public bool isTransition;
        public bool isSolid;
        public bool isStatic;
        public bool isDeath;
        public Dictionary<double, Texture2D> sprites;

        public Entity() { }

        public Entity(int argX, int argY, string argName, bool argDeath, bool argSolid, bool argStatic, Texture2D argSprite)
        {
            roomX = argX;
            roomY = argY;
            name = argName;
            isSolid = argSolid;
            isStatic = argStatic;
            isDeath = argDeath;
            sprite = argSprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        { spriteBatch.Draw(sprite, new Vector2(screenX, screenY), Color.White); }
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
