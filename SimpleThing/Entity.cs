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
    public class Entity
    {
        public double roomX, roomY;
        public int screenX, screenY;
        public double velocityX, velocityY;
        public double direction;

        public bool isKey;
        public bool isTransition;
        public bool isSolid;
        public bool isStatic;
        public bool isDeath;
        public Texture2D sprite;
        public Dictionary<double, Texture2D> sprites;

        public Entity() { }
        public Entity(double argX, double argY, int argVelocity, bool argSolid, bool argStatic, Dictionary<double, Texture2D> argSprites)
        {
            roomX = argX;
            roomY = argY;
            velocityX = argVelocity;
            isSolid = argSolid;
            isStatic = argStatic;
            sprites = argSprites;
            sprite = argSprites[0];
        }

        public Entity(double argX, double argY, bool argDeath, bool argSolid, bool argStatic, Texture2D argSprite)
        {
            roomX = argX;
            roomY = argY;
            velocityX = 0;
            isTransition = false;
            isKey = false;
            isSolid = argSolid;
            isStatic = argStatic;
            isDeath = argDeath;
            sprite = argSprite;
        }

        public virtual void Update() { }
        public virtual void Update(List<Entity> entityList)
        {
            if (!isStatic)
            {
                bool up, down, left, right;
                up = down = left = right = true;

                for (int count = 0; count < 10; count++)
                {
                    for (int i = 0; i < entityList.Count; i++)
                    {
                        if (entityList[i].isSolid)
                        {
                            if (Collision.TestRight(this, entityList[i]) == 0)
                            { right = false; }
                            if (Collision.TestLeft(this, entityList[i]) == Math.PI)
                            { left = false; }
                            double tempDouble = Collision.TestVertical(this, entityList[i]);
                            if (tempDouble == Math.PI / 2)
                            { up = false; }
                            if (tempDouble == -Math.PI / 2)
                            { down = false; }
                        }

                        if (Collision.Test(this, entityList[i]) && Collision.TestVertical(this, entityList[i]) == -Math.PI / 2)
                        { roomY = entityList[i].roomY - sprite.Height; }
                    }

                    if (velocityX > 0 && right)
                    { roomX += velocityX / 10; }

                    if (velocityX < 0 && left)
                    { roomX += velocityX / 10; }

                    if (velocityX != 0)
                    {
                        if (Math.Abs(velocityX) < 0.01)
                        { velocityX = 0; }
                        if (!down)
                        { velocityX = Math.Abs(velocityX) / velocityX * (Math.Abs(velocityX) - 0.01); }
                    }

                    if (!down)
                    { velocityY = 0; }

                    roomY -= velocityY / 10;
                }

                if (down)
                { velocityY -= 0.25; }
            }
        }
        public virtual void Update(KeyboardState newKeyboardState, KeyboardState oldKeyboardState, MouseState newMouseState, 
            MouseState oldMouseState, List<Entity> entityList)
        { }
        public virtual void Draw(SpriteBatch spriteBatch)
        { spriteBatch.Draw(sprite, new Vector2(screenX, screenY), Color.White); }
    }

    public class Player : Entity
    {
        Entity heldObject;

        public Player(double argX, double argY, Dictionary<double, Texture2D> argSprites)
        {
            roomX = argX;
            roomY = argY;
            velocityX = 4;
            velocityY = 0;
            isSolid = true;
            isStatic = true;
            sprites = argSprites;
            sprite = argSprites[0];

            heldObject = null;
        }
        public override void Update(KeyboardState newKeyboardState, KeyboardState oldKeyboardState, MouseState newMouseState,
            MouseState oldMouseState, List<Entity> entityList)
        {
            bool up, down, left, right;
            up = down = left = right = true;

            double tempRoomX = roomX;

            for (int count = 0; count < 10; count++)
            {
                for (int i = 0; i < entityList.Count; i++)
                {
                    if (entityList[i].isStatic && entityList[i].isSolid)
                    {
                        if (Collision.TestRight(this, entityList[i]) == 0)
                        { right = false; }
                        if (Collision.TestLeft(this, entityList[i]) == Math.PI)
                        { left = false; }
                        double tempDouble = Collision.TestVertical(this, entityList[i]);
                        if (tempDouble == Math.PI / 2)
                        { up = false; }
                        if (tempDouble == -Math.PI / 2)
                        { down = false; }

                        if (Collision.Test(this, entityList[i])) //&& Collision.TestVertical(this, entityList[i]) == -Math.PI / 2)
                        { roomY = entityList[i].roomY - sprite.Height; }
                    }
                }
                if (newKeyboardState.IsKeyDown(Keys.A))
                {
                    if (left)
                    {
                        roomX -= velocityX / 10;
                        if (heldObject != null)
                        { heldObject.velocityX = -4; }
                    }
                    if (!down)
                    { direction = Math.PI; }
                }
                if (newKeyboardState.IsKeyDown(Keys.D))
                {
                    if (right)
                    {
                        roomX += velocityX / 10;
                        if (heldObject != null)
                        { heldObject.velocityX = 4; }
                    }
                    if (!down)
                    { direction = 0; }
                }

                if (!down)
                {
                    velocityY = 0;
                    if (!oldKeyboardState.IsKeyDown(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Space))
                    { velocityY = 6; }
                }

                if (oldKeyboardState.IsKeyDown(Keys.Space) && !newKeyboardState.IsKeyDown(Keys.Space) && velocityY > 0)
                { velocityY = 0; }

                roomY -= velocityY / 10;
            }

            bool gotObject = false;

            for (int i = 0; i < entityList.Count && !gotObject; i++)
            {
                if (heldObject == null)
                {
                    if (!entityList[i].isStatic && Collision.Test(this, entityList[i]) && newKeyboardState.IsKeyDown(Keys.E) && !oldKeyboardState.IsKeyDown(Keys.E))
                    {
                        heldObject = entityList[i];
                        heldObject.isStatic = true;
                        gotObject = true;
                    }
                }
            }

            if (heldObject != null)
            {
                if (direction == 0)
                { heldObject.roomX = roomX + sprite.Width; }
                if (direction == Math.PI)
                { heldObject.roomX = roomX - heldObject.sprite.Width; }
                heldObject.roomY = roomY + 16;
            }

            if (!gotObject && newKeyboardState.IsKeyDown(Keys.E) && !oldKeyboardState.IsKeyDown(Keys.E) && heldObject != null)
            {
                heldObject.isStatic = false;
                heldObject.velocityY = velocityY;

                if (tempRoomX == roomX)
                { heldObject.velocityX = 0; }

                heldObject = null;
                //objectHeld = false;
            }

            if (down)
            { velocityY -= 0.25; }

            sprite = sprites[direction];
        }
    }
}
