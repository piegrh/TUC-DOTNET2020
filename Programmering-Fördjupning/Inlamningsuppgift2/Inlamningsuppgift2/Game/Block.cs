using Engine;
using System;

namespace Inlamningsuppgift2.Game
{
    public class Block : GameObject, IRenderable, ICollider
    {
        public char Character { get; set; } = '#';
        public bool Enable { get; set; } = true;
        public void OnCollision(GameObject gameObject) { /* empty */ }
        public bool IsRendered { get; set; } = false;
        public ConsoleColor BGColor { get; set; } = System.Console.BackgroundColor;
        public ConsoleColor FGColor { get; set; } = System.ConsoleColor.Gray;
    }
}
