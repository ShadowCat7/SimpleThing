using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SimpleThing
{
    public class SpriteSheet
    {
        private Texture2D texture;
        private int columns;

        private int currentColumn;
        public int getCurrentColumn()
        { return currentColumn; }

        private int counter;
        public int getCounter()
        { return counter; }

        public SpriteSheet(Texture2D argTexture, int totalColumns)
        {
            texture = argTexture;
            columns = totalColumns;
            reset();
        }

        public void reset()
        {
            currentColumn = 0;
            counter = 0;
        }

        public void update()
        {
            if (counter == 4)
            {
                ++currentColumn;

                if (currentColumn == columns)
                { currentColumn = 0; }

                counter = 0;
            }
            else
            { ++counter; }
        }

        public void draw(SpriteBatch spriteBatch, int screenX, int screenY)
        {
            spriteBatch.Draw(texture, new Vector2(screenX, screenY),
                new Rectangle(currentColumn * texture.Width / columns, 0, texture.Width / columns, texture.Height), 
                Color.White);
        }

        public void draw(SpriteBatch spriteBatch, int screenX, int screenY, bool facingRight)
        {
            SpriteEffects effect;
            if (facingRight)
            { effect = SpriteEffects.None; }
            else
            { effect = SpriteEffects.FlipHorizontally; }

            spriteBatch.Draw(texture, new Vector2(screenX, screenY),
                new Rectangle(currentColumn * texture.Width / columns, 0, texture.Width / columns, texture.Height),
                Color.White, 0, new Vector2(), 1, effect, 0);
        }
    }
}
