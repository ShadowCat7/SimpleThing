using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleThing
{
    public class GameVector
    {
        public double magnitude;
        public double direction;

        public GameVector(double argMagnitude, double argDirection)
        {
            magnitude = argMagnitude;
            direction = argDirection;
        }

        public double getXLength()
        { return magnitude * Math.Cos(direction); }
        public double getYLength()
        { return magnitude * Math.Sin(direction); }

        public int getXDirection()
        {
            if (getXLength() != 0)
            { return (int)(getXLength() / Math.Abs(getXLength())); }
            else
            { return 0; }
        }

        public void setXLength(double length)
        { makeByCoordinates(length, getYLength()); }

        public void setYLength(double length)
        { makeByCoordinates(getXLength(), length); }

        public void makeByCoordinates(double x, double y)
        {
            magnitude = Math.Sqrt(x * x + y * y);
            direction = Math.Atan2(y, x);
        }
    }
}
