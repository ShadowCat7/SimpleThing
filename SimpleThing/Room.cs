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
    public abstract class Room
    {
        protected Player player;
        protected List<StaticEntity> entityList;

        protected int onScreenX;
        protected int onScreenY;
        protected int sizeX;
        protected int sizeY;

        protected Room() { }

        public virtual void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState) { }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < entityList.Count; i++)
            { entityList[i].draw(spriteBatch); }
        }
    }
}
