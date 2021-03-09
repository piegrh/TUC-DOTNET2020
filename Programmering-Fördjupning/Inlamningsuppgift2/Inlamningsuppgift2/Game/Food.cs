using Engine;
using Inlamningsuppgift2.Game.Players;
using System;
namespace Inlamningsuppgift2.Game
{
    public class Food : DynamicObject, IRenderable, ICollider
    {
        protected static int RandomResetTime => App.rnd.Next(10, 30);
        protected float timer;
        protected int resetTime;
        public char Character { get; set; } = '*';
        public ConsoleColor BGColor { get; set; } = Console.BackgroundColor;
        public ConsoleColor FGColor { get; set; } = ConsoleColor.Yellow;
        public bool IsRendered { get; set; } = false;
        public Food()
        {
            resetTime = RandomResetTime;
            MoveDirection = Direction.None;
            timer = 0;
        }
        public void OnCollision(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                GameEvents.FoodPickup(gameObject as Player, this);
                Active = false;
            }
        }
        public override void Update()
        {
            if ((timer += App.time.DeltaTime) > resetTime)
            { 
                // Deactivate
                Active = false;
                timer = 0;
                resetTime = RandomResetTime;
            }
        }
    }
}
