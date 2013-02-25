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
    class Shooter : StaticEntity
    {
        public int count;
        public int projectileSpeed;
        public int rateOfFire;

        public Shooter()
        { }

        public override void update(List<StaticEntity> entityList)
        {
            if (count == projectileSpeed)
            {
                entityList.Add(new Projectile(roomX, roomY, projectileSpeed, direction, ImageHandler.dead));
                count = 0;
            }

            ++count;
        }
    }

    public class Projectile : StaticEntity
    {
        public int speed;

        public Projectile(double argRoomX, double argRoomY, int argVelocity, double argDirection, Texture2D argSprite)
        {
            roomX = argRoomX;
            roomY = argRoomY;
            speed = argVelocity;
            direction = argDirection;
            sprite = argSprite;
        }

        public override void update(List<StaticEntity> entityList)
        {
            for (int j = 0; j < 10; ++j)
            {
                for (int i = 0; i < entityList.Count; ++i)
                {
                    if (entityList[i].isSolid)
                    {
                        if (Collision.Test(this, entityList[i]))
                        { }
                        else
                        { }
                    }
                }
            }
        }
    }

    class LiquidDropper : StaticEntity
    {
        Random rand;
        List<StaticEntity> droplets;
        public LiquidDropper()
        {
            rand = new Random();
        }

        public override void update()
        {
            int angle = rand.Next(1, 21);
        }
    }
}
