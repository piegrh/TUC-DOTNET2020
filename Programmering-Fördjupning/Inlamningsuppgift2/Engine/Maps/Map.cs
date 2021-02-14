using System;
using System.Collections.Generic;

namespace Engine.Maps
{
    public class Map
    {
        static readonly Direction[] DirectionsArray = new Direction[] {
            Direction.Up, Direction.Down, Direction.Left, Direction.Right
        };
        public class Node : ICloneable
        {
            public const int FLOOR = 0;
            public const int WALL = -1;
            public int NodeType { get; set; }
            /// <summary>
            /// Negative NodeType value => Wall, Positive => Floor
            /// </summary>
            public bool IsWall => NodeType < FLOOR;
            /// <summary>
            /// Negative NodeType value => Wall, Positive => Floor
            /// </summary>
            public bool IsFloor => !IsWall;
            public Vector2 position;
            public Node(Vector2 position, int nodeType = FLOOR)
            {
                this.position = position;
                NodeType = nodeType;
            }
            public Node(Node node)
            {
                position = new Vector2(node.position.x, node.position.y);
                NodeType = node.NodeType;
            }
            public object Clone()
            {
                return new Node(new Vector2(position), NodeType);
            }
        }
        public Node[,] Nodes { get; protected set; }
        public Vector2 Size { get; protected set; }
        /// <summary>
        /// <see cref="Map"/> constructor
        /// </summary>
        /// <param name="map"></param>
        public Map(Map map)
        {
            Nodes = new Node[map.Size.x, map.Size.y];
            Size = map.Size;
            for (int y = 0; y < map.Size.y; y++)
            {
                for (int x = 0; x < map.Size.x; x++)
                {
                    Nodes[x, y] = new Node(map.Nodes[x, y]);
                }
            }
        }
        /// <summary>
        /// <see cref="Map"/> constructor
        /// </summary>
        /// <param name="size"></param>
        public Map(Vector2 size)
        {
            Size = size;
            Nodes = new Node[Size.x, Size.y];
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    Nodes[x, y] = new Node(new Vector2(x, y), Node.FLOOR);
                }
            }
        }
        /// <summary>
        /// Returns the <see cref="Node"/> located at position (<paramref name="x"/>,<paramref name="y"/>).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Node GetNode(int x, int y) => Nodes[x, y];
        /// <summary>
        /// Returns the <see cref="Node"/>  located at position <paramref name="p"/>.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Node GetNode(Vector2 p) => GetNode(p.x, p.y);
        /// <summary>
        /// Tries to get a <paramref name="node"/> from <paramref name="position"/>.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool TryGetNode(Vector2 position, out Node node)
        {
            node = null;
            if (Utils.IsInBounds(position, Size))
            {
                node = Nodes[position.x, position.y];
                return (node != null);
            }
            return false;
        }
        /// <summary>
        /// Returns <see langword="true"/> if a neighboring <see cref="Node"/> was found.
        /// </summary>
        /// <param name="positon"></param>
        /// <param name="dir"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool GetNeighbor(Vector2 positon, Direction dir, out Node n)
        {
            return TryGetNode(positon + Utils.DirectionToVector2(dir), out n);
        }
        /// <summary>
        /// Returns adjecent <see cref="Node"/>s
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public List<Node> GetNeighbors(Vector2 position)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < DirectionsArray.Length; i++)
            {
                if (GetNeighbor(position, DirectionsArray[i], out Node n))
                {
                    nodes.Add(n);
                }
            }
            return nodes;
        }
        /// <summary>
        /// Converts a <see cref="Vector2"/> into a 1D index.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int GetIndexOf(Vector2 p) => p.x + (Size.x * p.y);
        /// <summary>
        /// Sets all <see cref="Node"/>s to <see langword="null"/> in <see cref="Nodes"/>.
        /// </summary>
        public void Clear()
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    Nodes[x, y] = null;
                }
            }
        }
        /// <summary>
        /// Returns a <see langword="string"/> representation of this <see cref="Map"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    if (Nodes[x, y] is null)
                    {
                        continue;
                    }
                    sb.Append(Nodes[x, y].IsFloor ? ' ' : '#');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        /// <summary>
        /// Returns a <see langword="int"/>[,] representation of this <see cref="Map"/>.
        /// </summary>
        /// <returns></returns>
        public int[,] To2DIntArray()
        {
            int[,] arr = new int[Size.x, Size.y];
            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    if (Nodes[x, y] is null)
                    {
                        continue;
                    }
                    arr[x, y] = Nodes[x, y].IsFloor ? Node.FLOOR : Node.WALL;
                }
            }
            return arr;
        }
    }
}
