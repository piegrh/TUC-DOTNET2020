using Engine.Maps;
using Engine.Navigation;
using Xunit;

namespace Engine.UnitTests
{
    public class Tests
    {
        [Theory]
        [InlineData(4, 4, 4, 4)]
        [InlineData(34, 34, 34, 34)]
        public void Vector2_Equal_IsTrue(int ax, int ay, int bx, int by)
        {
            Vector2 a = new Vector2(ax, ay);
            Vector2 b = new Vector2(bx, by);
            Assert.True(a == b);
        }
        [Theory]
        [InlineData(1, 2, 3, 4)]
        public void Vector2_Equal_IsFalse(int ax, int ay, int bx, int by)
        {
            Vector2 a = new Vector2(ax, ay);
            Vector2 b = new Vector2(bx, by);
            Assert.False(a == b);
        }
        [Theory]
        [InlineData(1, 2, 3, 4)]
        public void Vector2_NotEqual_IsTrue(int ax, int ay, int bx, int by)
        {
            Vector2 a = new Vector2(ax, ay);
            Vector2 b = new Vector2(bx, by);
            Assert.True(a != b);
        }
        [Theory]
        [InlineData(1, 2, 1, 2, 2, 4)]
        public void Vector2_Additon(int ax, int ay, int bx, int by, int resultx, int resulty)
        {
            Vector2 a = new Vector2(ax, ay);
            Vector2 b = new Vector2(bx, by);
            Vector2 expected = new Vector2(resultx, resulty);
            Assert.Equal(expected, a + b);
        }
        [Theory]
        [InlineData(8, 4, 3, 3, 5, 1)]
        public void Vector2_Subtraction(int ax, int ay, int bx, int by, int resultx, int resulty)
        {
            Vector2 a = new Vector2(ax, ay);
            Vector2 b = new Vector2(bx, by);
            Vector2 expected = new Vector2(resultx, resulty);
            Assert.Equal(expected, a - b);
        }
        [Fact]
        public void Vector2_Division_Scalar()
        {
            Vector2 a = new Vector2(2, 2);
            Vector2 expected = new Vector2(1, 1);
            Assert.Equal(expected, a / 2);
        }
        [Theory]
        [InlineData(1, 2, 10, 10, 20)]
        public void Vector2_Multiplication_Scalar(int ax, int ay, int s, int resultx, int resulty)
        {
            Vector2 a = new Vector2(ax, ay);
            Vector2 expected = new Vector2(resultx, resulty);
            Assert.Equal(expected, a * s);
        }
    // =================================================== Utils
    [Theory]
        [InlineData(-1, 0, 10, 0)]
        [InlineData(0, 0, 10, 0)]
        [InlineData(10, 0, 10, 10)]
        [InlineData(5, 0, 10, 5)]
        [InlineData(11, 0, 10, 10)]
        public void Utils_Clamp_Int(int n, int min, int max, int expected)
        {
            Assert.Equal(expected, Utils.Clamp(n, min, max));
        }
        [Fact]
        public void Utils_Clamp_Vector2()
        {
            Vector2 mapSize = new Vector2(10, 10);

            Vector2 vec2 = new Vector2(10, 10);
            Utils.Clamp(ref vec2, Vector2.Zero, mapSize);
            Assert.Equal(new Vector2(9, 9), vec2);

            vec2 = new Vector2(9, 9);
            Utils.Clamp(ref vec2, Vector2.Zero, mapSize);
            Assert.Equal(new Vector2(9, 9), vec2);

            vec2 = new Vector2(0, 0);
            Utils.Clamp(ref vec2, Vector2.Zero, mapSize);
            Assert.Equal(new Vector2(0, 0), vec2);

            vec2 = new Vector2(-1, -1);
            Utils.Clamp(ref vec2, Vector2.Zero, mapSize);
            Assert.Equal(new Vector2(0, 0), vec2);
        }
        [Fact]
        public void Utils_Between()
        {
            //true
            Assert.True(Utils.Between(10, 0, 10));
            Assert.True(Utils.Between(5, 0, 10));
            Assert.True(Utils.Between(0, 0, 10));
            //false
            Assert.False(Utils.Between(-1, 0, 10));
        }
        [Fact]
        public void Utils_DirectionToPoint()
        {
            Assert.Equal(Vector2.Up, Utils.DirectionToVector2(Direction.Up));
            Assert.Equal(Vector2.Down, Utils.DirectionToVector2(Direction.Down));
            Assert.Equal(Vector2.Left, Utils.DirectionToVector2(Direction.Left));
            Assert.Equal(Vector2.Right, Utils.DirectionToVector2(Direction.Right));
            Assert.Equal(Vector2.Zero, Utils.DirectionToVector2(Direction.None));
        }
        [Fact]
        public void Utils_PointToDirection()
        {
            Assert.Equal(Direction.Up, Utils.Vector2ToDirection(Vector2.Up));
            Assert.Equal(Direction.Down, Utils.Vector2ToDirection(Vector2.Down));
            Assert.Equal(Direction.Left, Utils.Vector2ToDirection(Vector2.Left));
            Assert.Equal(Direction.Right, Utils.Vector2ToDirection(Vector2.Right));
            Assert.Equal(Direction.None, Utils.Vector2ToDirection(Vector2.Zero));
            Assert.Equal(Direction.None, Utils.Vector2ToDirection(new Vector2(4, 4)));
        }
        [Fact]
        public void Utils_Swap()
        {
            int a = 100;
            int b = 0;
            Utils.Swap(ref a, ref b);
            Assert.Equal(0, a);
            Assert.Equal(100, b);
        }
        //========================================================== Map
        [Fact]
        public void Map_Copy()
        {
            Vector2 size = new Vector2(10, 10);
            Map mapA = new Map(size);
            Map mapB = new Map(mapA);
            Assert.Equal(mapA.Size, mapB.Size);
            for (int y = 0; y < mapA.Size.y; y++)
            {
                for (int x = 0; x < mapA.Size.x; x++)
                {
                    Assert.NotEqual(mapA.Nodes[x, y], mapB.Nodes[x, y]);
                    Assert.Equal(mapA.Nodes[x, y].NodeType, mapB.Nodes[x, y].NodeType);
                    Assert.Equal(mapA.Nodes[x, y].position, mapB.Nodes[x, y].position);
                }
            }
        }
        [Fact]
        public void Map_Size_Equal()
        {
            Vector2 size = new Vector2(10, 10);
            Map map = new Map(size);
            Assert.Equal(size, map.Size);
            Assert.Equal(size.x, map.Nodes.GetLength(0));
            Assert.Equal(size.y, map.Nodes.GetLength(1));
        }
        [Fact]
        public void Map_Get()
        {
            Vector2 size = new Vector2(10, 10);
            Map lvl = new Map(size);
            Map.Node node = new Map.Node(new Vector2(1, 1));
            lvl.Nodes[1, 1] = node;
            Assert.Equal(node, lvl.GetNode(new Vector2(1, 1)));
        }
        [Fact]
        public void Level_TryGet_true()
        {
            Vector2 size = new Vector2(10, 10);
            Map map = new Map(size);
            Map.Node node = new Map.Node(new Vector2(1, 1));
            map.Nodes[1, 1] = node;
            Assert.True(map.TryGetNode(new Vector2(1, 1), out Map.Node n) && node == n);
        }
        [Fact]
        public void Map_TryGet_IsNull()
        {
            Vector2 size = new Vector2(10, 10);
            Map map = new Map(size);
            Assert.Null(map.TryGetNode(new Vector2(20, 20), out Map.Node n) ? n : n);
        }
        [Fact]
        public void Map_GetNeighbors()
        {
            Vector2 size = new Vector2(10, 10);
            Map map = new Map(size);

            Vector2 p = new Vector2(1, 1);

            Map.Node up = new Map.Node(new Vector2(1, 0));
            Map.Node down = new Map.Node(new Vector2(1, 2));
            Map.Node left = new Map.Node(new Vector2(0, 1));
            Map.Node right = new Map.Node(new Vector2(2, 1));

            map.Nodes[up.position.x, up.position.y] = up;
            map.Nodes[down.position.x, down.position.y] = down;
            map.Nodes[left.position.x, left.position.y] = left;
            map.Nodes[right.position.x, right.position.y] = right;

            Assert.Equal(up, map.GetNeighbor(p, Direction.Up, out Map.Node n) ? n : n);
            Assert.Equal(down, map.GetNeighbor(p, Direction.Down, out n) ? n : n);
            Assert.Equal(left, map.GetNeighbor(p, Direction.Left, out n) ? n : n);
            Assert.Equal(right, map.GetNeighbor(p, Direction.Right, out n) ? n : n);
        }
        [Fact]
        public void Map_IsInBounds()
        {
            Vector2 size = new Vector2(10, 10);

            Vector2 a = new Vector2(5, 5);
            Vector2 b = new Vector2(0, 0);
            Vector2 c = new Vector2(3, 9);

            // True
            Assert.True(Utils.IsInBounds(a, size));
            Assert.True(Utils.IsInBounds(b, size));
            Assert.True(Utils.IsInBounds(c, size));

            Vector2 d = new Vector2(10, 10);
            Vector2 e = new Vector2(11, 11);
            Vector2 f = new Vector2(-1, -2);

            // False
            Assert.False(Utils.IsInBounds(d, size));
            Assert.False(Utils.IsInBounds(e, size));
            Assert.False(Utils.IsInBounds(f, size));
        }
        [Fact]
        public void Map_GetIndex()
        {
            Vector2 size = new Vector2(10, 10);
            Map map = new Map(size);

            Vector2 a = new Vector2(0, 0);
            Vector2 b = new Vector2(5, 0);
            Vector2 c = new Vector2(5, 5);
            Vector2 d = new Vector2(5, 8);

            Assert.Equal(0, map.GetIndexOf(a));
            Assert.Equal(5, map.GetIndexOf(b));
            Assert.Equal(55, map.GetIndexOf(c));
            Assert.Equal(85, map.GetIndexOf(d));
        }
        //========================================================== Collision
        private class TestGameObject : GameObject, ICollider
        {
            public void OnCollision(GameObject go) { /* EMPTY */}
        }
        [Fact]
        public void CollisionDetectionHelper_ContainsActiveCollider_IsTrue()
        {
            Grid<GameObject> grid = new Grid<GameObject>(new Vector2(10,10));
            TestGameObject a = new TestGameObject() { Position = new Vector2(5, 5) };
            grid.Get(a.Position).Add(a);
            bool result = CollisionDetectionHelper.ContainsActiveCollider(grid.Get(new Vector2(5, 5)), out GameObject collision);            
            Assert.Equal(a, collision);
            Assert.True(result);
        }
        [Fact]
        public void CollisionDetectionHelper_ContainsActiveCollider_IsFalse()
        {
            Grid<GameObject> grid = new Grid<GameObject>(new Vector2(10, 10));
            TestGameObject a = new TestGameObject() { Position = new Vector2(5, 5) };
            a.Active = false; // Inactive GameObjects should not collid with other colliders
            grid.Get(a.Position).Add(a);
            bool result = CollisionDetectionHelper.ContainsActiveCollider(grid.Get(new Vector2(5, 5)), out GameObject collision);
            Assert.Null(collision);
            Assert.False(result);
        }
        //========================================================== LevelGenerator
        [Fact]
        public void MapGenerator_FromIntArray()
        {
            Map room = MapGenerator.SimpleRoom(new Vector2(10, 10));
            Map room2 = MapGenerator.CreateFrom2DArray(room.To2DIntArray());
            for (int y = 0; y < room.Size.y; y++)
            {
                for (int x = 0; x < room.Size.x; x++)
                {
                    Assert.Equal(room.Nodes[x, y].NodeType, room2.Nodes[x, y].NodeType);
                }
            }
        }
        [Fact]
        public void MapGenerator_SimpleRoom()
        {
            Map map = MapGenerator.SimpleRoom(new Vector2(5, 3));

            //0. #####
            //1. #   #
            //2. #####

            // Row 0
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(0, 0)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(1, 0)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(2, 0)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(3, 0)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(4, 0)).NodeType);

