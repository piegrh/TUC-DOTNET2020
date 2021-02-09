namespace Engine.Navigation
{
    public class NavPath
    {
        readonly int[] arr;
        readonly Vector2 size;
        /// <summary>
        /// <see cref="NavPath"/> Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="list"></param>
        public NavPath(Vector2 size, int[] arr)
        {
            this.arr = arr ?? new int[0];
            this.size = size;
        }
        /// <summary>
        /// Gets the postion of the next hop.
        /// </summary>
        /// <param name="src"></param>
        /// <returns><see cref="PathFinder.Invalid"/> if there is no next hop, otherwise the next <see cref="Vector2"/> position to hop to</returns>
        public Vector2 Next(Vector2 src)
        {
            return Next(src.x + (size.x * src.y));
        }
        /// <summary>
        /// Gets the postion of the next hop.
        /// </summary>
        /// <param name="srcIndex"></param>
        /// <returns><see cref="PathFinder.Invalid"/> if there is no next hop, otherwise the next <see cref="Vector2"/> position to hop to</returns>
        public Vector2 Next(int srcIndex)
        {
            int destIndex = PathFinder.UNVISITED;
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                if (arr[i] == srcIndex)
                {
                    destIndex = arr[--i];
                }
            }
            return destIndex == PathFinder.UNVISITED ? PathFinder.Invalid : new Vector2(destIndex % size.x, destIndex / size.x);
        }
    }
}
