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
    public class GameRoom : Room
    {
        public GameRoom(int argSizeX, int argSizeY)
        {
            sizeX = argSizeX;
            sizeY = argSizeY;
            entityList = new List<StaticEntity>();

            player = new Player(100, 800, ImageHandler.playerStand);

            for (int i = 0; i < 1600 / 64; ++i)
            {
                entityList.Add(new StaticEntity(64 * i, 960 - 64, true, ImageHandler.wall));
                entityList.Add(new StaticEntity(64 * i, 0, true, ImageHandler.wall));
            }

            for (int i = 1; i < 960 / 64 - 1; ++i)
            {
                entityList.Add(new StaticEntity(0, 64 * i, true, ImageHandler.wall));
                entityList.Add(new StaticEntity(1600 - 64, 64 * i, true, ImageHandler.wall));
            }

            entityList.Add(new StaticEntity(64 * 3, 64 * 4, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64, 64 * 6, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64 * 3, 64 * 8, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64, 64 * 10, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64 * 3, 64 * 12, true, ImageHandler.wall));

            entityList.Add(new StaticEntity(1600 - 64 * 4, 64 * 4, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(1600 - 64 * 2, 64 * 6, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(1600 - 64 * 4, 64 * 8, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(1600 - 64 * 2, 64 * 10, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(1600 - 64 * 4, 64 * 12, true, ImageHandler.wall));

            entityList.Add(new StaticEntity(64 * 6, 64 * 4, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64 * 9, 64 * 4, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64 * 12, 64 * 4, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64 * 15, 64 * 4, true, ImageHandler.wall));
            entityList.Add(new StaticEntity(64 * 18, 64 * 4, true, ImageHandler.wall));
        }

        public override void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                entityList[i].update();
                entityList[i].update(gameTime, entityList);
            }

            player.update(gameTime, newKeyboardState, oldKeyboardState, newMouseState, oldMouseState, entityList);

            getScreenPositionFromEntity(player);

            player.setScreenPosition(onScreenX, onScreenY);

            for (int i = 0; i < entityList.Count; i++)
            { entityList[i].setScreenPosition(onScreenX, onScreenY); }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < entityList.Count; ++i)
            { entityList[i].draw(spriteBatch); }

            if (player.onScreen)
            { player.draw(spriteBatch); }
        }

        public void getScreenPositionFromEntity(Player player)
        {
            if (player.getRoomX() + player.getSizeX() / 2 < Screen.X / 2)
            { onScreenX = 0; }
            else if (player.getRoomX() + player.getSizeX() / 2 > sizeX - Screen.X / 2)
            { onScreenX = sizeX - Screen.X; }
            else
            { onScreenX = (int)player.getRoomX() + player.getSizeX() / 2 - Screen.X / 2; }

            if (player.getRoomY() + player.getSizeY() / 2 < Screen.Y / 2)
            { onScreenY = 0; }
            else if (player.getRoomY() + player.getSizeY() / 2 > sizeY - Screen.Y / 2)
            { onScreenY = sizeY - Screen.Y; }
            else
            { onScreenY = (int)player.getRoomY() + player.getSizeY() / 2 - Screen.Y / 2; }
        }
    }
}
