using System;
namespace Engine
{
    public class Renderer
    {
        readonly GameWorld world;
        const int offsetY = 1;
        public bool UseColor { get; set; } = true;
        public Renderer(GameWorld world)
        {
            this.world = world;
            Console.CursorVisible = false;
            try
            {
                Console.SetWindowSize(world.Size.x, world.Size.y + offsetY);
                Console.SetBufferSize(world.Size.x, world.Size.y + offsetY);
            }
            catch (Exception e)
            {
                App.Quit(e.Message);
                return;
            }
            Console.CursorVisible = false;
        }
        /// <summary>
        /// Clears first row then blanks out all <see cref="DynamicObject"/> and flags them as not rendered,
        /// so that they will get rendered again in <see cref="Render()"/>
        /// <para>This should be called before <see cref="Render()"/></para>
        /// </summary>
        public void RenderBlank()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(' ', world.Size.x));
            foreach (GameObject go in world.Gameobjects)
            {
                if (!go.Active)
                {
                    continue;
                }
                if (go is DynamicObject)
                {
                    (go as IRenderable).IsRendered = false;
                    Console.SetCursorPosition(go.Position.x, go.Position.y + offsetY);
                    Console.Write(' ');
                }
            }
        }
        /// <summary>
        /// Render <see cref="IRenderable"/> active <see cref="GameObject"/>s that are not yet rendered.
        /// </summary>
        public void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(world.GameInfo);
            ConsoleColor fg = Console.ForegroundColor;
            ConsoleColor bg = Console.BackgroundColor;
            foreach (GameObject go in world.Gameobjects)
            {
                if (!go.Active)
                {
                    continue;
                }
                if (go is IRenderable && !(go as IRenderable).IsRendered)
                {
                    RenderObject(go, UseColor);
                }
            }
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
        }
        /// <summary>
        /// Render <paramref name="go"/> in the console
        /// if <paramref name="go"/> implements <seealso cref="IRenderable"/>.
        /// <para>
        /// If  <paramref name="go"/> gets rendered it will be flagged as rendered.
        /// </para>
        /// </summary>
        /// <param name="go">GameObject to be rendered</param>
        /// <param name="useColors">Flags whether or not <see cref="Renderer"/> should use colors</param>
        static void RenderObject(GameObject go, bool useColors)
        {
            Console.SetCursorPosition(go.Position.x, go.Position.y + offsetY);
            var iRendable = (go as IRenderable);
            if (useColors)
            {
                Console.ForegroundColor = iRendable.FGColor;
                Console.BackgroundColor = iRendable.BGColor;
            }
            Console.Write(iRendable.Character);
            iRendable.IsRendered = true;
        }
    }
}
