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

namespace MapMaker
{
    public static class Collision
    {
        public static bool test(Button mover, Button checkAgainst)
        {
            if (testCoordinate(mover.screenX, mover.screenY, checkAgainst))
            { return true; }
            else if (testCoordinate(mover.screenX + mover.sizeX, mover.screenY, checkAgainst))
            { return true; }
            else if (testCoordinate(mover.screenX, mover.screenY + mover.sizeY, checkAgainst))
            { return true; }
            else if (testCoordinate(mover.screenX + mover.sizeX, mover.screenY + mover.sizeY, checkAgainst))
            { return true; }
            else
            { return false; }
        }
        public static bool testCoordinate(double xPosition, double yPosition, Button checkInEntity)
        {
            if (xPosition > checkInEntity.screenX && xPosition < checkInEntity.screenX + checkInEntity.sizeX)
            {
                if (yPosition > checkInEntity.screenY && yPosition < checkInEntity.screenY + checkInEntity.sizeY)
                { return true; }
            }

            return false;
        }
    }
}
