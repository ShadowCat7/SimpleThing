using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleThing
{
    class First : GameRoom
    {
        public First()
        {
            entityList = new List<Entity>();
            entityList.Add(new Entity(-200, 400, false, true, true, ImageHandler.ground));
            entityList.Add(new Entity(504, 400, false, true, true, ImageHandler.ground));

            entityList.Add(new Entity(0, 200, false, true, true, ImageHandler.wall));
            entityList.Add(new Entity(784, 200, false, true, true, ImageHandler.wall));
            entityList.Add(new Entity(264, 416, false, true, true, ImageHandler.wall));
            entityList.Add(new Entity(504, 416, false, true, true, ImageHandler.wall));

            for (int i = 0; i < 7; i++)
            { entityList.Add(new Entity(280 + 32 * i, 432, true, true, true, ImageHandler.spike)); }

            player = new Player(100, 300, 5, true, true, ImageHandler.playerSprites);

            deadBodies = new List<Entity>();

            door = new Entity(100, 416 - 64 - 16, false, false, true, ImageHandler.door);
            door.isTransition = true;
            entityList.Add(door);

            key = new Entity(700, 300, false, false, false, ImageHandler.key);
            key.isKey = true;
            entityList.Add(key);
        }
    }

    class Second : GameRoom
    {
        public Second()
        {
            entityList = new List<Entity>();
            entityList.Add(new Entity(-200, 400, false, true, true, ImageHandler.ground));
            entityList.Add(new Entity(504, 400, false, true, true, ImageHandler.ground));

            for (int i = 0; i < 7; i++)
            { entityList.Add(new Entity(280 + 32 * i, 432, true, true, true, ImageHandler.spike)); }

            player = new Player(100, 300, 5, true, true, ImageHandler.playerSprites);

            deadBodies = new List<Entity>();

            door = new Entity(100, 416 - 64 - 16, false, false, true, ImageHandler.door);
            door.isTransition = true;
            entityList.Add(door);

            key = new Entity(700, 300, false, false, false, ImageHandler.key);
            key.isKey = true;
            entityList.Add(key);
        }
    }
}
