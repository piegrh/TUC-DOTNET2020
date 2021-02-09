namespace Engine
{
    /// <summary>
    /// Representation of 2D vectors and points
    /// </summary>
    public struct Vector2
    {
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 Down = new Vector2(0, 1);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 Up = Down * -1;
        public static readonly Vector2 Left = Right * -1;
        public int x;
        public int y;
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(Vector2 p)
        {
            x = p.x;
            y = p.y;
        }
        public override int GetHashCode() => base.GetHashCode();
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);
        public static Vector2 operator *(Vector2 a, int b) => new Vector2(a.x * b, a.y * b);
        public static Vector2 operator /(Vector2 a, int b) => new Vector2(a.x / b, a.y / b);
        public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);
        public static bool operator !=(Vector2 a, Vector2 b) => !(a.Equals(b));
        public override bool Equals(object obj)
        {
            if ((obj is null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            Vector2 vec = (Vector2)obj;
            return x == vec.x && y == vec.y;
        }
        public override string ToString() => $"({x},{y})";
    }
}
