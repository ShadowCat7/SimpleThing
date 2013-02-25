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
    public static class ImageHandler
    {
        public static Texture2D player;
        public static Texture2D bullet;
        public static Texture2D wall;
        public static Texture2D ball;

        public static Texture2D getImage(string imageName)
        {
            if (imageName == "player")
            { return player; }
            else if (imageName == "bullet")
            { return bullet; }
            else if (imageName == "wall")
            { return wall; }
            else if (imageName == "ball")
            { return ball; }
            else
            { return null; }
        }
    }
}
