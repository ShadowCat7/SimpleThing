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
        public static bool testUp(StaticEntity mover, StaticEntity checkAgainst)
        {
            if (mover.getRoomY() <= checkAgainst.getRoomY() + checkAgainst.getSizeY() + 1 && mover.getRoomY() + mover.getSizeY() > checkAgainst.getRoomY())
            {
                if (mover.getRoomX() >= checkAgainst.getRoomX() && mover.getRoomX() <= checkAgainst.getRoomX() + checkAgainst.getSizeX())
                { return true; }
                else if (mover.getRoomX() + mover.getSizeX() >= checkAgainst.getRoomX() &&
                    mover.getRoomX() + mover.getSizeX() <= checkAgainst.getRoomX() + checkAgainst.getSizeX())
                { return true; }
            }

            return false;
        }
        public static bool testDown(StaticEntity mover, StaticEntity checkAgainst)
        {
            if (mover.getRoomY() + mover.getSizeY() >= checkAgainst.getRoomY() - 1 && mover.getRoomY() < checkAgainst.getRoomY() + checkAgainst.getSizeY())
            {
                if (mover.getRoomX() >= checkAgainst.getRoomX() && mover.getRoomX() <= checkAgainst.getRoomX() + checkAgainst.getSizeX())
                { return true; }
                else if (mover.getRoomX() + mover.getSizeX() >= checkAgainst.getRoomX() &&
                    mover.getRoomX() + mover.getSizeX() <= checkAgainst.getRoomX() + checkAgainst.getSizeX())
                { return true; }
            }

            return false;
        }
        public static bool testLeft(StaticEntity mover, StaticEntity checkAgainst)
        {
            if (mover.getRoomX() <= checkAgainst.getRoomX() + checkAgainst.getSizeX() + 1 && mover.getRoomX() + mover.getSizeX() > checkAgainst.getRoomX())
            {
                if (mover.getRoomY() >= checkAgainst.getRoomY() && mover.getRoomY() <= checkAgainst.getRoomY() + checkAgainst.getSizeY())
                { return true; }
                else if (mover.getRoomY() + mover.getSizeY() >= checkAgainst.getRoomY() && 
                    mover.getRoomY() + mover.getSizeY() <= checkAgainst.getRoomY() + checkAgainst.getSizeY())
                { return true; }
            }

            return false;
        }
        public static bool testRight(StaticEntity mover, StaticEntity checkAgainst)
        {
            if (mover.getRoomX() + mover.getSizeX() >= checkAgainst.getRoomX() - 1 && mover.getRoomX() < checkAgainst.getRoomX() + checkAgainst.getSizeX())
            {
                if (mover.getRoomY() >= checkAgainst.getRoomY() && mover.getRoomY() <= checkAgainst.getRoomY() + checkAgainst.getSizeY())
                { return true; }
                else if (mover.getRoomY() + mover.getSizeY() >= checkAgainst.getRoomY() &&
                    mover.getRoomY() + mover.getSizeY() <= checkAgainst.getRoomY() + checkAgainst.getSizeY())
                { return true; }
            }

            return false;
        }
        public static bool test(StaticEntity mover, StaticEntity checkAgainst)
        {
            if (testCoordinate(mover.getRoomX(), mover.getRoomY(), checkAgainst))
            { return true; }
            else if (testCoordinate(mover.getRoomX() + mover.getSizeX(), mover.getRoomY(), checkAgainst))
            { return true; }
            else if (testCoordinate(mover.getRoomX(), mover.getRoomY() + mover.getSizeY(), checkAgainst))
            { return true; }
            else if (testCoordinate(mover.getRoomX() + mover.getSizeX(), mover.getRoomY() + mover.getSizeY(), checkAgainst))
            { return true; }
            else
            { return false; }
        }
        public static bool testCoordinate(double xPosition, double yPosition, StaticEntity checkInEntity)
        {
            if (xPosition >= checkInEntity.getRoomX() && xPosition <= checkInEntity.getRoomX() + checkInEntity.getSizeX())
            {
                if (yPosition >= checkInEntity.getRoomY() && yPosition <= checkInEntity.getRoomY() + checkInEntity.getSizeY())
                { return true; }
            }

            return false;
        }
    }
}
