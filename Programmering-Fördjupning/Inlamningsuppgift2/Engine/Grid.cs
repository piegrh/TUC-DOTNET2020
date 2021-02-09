using System.Collections.Generic;

namespace Engine
{
    public class Grid<T> where T : GameObject
    {
        readonly List<T>[,] map;
        readonly Vector2 Size;
        public Grid(Vector2 size)
        {
            Size = size;
            map = new List<T>[Size.x, Size.y];
        }
        /// <summary>
        /// Returns <see cref="List{T}"/> at <paramref name="position"/>
        /// </summary>
        /// <param name="position"></param>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        /// <returns></returns>
        public List<T> Get(Vector2 position)
        {
            if (map[position.x, position.y] == null)
                map[position.x, position.y] = new List<T>();
            return map[position.x, position.y];
        }
        public void Clear()
        {
            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    if (map[x, y] != null)
                    {
                        map[x, y] = null;
                    }
                }
            }
        }
    }
}
