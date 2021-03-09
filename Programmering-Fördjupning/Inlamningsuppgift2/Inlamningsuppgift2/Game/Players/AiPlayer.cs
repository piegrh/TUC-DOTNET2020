using Engine;
using Engine.Maps;
using Engine.Navigation;
using System;

namespace Inlamningsuppgift2.Game.Players
{
    public class AiPlayer : Player
    {
        protected Vector2 targetPosition;
        protected NavigationPath path = null;
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
                MoveDirection = Direction.None;
                return;
            }
            Vector2 nextPosition = path.NextPosition(Position);
            if (IsValidPath(nextPosition))
            {
                MoveDirection = Utils.Vector2ToDirection(nextPosition - Position);
                if (DestinationReached(nextPosition))
                {
                    ResetTarget();
                }
            } else
            {
                ResetTarget();
                MoveDirection = Utils.RandomDirection;
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
