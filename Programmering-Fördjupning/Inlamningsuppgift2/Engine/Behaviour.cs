namespace Engine
{
    public interface IBehaviour
    {
        /// <summary>
        /// Gets called once every frame from <see cref="App.Run(GameWorld, bool)"/>
        /// </summary>
        void Update();
    }
}
