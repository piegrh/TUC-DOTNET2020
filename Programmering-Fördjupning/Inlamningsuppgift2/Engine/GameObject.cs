namespace Engine
{
    public abstract class GameObject
    {
        public Vector2 Position;
        public bool Active { get; set; } = true;
        public GameObject()
        {
            Position = Vector2.Zero;
        }
        public virtual void Update() { /* empty */ }
        public virtual void OnDestroy() { /* empty */ }
    }
}
