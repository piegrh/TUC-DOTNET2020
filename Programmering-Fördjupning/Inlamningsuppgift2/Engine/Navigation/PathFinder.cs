using Engine.Maps;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Navigation
{
    public class PathFinder
    {
        public const int UNVISITED = -1;
        public readonly static Vector2 Invalid = new Vector2(UNVISITED, UNVISITED);
        static int[] CreatePath(int dest, int[] prev)
        {
            List<int> list = new List<int>();
            for (int i = dest; i != UNVISITED; i = prev[i])
            {
                list.Add(i);
            }
            return list.ToArray();
        }
        /// <summary>
        /// Returns a path from <paramref name="src"/> to the nearest <see cref="Map.Node"/> of <see cref="Map.Node.NodeType"/>  
        /// </summary>
        /// <param name="map"></param>
        /// <param name="src"></param>
        /// <param name="targetNodeType"></param>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        public static NavPath GetPath(Map map, Vector2 src, int targetNodeType, out Vector2 targetPos)
        {
            var arr = BFS(map, src, targetNodeType, out targetPos);
            if (targetPos == Invalid)
            {
                return null;
            }
            return new NavPath(map.Size, CreatePath(map.GetIndexOf(targetPos), arr));
        }
        static int[] BFS(Map map, Vector2 src, int TargetNodeType, out Vector2 dest)
        {
            dest = Invalid;
            int numberOfNodes = map.Size.x * map.Size.y;

            Queue<Map.Node> q = new Queue<Map.Node>();
            bool[] visited = new bool[numberOfNodes];
            int[] prev = Enumerable.Repeat(UNVISITED, numberOfNodes).ToArray();

            q.Enqueue(map.GetNode(src)); // add src to queue
            visited[map.GetIndexOf(src)] = true; // flag src as visited

            while (q.Count > 0)
            {
                Map.Node currentNode = q.Dequeue();
                if (currentNode.NodeType == TargetNodeType)
                {
                    dest = currentNode.position;
                    break;
                }
                foreach (var neighbor in map.GetNeighbors(currentNode.position))
                {
                    if (neighbor.IsWall)
                    {
                        continue;
                    }
                    int indexOfNeigbor = map.GetIndexOf(neighbor.position);
                    if (!visited[indexOfNeigbor])
                    {
                        q.Enqueue(neighbor);
                        visited[indexOfNeigbor] = true;
                        prev[indexOfNeigbor] = map.GetIndexOf(currentNode.position);
                    }
                }
            }
            return prev;
        }
    }
}
