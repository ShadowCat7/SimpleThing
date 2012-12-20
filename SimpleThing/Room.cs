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
    public class Room
    {
        public Player player;
        public List<Entity> entityList;
        public List<Entity> deadBodies;
        public Entity key;
        public Entity door;
        public int onScreenX;
        public int onScreenY;
        public int sizeX;
        public int sizeY;
        public Room() { }

        public virtual void Update(KeyboardState newKeyboardState, KeyboardState oldKeyboardState, MouseState newMouseState,
            MouseState oldMouseState) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }

    public class GameRoom : Room
    {
        public GameRoom(int argSizeX, int argSizeY, Player argPlayer, Entity argDoor, Entity argKey, List<Entity> argEntityList)
        {
            sizeX = argSizeX;
            sizeY = argSizeY;
            player = argPlayer;
            door = argDoor;
            key = argKey;
            entityList = argEntityList;

            deadBodies = new List<Entity>();
        }

        public override void Update(KeyboardState newKeyboardState, KeyboardState oldKeyboardState, MouseState newMouseState, 
            MouseState oldMouseState)
        {
            player.Update(newKeyboardState, oldKeyboardState, newMouseState, oldMouseState, entityList);

            for (int i = 0; i < entityList.Count; i++)
            {
                entityList[i].Update();
                entityList[i].Update(entityList);
            }

            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].isDeath)
                {
                    if (Collision.Test(player, entityList[i]))
                    {
                        Entity temp = new Entity(player.roomX, player.roomY, false, true, true, ImageHandler.dead);
                        deadBodies.Add(temp);
                        entityList.Add(temp);

                        if (deadBodies.Count > 2)
                        {
                            entityList.Remove(deadBodies[0]);
                            deadBodies.RemoveAt(0);
                        }

                        player = new Player(100, 100, ImageHandler.playerSprites);
                    }
                }
            }

            if (player.roomX + player.sprite.Bounds.Center.X < Screen.X / 2)
            {
                player.screenX = (int)player.roomX;
                onScreenX = 0;
            }
            else if (player.roomX + player.sprite.Bounds.Center.X > sizeX - Screen.X / 2)
            {
                player.screenX = Screen.X - (sizeX - (int)player.roomX);
                onScreenX = sizeX - Screen.X;
            }
            else
            {
                player.screenX = Screen.X / 2;
                onScreenX = (int)player.roomX + player.sprite.Bounds.Center.X + Screen.X / 2;
            }

            if (player.roomY + player.sprite.Bounds.Center.Y < Screen.Y / 2)
            {
                player.screenY = (int)player.roomY;
                onScreenY = 0;
            }
            else if (player.roomY + player.sprite.Bounds.Center.Y > sizeY - Screen.Y / 2)
            {
                player.screenY = Screen.Y - (sizeY - (int)player.roomY);
                onScreenY = sizeY - Screen.Y;
            }
            else
            {
                player.screenY = Screen.Y / 2;
                onScreenY = (int)player.roomY + player.sprite.Bounds.Center.Y + Screen.Y / 2;
            }

            for (int i = 0; i < entityList.Count; i++)
            {
                entityList[i].screenX = (int)entityList[i].roomX - onScreenX;
                entityList[i].screenY = (int)entityList[i].roomY - onScreenY;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < entityList.Count; i++)
            { entityList[i].Draw(spriteBatch); }
            player.Draw(spriteBatch);
        }
    }
}
