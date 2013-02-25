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
    public class StaticEntity
    {
        protected double roomX, roomY;
        public double getRoomX()
        { return roomX; }
        public double getRoomY()
        { return roomY; }

        protected int sizeX, sizeY;
        public int getSizeX()
        { return sizeX; }
        public int getSizeY()
        { return sizeY; }

        protected int screenX, screenY;

        public bool onScreen = false;

        public void setScreenPosition(int roomScreenX, int roomScreenY)
        {
            if (Collision.test(this, new StaticEntity(roomScreenX, roomScreenY, Screen.X, Screen.Y)))
            {
                screenX = (int)roomX - roomScreenX;
                screenY = (int)roomY - roomScreenY;
                onScreen = true;
            }
            else
            { onScreen = false; }
        }

        protected bool solid;
        public bool isSolid()
        { return solid; }

        protected Texture2D sprite;
        private SpriteSheet _sprites;
        protected SpriteSheet sprites
        {
            get
            { return _sprites; }
            set
            {
                if (value != _sprites)
                { value.reset(); }
                _sprites = value;
            }
        }

        protected StaticEntity() { }
        private StaticEntity(int argX, int argY, int argSizeX, int argSizeY)
        {
            roomX = argX;
            roomY = argY;
            sizeX = argSizeX;
            sizeY = argSizeY;
        }
        public StaticEntity(double argX, double argY, bool argSolid, Texture2D argSprite)
        {
            roomX = argX;
            roomY = argY;
            solid = argSolid;
            sprite = argSprite;

            sizeX = sprite.Bounds.Width;
            sizeY = sprite.Bounds.Height;
        }
        public StaticEntity(double argX, double argY, int argSizeX, int argSizeY, bool argSolid, Texture2D argSprite)
        {
            roomX = argX;
            roomY = argY;
            sizeX = argSizeX;
            sizeY = argSizeY;
            solid = argSolid;
            sprite = argSprite;
        }
        public StaticEntity(double argX, double argY, int argSizeX, int argSizeY, bool argSolid, SpriteSheet argSprites)
        {
            roomX = argX;
            roomY = argY;
            sizeX = argSizeX;
            sizeY = argSizeY;
            solid = argSolid;
            sprites = argSprites;
        }

        public virtual void update() { }
        public virtual void update(GameTime gameTime, List<StaticEntity> entityList) { }
        public virtual void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState, List<StaticEntity> entityList) { }

        public virtual void hitByBullet(int bulletDamage) { }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (onScreen)
            {
                if (sprite != null)
                { spriteBatch.Draw(sprite, new Vector2(screenX, screenY), Color.White); }
                else
                { sprites.draw(spriteBatch, screenX, screenY); }
            }
        }
    }
}
