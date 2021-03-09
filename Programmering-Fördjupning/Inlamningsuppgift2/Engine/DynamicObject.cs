namespace Engine
{
    public abstract class DynamicObject : GameObject, IMovable
    {
        /// <summary>
        /// Move Direction
        /// </summary>
        public Direction MoveDirection { get; set; }
    }
}
