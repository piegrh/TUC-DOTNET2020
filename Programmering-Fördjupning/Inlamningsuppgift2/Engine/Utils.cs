namespace Engine
{
    public static class Utils
    {
        /// <summary>
        /// Gets a random <see cref="Direction"/>. 
        /// <para>
        /// Directions {<see cref="Direction.Up"/>,<see cref="Direction.Down"/>,<see cref="Direction.Left"/>,<see cref="Direction.Right"/>}
        /// </para>
        /// </summary>
        public static Direction RandomDirection => (Direction)App.rnd.Next(1, 5);
        /// <summary>
        /// Checks whether <paramref name="n"/> is within the given range. [<paramref name="LowerBounds"/>, <paramref name="UpperBounds"/>]
        /// <para>If <paramref name="LowerBounds"/> is greater than <paramref name="UpperBounds"/> the values will get swapped</para>
        /// </summary>
        /// <param name="n"></param>
        /// <param name="LowerBounds"></param>
        /// <param name="UpperBounds"></param>
        /// <returns></returns>
        public static bool Between<T>(T n, T LowerBounds, T UpperBounds) where T : System.IComparable
        {
            if (LowerBounds.CompareTo(UpperBounds) > 0)
            {
                Swap(ref LowerBounds, ref UpperBounds);
            }
            return n.CompareTo(LowerBounds) >= 0 && n.CompareTo(UpperBounds) < 1;
        }
        /// <summary>
        /// Swap valus of <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        /// <summary>
        /// Takes a <see cref="Direction"/> and returns a corresponding <see cref="Vector2"/>
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static Vector2 DirectionToVector2(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return Vector2.Up;
                case Direction.Down: return Vector2.Down;
                case Direction.Left: return Vector2.Left;
                case Direction.Right: return Vector2.Right;
                case Direction.None: break;
            }
            return Vector2.Zero;
        }
        /// <summary>
        /// Takes a <see cref="Vector2"/> and returns the corresponding <see cref="Direction"/>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Direction Vector2ToDirection(Vector2 p)
        {
            if (p == Vector2.Up)
                return Direction.Up;
            if (p == Vector2.Down)
                return Direction.Down;
            if (p == Vector2.Left)
                return Direction.Left;
            if (p == Vector2.Right)
                return Direction.Right;
            return Direction.None;
        }
        /// <summary>
        /// Clamps the given value <paramref name="n"/> between the given [<paramref name="min"/>,<paramref name="max"/>] values.
        /// </summary>
        /// <param name="n">a number to be clamped</param>
        /// <param name="min">lower bounds, inclusive</param>
        /// <param name="max">upper bounds, inclusive</param>
        /// <returns></returns>
        public static T Clamp<T>(T n, T min, T max) where T : System.IComparable 
            => n.CompareTo(max) > 0 ? max : n.CompareTo(min) < 0 ? min : n;
        /// <summary>
        /// Clamps <paramref name="p"/>. [<paramref name="lowerBounds"/>, <paramref name="upperBounds"/>[
        /// </summary>
        /// <param name="p"></param>
        /// <param name="lowerBounds">Inclusive</param>
        /// <param name="upperBounds">Exclusive</param>
        public static void Clamp(ref Vector2 p, Vector2 lowerBounds, Vector2 upperBounds)
        {
            p.x = Clamp(p.x, lowerBounds.x, upperBounds.x - 1);
            p.y = Clamp(p.y, lowerBounds.y, upperBounds.y - 1);
        }
        /// <summary>
        /// Checks if <paramref name="p"/> is a point inside <paramref name="bounds"/>
        /// </summary>
        /// <param name="p"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static bool IsInBounds(Vector2 p, Vector2 bounds)
        {
            return Between(p.x, 0, bounds.x - 1) && Between(p.y, 0, bounds.y - 1);
        }
    }
}
