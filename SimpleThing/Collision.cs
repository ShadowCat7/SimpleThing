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
    public static class Collision
    {
        public static double TestVertical(Entity collider, Entity collideWith)
        {
            if ((collider.roomX >= collideWith.roomX && collider.roomX <= collideWith.roomX +
                collideWith.sprite.Width - 1) || (collider.roomX + collider.sprite.Width >= collideWith.roomX + 1 &&
                collider.roomX + collider.sprite.Width <= collideWith.roomX + collideWith.sprite.Width))
            {
                if (collider.roomY <= collideWith.roomY + collideWith.sprite.Height && collider.roomY >=
                    collideWith.roomY)
                { return Math.PI / 2; }
                if (collider.roomY + collider.sprite.Height >= collideWith.roomY && collider.roomY +
                    collider.sprite.Height <= collideWith.roomY + collideWith.sprite.Height)
                { return -Math.PI / 2; }
            }
            return 10;
        }
        /// <summary>
        /// Tests any collisions on the left and right sides of an element.
        /// </summary>
        /// <param name="collider">The moving element.</param>
        /// <param name="collideWith">The element being collided with.</param>
        /// <returns>Returns the direction in radians, or 10 if there were no collision.</returns>
        public static double TestLeft(Entity collider, Entity collideWith)
        {
            if ((collider.roomY >= collideWith.roomY && collider.roomY <= collideWith.roomY +
                collideWith.sprite.Height - 1) || (collider.roomY + collider.sprite.Height >= collideWith.roomY + 1 &&
                collider.roomY + collider.sprite.Height <= collideWith.roomY + collideWith.sprite.Height))
            {
                if (collider.roomX <= collideWith.roomX + collideWith.sprite.Width + 2 && collider.roomX >= collideWith.roomX)
                { return Math.PI; }
            }

            if ((collideWith.roomY >= collider.roomY && collideWith.roomY <= collider.roomY +
                collider.sprite.Height - 1) || (collideWith.roomY + collideWith.sprite.Height >= collider.roomY + 1 &&
                collideWith.roomY + collideWith.sprite.Height <= collider.roomY + collider.sprite.Height))
            {
                if (collideWith.roomX <= collider.roomX + collider.sprite.Width && collideWith.roomX >= collider.roomX)
                { return Math.PI; }
            }

            return 10;
        }

        public static double TestRight(Entity collider, Entity collideWith)
        {
            if ((collider.roomY >= collideWith.roomY && collider.roomY <= collideWith.roomY +
                collideWith.sprite.Height - 1) || (collider.roomY + collider.sprite.Height >= collideWith.roomY + 1 &&
                collider.roomY + collider.sprite.Height <= collideWith.roomY + collideWith.sprite.Height))
            {
                if (collider.roomX + collider.sprite.Width + 1 >= collideWith.roomX && collider.roomX + collider.sprite.Width <=
                    collideWith.roomX + collideWith.sprite.Width)
                { return 0; }
            }

            if ((collideWith.roomY >= collider.roomY && collideWith.roomY <= collider.roomY +
                collider.sprite.Height - 1) || (collideWith.roomY + collideWith.sprite.Height >= collider.roomY + 1 &&
                collideWith.roomY + collideWith.sprite.Height <= collider.roomY + collider.sprite.Height))
            {
                if (collideWith.roomX + collideWith.sprite.Width + 1 >= collider.roomX && collideWith.roomX + collideWith.sprite.Width <=
                    collider.roomX + collider.sprite.Width)
                { return 0; }
            }

            return 10;
        }

        public static bool Test(Entity collider, Entity collideWith)
        {
            if (collider.roomX >= collideWith.roomX && collider.roomX <= collideWith.roomX + collideWith.sprite.Width)
            {
                if (collider.roomY >= collideWith.roomY && collider.roomY <= collideWith.roomY + collideWith.sprite.Height)
                { return true; }
                if (collider.roomY + collider.sprite.Height >= collideWith.roomY && collider.roomY + collider.sprite.Height <=
                    collideWith.roomY + collideWith.sprite.Height)
                { return true; }
            }
            if (collider.roomX + collider.sprite.Width >= collideWith.roomX && collider.roomX + collider.sprite.Width <=
                collideWith.roomX + collideWith.sprite.Width)
            {
                if (collider.roomY >= collideWith.roomY && collider.roomY <= collideWith.roomY + collideWith.sprite.Height)
                { return true; }
                if (collider.roomY + collider.sprite.Height >= collideWith.roomY && collider.roomY + collider.sprite.Height <=
                    collideWith.roomY + collideWith.sprite.Height)
                { return true; }
            }

            Entity tempElement = collider;
            collider = collideWith;
            collideWith = tempElement;

            if (collider.roomX >= collideWith.roomX && collider.roomX <= collideWith.roomX + collideWith.sprite.Width)
            {
                if (collider.roomY >= collideWith.roomY && collider.roomY <= collideWith.roomY + collideWith.sprite.Height)
                { return true; }
                if (collider.roomY + collider.sprite.Height >= collideWith.roomY && collider.roomY + collider.sprite.Height <=
                    collideWith.roomY + collideWith.sprite.Height)
                { return true; }
            }
            if (collider.roomX + collider.sprite.Width >= collideWith.roomX && collider.roomX + collider.sprite.Width <=
                collideWith.roomX + collideWith.sprite.Width)
            {
                if (collider.roomY >= collideWith.roomY && collider.roomY <= collideWith.roomY + collideWith.sprite.Height)
                { return true; }
                if (collider.roomY + collider.sprite.Height >= collideWith.roomY && collider.roomY + collider.sprite.Height <=
                    collideWith.roomY + collideWith.sprite.Height)
                { return true; }
            }

            return false;
        }
    }
}
