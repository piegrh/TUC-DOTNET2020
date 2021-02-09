namespace Engine
{
    public interface IRenderable
    {
        /// <summary>
        /// Character to be rendered
        /// </summary>
        public char Character { get; set; }
        /// <summary>
        /// Render background <see cref="System.ConsoleColor"/>
        /// </summary>
        public System.ConsoleColor BGColor { get; set; }
        /// <summary>
        /// Render foreground <see cref="System.ConsoleColor"/>
        /// </summary>
        public System.ConsoleColor FGColor { get; set; }
        /// <summary>
        /// Rendered flag
        /// </summary>
        public bool IsRendered { get; set; }
    }
}
