namespace Engine.Navigation
{
    public class NavigationPath
    {
        readonly int[] arr;
        readonly Vector2 size;
        /// <summary>
        /// <see cref="NavigationPath"/> Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="list"></param>
        public NavigationPath(Vector2 size, int[] arr)
        {
            this.arr = arr ?? new int[0];
            this.size = size;
        }
        /// <summary>
        /// Gets the postion of the next hop.
        /// </summary>
        /// <param name="src"></param>
        /// <returns><see cref="PathFinder.Invalid"/> if there is no next hop, otherwise the next <see cref="Vector2"/> position to hop to</returns>
        public Vector2 NextPosition(Vector2 src)
        {
            return NextPosition(src.x + (size.x * src.y));
        }
        /// <summary>
        /// Gets the postion of the next hop.
        /// </summary>
        /// <param name="srcIndex"></param>
        /// <returns><see cref="PathFinder.Invalid"/> if there is no next hop, otherwise the next <see cref="Vector2"/> position to hop to</returns>
        public Vector2 NextPosition(int srcIndex)
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
