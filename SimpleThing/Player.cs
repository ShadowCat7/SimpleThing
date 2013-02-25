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
    public class Player : MovingEntity
    {
        private int facing;

        public Player(double argX, double argY, SpriteSheet argSprites)
        {
            roomX = argX;
            roomY = argY;
            sprites = argSprites;

            solid = false;
            sizeX = 27;
            sizeY = 59;

            topSpeed = 300;
            velocity = new GameVector(0, 0);
            gravityOn = true;
            gravityChange = 0.001;
            friction = 80;

            facing = 1;
        }
        public override void update(GameTime gameTime, KeyboardState newKeyboardState,  KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState, List<StaticEntity> entityList)
        {
            sprites.update();

            if (newKeyboardState.IsKeyDown(Keys.A) && newKeyboardState.IsKeyDown(Keys.D))
            { }
            else if (newKeyboardState.IsKeyDown(Keys.A) || newKeyboardState.IsKeyDown(Keys.D))
            {
                if (newKeyboardState.IsKeyDown(Keys.A))
                { facing = -1; }
                else if (newKeyboardState.IsKeyDown(Keys.D))
                { facing = 1; }

                if (!onGround)
                { velocity.setXLength(velocity.getXLength() + facing * topSpeed / 16.0); }
                else
                {
                    velocity.setXLength(velocity.getXLength() + facing * topSpeed / 8.0);
                    if (newKeyboardState.IsKeyDown(Keys.W))
                    {
                        if (newMouseState.LeftButton == ButtonState.Pressed)
                        { sprites = ImageHandler.playerWalkUpShoot; }
                        else
                        { sprites = ImageHandler.playerWalkUp; }
                    }
                    else
                    {
                        if (newMouseState.LeftButton == ButtonState.Pressed)
                        { sprites = ImageHandler.playerWalkShoot; }
                        else
                        { sprites = ImageHandler.playerWalk; }
                    }
                }
            }
            else //if (!newKeyboardState.IsKeyDown(Keys.A) && !newKeyboardState.IsKeyDown(Keys.D))
            {
                if (onGround)
                {
                    if (Math.Abs(velocity.getXLength()) > friction)
                    {
                        velocity.setXLength((Math.Abs(velocity.getXLength()) - friction)
                          * velocity.getXLength() / Math.Abs(velocity.getXLength()));
                    }
                    else
                    {
                        velocity.setXLength(0);
                        if (newKeyboardState.IsKeyDown(Keys.W))
                        {
                            if (newMouseState.LeftButton == ButtonState.Pressed)
                            { sprites = ImageHandler.playerUpShoot; }
                            else
                            { sprites = ImageHandler.playerUp; }
                        }
                        else
                        {
                            if (newMouseState.LeftButton == ButtonState.Pressed)
                            { sprites = ImageHandler.playerShoot; }
                            else
                            { sprites = ImageHandler.playerStand; }
                        }
                    }
                }
            }
            if (!onGround)
            {
                if (newKeyboardState.IsKeyDown(Keys.W))
                {
                    if (newMouseState.LeftButton == ButtonState.Pressed)
                    { sprites = ImageHandler.playerAirUpShoot; }
                    else
                    { sprites = ImageHandler.playerAirUp; }
                }
                else if (newKeyboardState.IsKeyDown(Keys.S))
                {
                    if (newMouseState.LeftButton == ButtonState.Pressed)
                    { sprites = ImageHandler.playerAirDownShoot; }
                    else
                    { sprites = ImageHandler.playerAirDown; }
                }
                else
                {
                    if (newMouseState.LeftButton == ButtonState.Pressed)
                    { sprites = ImageHandler.playerAirShoot; }
                    else
                    { sprites = ImageHandler.playerAir; }
                }
            }

            if (newKeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
            {
                if (onGround)
                { velocity.setYLength(550); }
            }

            if (!newKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyDown(Keys.Space))
            {
                if (velocity.getYLength() > 200)
                { velocity.setYLength(200); }
            }        

            if (newMouseState.LeftButton == ButtonState.Pressed)
            {
                if (sprites.getCurrentColumn() % 2 == 1 && sprites.getCounter() == 0)
                {
                    double tempDirection = (Rando.random.Next(11) - 5) * 0.01;

                    if (newKeyboardState.IsKeyDown(Keys.W))
                    {
                        tempDirection += Math.PI / 2;
                        entityList.Add(new Bullet(roomX + 12, roomY - 2, 800, tempDirection, 0, ImageHandler.bullet));
                    }
                    else if (!onGround && newKeyboardState.IsKeyDown(Keys.S))
                    {
                        tempDirection += -Math.PI / 2;
                        if (facing == 1)
                        { entityList.Add(new Bullet(roomX + 16, roomY + 71, 800, tempDirection, 0, ImageHandler.bullet)); }
                        else if (facing == -1)
                        { entityList.Add(new Bullet(roomX + 6, roomY + 71, 800, tempDirection, 0, ImageHandler.bullet)); }
                    }
                    else
                    {
                        if (facing == 1)
                        { entityList.Add(new Bullet(roomX + 42, roomY + 30, 800, tempDirection, 0, ImageHandler.bullet)); }
                        else if (facing == -1)
                        {
                            tempDirection += Math.PI;
                            entityList.Add(new Bullet(roomX - 28, roomY + 30, 800, tempDirection, 0, ImageHandler.bullet));
                        }
                    }
                }
            }

            update(gameTime, entityList);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (facing == 1)
            { sprites.draw(spriteBatch, (int)screenX, (int)screenY - 14); }
            else if (facing == -1)
            { sprites.draw(spriteBatch, (int)screenX - 28, (int)screenY - 14, false); }
        }
    }
}
