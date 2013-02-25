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
    public class Bullet : MovingEntity
    {
        private int damage;

        public Bullet(double argX, double argY, int argTopSpeed, double argDirection, int argDamage, Texture2D argSprite)
        {
            roomX = argX;
            roomY = argY;
            velocity = new GameVector(argTopSpeed, argDirection);
            topSpeed = argTopSpeed;
            damage = argDamage;
            sprite = argSprite;

            solid = false;
            gravityOn = false;
            sizeX = sprite.Width;
            sizeY = sprite.Height;
        }

        public override void update(GameTime gameTime, List<StaticEntity> entityList)
        {
            base.update(gameTime, entityList);

            for (int i = 0; i < entityList.Count; ++i)
            {
                if (this != entityList[i] && entityList[i].isSolid() && Collision.test(this, entityList[i]))
                {
                    entityList[i].hitByBullet(0);
                    entityList.Remove(this);
                }
            }
        }
    }
}
