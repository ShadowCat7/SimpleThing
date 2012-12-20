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
    public static class ImageHandler
    {
        public static Dictionary<double, Texture2D> playerSprites;
        public static Texture2D dead;
        public static Texture2D key;
        public static Texture2D wall;
        public static Texture2D spike;
        public static Texture2D door;

        public static Texture2D getImage(string imageName)
        {
            if (imageName == "wall")
            { return wall; }
            else if (imageName == "spike")
            { return spike; }
            else
            { return null; }
        }
    }
}