            // Row 1
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(0, 1)).NodeType);
            Assert.Equal(Map.Node.FLOOR, map.GetNode(new Vector2(1, 1)).NodeType);
            Assert.Equal(Map.Node.FLOOR, map.GetNode(new Vector2(2, 1)).NodeType);
            Assert.Equal(Map.Node.FLOOR, map.GetNode(new Vector2(3, 1)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(4, 1)).NodeType);

            // Row 2
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(0, 2)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(1, 2)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(2, 2)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(3, 2)).NodeType);
            Assert.Equal(Map.Node.WALL, map.GetNode(new Vector2(4, 2)).NodeType);
        }
        [Fact]
        public void MapGenerator_SanityCheck()
        {
            Map map = MapGenerator.RandomDungeon(new Vector2(100, 80));
            for (int y = 0; y < map.Size.y; y++)
            {
                for (int x = 0; x < map.Size.x; x++)
                {
                    Assert.Equal(new Vector2(x, y), map.Nodes[x, y].position);
                }
            }
        }
        //========================================================== Node
        [Fact]
        public void Node_IsWall_True()
        {
            Map.Node n = new Map.Node(new Vector2(0, 0), Map.Node.WALL);
            Assert.True(n.IsWall);
        }
        [Fact]
        public void Node_IsWall_False()
        {
            Map.Node n = new Map.Node(new Vector2(0, 0), Map.Node.FLOOR);
            Assert.False(n.IsWall);
            n = new Map.Node(new Vector2(0, 0), 243563);
            Assert.False(n.IsWall);
            n = new Map.Node(new Vector2(0, 0), 0);
            Assert.False(n.IsWall);
        }
        //========================================================== PathFinder
        [Fact]
        public void PathFinder_GetPath_InvalidPath()
        {
            int TargetNodeType = 9;
            Map m = MapGenerator.SimpleRoom(new Vector2(10, 10));
            PathFinder.GetPath(m, new Vector2(5, 5), TargetNodeType, out Vector2 result);
            Assert.Equal(PathFinder.Invalid, result);
        }
        [Fact]
        public void PathFinder_GetPath_ValidPath()
        {
            int TargetNodeType = 9;
            Vector2 expectedTargetPos = new Vector2(7, 7);
            Map m = MapGenerator.SimpleRoom(new Vector2(10, 10));

            m.GetNode(expectedTargetPos).NodeType = TargetNodeType; // Nearest target
            m.GetNode(new Vector2(9,9)).NodeType = TargetNodeType; 

            PathFinder.GetPath(m, new Vector2(5, 5), TargetNodeType, out Vector2 result);
            Assert.NotEqual(PathFinder.Invalid, result);
            Assert.Equal(expectedTargetPos, result);
        }
    }
}
