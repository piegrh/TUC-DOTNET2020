using Engine;
using Engine.Core;
using Engine.Maps;
using System.Collections.Generic;
namespace Inlamningsuppgift2.Game
{
    public class NotSnakeMapBuilder : MapBuilder
    {
        public override GameObject[] Build(Map map)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            System.ConsoleColor a = System.ConsoleColor.White;
            System.ConsoleColor b = System.ConsoleColor.Gray;
            System.ConsoleColor borderColor = System.ConsoleColor.DarkGray;
            foreach (Map.Node node in map.Nodes)
            {
                if (node.IsWall)
                {
                    System.ConsoleColor bg1;
                    System.ConsoleColor bg2;
                    if (IsBorder(node, map.Size))
                    {
                        bg1 = bg2 = borderColor;
                    }
                    else
                    {
                        bg1 = node.position.y % 2 == 0 ? a : b;
                        bg2 = node.position.y % 2 != 0 ? a : b;
                    }
                    gameObjects.Add(new Block()
                    {
                        Position = node.position,
                        BGColor = map.GetIndexOf(node.position) % 2 == 0 ? bg1 : bg2,
                        Character = ' '
                    });
                }
            }
            return gameObjects.ToArray();
        }
        static bool IsBorder(Map.Node n, Vector2 size)
        {
            return !(n.position.y != 0 && n.position.y != size.y - 1 && n.position.x != 0 && n.position.x != size.x - 1);
        }
    }
}
