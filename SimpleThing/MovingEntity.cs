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
    public class MovingEntity : StaticEntity
    {
        protected int topSpeed;
        protected GameVector velocity;
        protected bool gravityOn;
        protected bool onGround = false;
        protected double friction;
        protected double gravityChange;

        protected MovingEntity() { }

        protected virtual void checkVelocity()
        {
            if (!gravityOn)
            {
                if (velocity.magnitude > topSpeed)
                { velocity.magnitude = topSpeed; }
            }
            else
            {
                if (Math.Abs(velocity.getXLength()) > topSpeed)
                { velocity.setXLength(topSpeed * Math.Abs(velocity.getXLength()) / velocity.getXLength()); }
            }
        }

        public override void update(GameTime gameTime, List<StaticEntity> entityList)
        {
            if (gravityOn)
            { velocity.setYLength(velocity.getYLength() - 1000 * gameTime.ElapsedGameTime.Milliseconds * gravityChange); }

            for (int i = 0; i < 10; ++i)
            {
                checkVelocity();

                double addX = velocity.getXLength() * gameTime.ElapsedGameTime.Milliseconds * 0.001 / 10;
                double addY = velocity.getYLength() * gameTime.ElapsedGameTime.Milliseconds * 0.001 / 10;

                if (gravityOn || solid)
                {
                    bool up, down, left, right;
                    up = down = left = right = true;

                    for (int j = 0; j < entityList.Count; ++j)
                    {
                        if (this != entityList[j] && entityList[j].isSolid())
                        {
                            if (up && Collision.testUp(this, entityList[j]))
                            { up = false; }
                            if (down && Collision.testDown(this, entityList[j]))
                            {
                                down = false;
                                if (gravityOn)
                                {
                                    if (roomY + sizeY > entityList[j].getRoomY() && roomY + sizeY < entityList[j].getRoomY() + 10)
                                    { roomY = entityList[j].getRoomY() - 1 - sizeY; }
                                }
                            }
                            if (left && Collision.testLeft(this, entityList[j]))
                            { left = false; }
                            if (right && Collision.testRight(this, entityList[j]))
                            { right = false; }
                        }
                    }

                    onGround = !down;

                    if (addX > 0 && !right)
                    { addX = 0; }
                    if (addX < 0 && !left)
                    { addX = 0; }
                    if (addY > 0 && !up)
                    { addY = 0; }
                    if (addY < 0 && !down)
                    { addY = 0; }
                }

                roomX += addX;
                roomY -= addY;

                if (gameTime.ElapsedGameTime.Milliseconds != 0)
                {
                    addX /= (gameTime.ElapsedGameTime.Milliseconds * 0.001) / 10;
                    addY /= (gameTime.ElapsedGameTime.Milliseconds * 0.001) / 10;
                    velocity.makeByCoordinates(addX, addY);
                }
            }
        }
    }
}