using Engine;
using System;

namespace Inlamningsuppgift2.Game.Players
{
    public class Player : DynamicObject, ICollider, IRenderable
    {
        public char Character { get; set; } = 'X';
        public ConsoleColor BGColor { get; set; } = Console.BackgroundColor;
        public ConsoleColor FGColor { get; set; } = ConsoleColor.Green;
        public bool IsRendered { get; set; } = false;
        public virtual void OnCollision(GameObject gameObject) { /* empty */ }
        // VS Studio wanted me to format this codeblock like this :(
        public override void Update() => MoveDirection = Input.GetKey() switch
        {
            ConsoleKey.W => Direction.Up,
            ConsoleKey.S => Direction.Down,
            ConsoleKey.A => Direction.Left,
            ConsoleKey.D => Direction.Right,
            _ => MoveDirection,
        };
    }
}
