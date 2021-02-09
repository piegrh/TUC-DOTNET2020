using Engine;
using Engine.Maps;
using Engine.Navigation;
using System;

namespace Inlamningsuppgift2.Game.Players
{
    public class AiPlayer : Player
    {
        protected Vector2 targetPosition;
        protected NavPath path = null;
        protected Map Map { get; private set; }
        protected bool DestinationReached(Vector2 position) => position == targetPosition;
        public AiPlayer(ConsoleColor color = ConsoleColor.Magenta)
        {
            Character = 'O';
            FGColor = color;
            GameEvents.OnGameStateUpdate += OnGameStateUpdate;
            App.onAppExit += OnExit;
        }
        void OnGameStateUpdate(Map map)
        {
            Map = map;
        }
        public override void Update()
        {
            path = PathFinder.GetPath(Map, Position, NotSnakeGame.NodeFood, out targetPosition);
            if (path is null)
            {
                // stand still if there is no food
                return;
            }
            Vector2 nextPos = path.Next(Position);
            if (IsValidPath(nextPos))
            {
                Dir = Utils.Vector2ToDirection(nextPos - Position);
                if (DestinationReached(nextPos))
                {
                    ResetTarget();
                }
            } else
            {
                ResetTarget();
                Dir = Utils.RandomDirection;
            }
        }
        protected virtual void ResetTarget()
        {
            path = null;
            targetPosition = PathFinder.Invalid;
        }
        protected virtual bool IsValidPath(Vector2 position)
        {
            if (position == PathFinder.Invalid || Map.GetNode(targetPosition).NodeType != NotSnakeGame.NodeFood)
            {
                return false;
            }
            return !Map.GetNode(position).IsWall;
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (!(path is null) && gameObject is Player)
            {
                ResetTarget();
            }
        }
        protected virtual void OnExit()
        {
            Map = null;
            UnSubscribe();
        }
        public override void OnDestroy()
        {
            OnExit();
        }
        protected void UnSubscribe()
        {
            GameEvents.OnGameStateUpdate -= OnGameStateUpdate;
            App.onAppExit -= OnExit;
        }
    }
}
