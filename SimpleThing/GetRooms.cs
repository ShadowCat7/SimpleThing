using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleThing
{
    static class GetRooms
    {
        private static StreamReader reader;
        public static List<Room> roomList;

        public static void getRooms()
        {
            roomList = new List<Room>();

            try
            {
                reader = new StreamReader("second.smf");

                int count = 0;

                while (!reader.EndOfStream)
                {
                    int tempSizeX = Convert.ToInt32(reader.ReadLine());
                    int tempSizeY = Convert.ToInt32(reader.ReadLine());

                    int tempPlayerX = Convert.ToInt32(reader.ReadLine());
                    int tempPlayerY = Convert.ToInt32(reader.ReadLine());
                    Player tempPlayer = new Player(tempPlayerX, tempPlayerY, ImageHandler.playerSprites);

                    int tempDoorX = Convert.ToInt32(reader.ReadLine());
                    int tempDoorY = Convert.ToInt32(reader.ReadLine());
                    StaticEntity tempDoor = new StaticEntity(tempDoorX, tempDoorY, false, false, true, ImageHandler.door);

                    int tempKeyX = Convert.ToInt32(reader.ReadLine());
                    int tempKeyY = Convert.ToInt32(reader.ReadLine());
                    StaticEntity tempKey = new StaticEntity(tempKeyX, tempKeyY, false, false, false, ImageHandler.key);

                    List<StaticEntity> entityList = new List<StaticEntity>();

                    while (reader.Peek() != '*')
                    {
                        int argX = Convert.ToInt32(reader.ReadLine());
                        int argY = Convert.ToInt32(reader.ReadLine());
                        bool argDeath = Convert.ToBoolean(reader.ReadLine());
                        bool argSolid = Convert.ToBoolean(reader.ReadLine());
                        bool argStatic = Convert.ToBoolean(reader.ReadLine());
                        string imageName = reader.ReadLine();

                        entityList.Add(new StaticEntity(argX, argY, argDeath, argSolid, argStatic, ImageHandler.getImage(imageName)));
                    }

                    entityList.Add(tempDoor);
                    entityList.Add(tempKey);

                    reader.ReadLine();

                    roomList.Add(new GameRoom(tempSizeX, tempSizeY, tempPlayer, tempDoor, tempKey, entityList));
                }
            }
            catch (IOException)
            { }
        }
    }
}
