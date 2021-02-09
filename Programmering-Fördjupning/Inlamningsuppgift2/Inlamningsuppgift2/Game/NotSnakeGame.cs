using Engine;
using Engine.Maps;
using Inlamningsuppgift2.Game.Players;
using System.Collections.Generic;

namespace Inlamningsuppgift2.Game
{
    public class NotSnakeGame : GameWorld
    {
        public const int NodeHuman = -10;
        public const int NodeAI = -11;
        public const int NodeFood = 10;
        const int GiveUp = 100;
        const int TimeLimit = 15;
        Dictionary<Player, float> lastMealTracker;
        Player human;
        Map map;
        int playerScore = 0;
        int foodCollected = 0;
        /// <summary>
        /// <see cref="NotSnakeGame"/> Constructor
        /// </summary>
        /// <param name="size">Size of the world</param>
        /// <param name="foodCnt"><see cref="Food"/> amount</param>
        /// <param name="AIPlayers">Number of <see cref="AiPlayer"/>s</param>
        /// <param name="humanPlayer">If <see langword="true"/> a controllable <see cref="Player"/> will get spawned.</param>
        public NotSnakeGame(Map map, bool humanPlayer = true, int foodCnt = 2, int AIPlayers = 1)
        {
            this.map = map;
            Size = map.Size;
            lastMealTracker = new Dictionary<Player, float>();
            Gameobjects.AddRange(new NotSnakeMapBuilder().Build(this.map));
            if (humanPlayer)
            {
                human = new Player { };
                if (GetRandomFreePosition(out human.Position, GiveUp))
                {
                    Gameobjects.Add(human);
                    lastMealTracker.Add(human, 0);
                }
                else
                {
                    App.Quit("Unable to add player.");
                    return;
                }
            }
            CreateAIPlayer(AIPlayers);
            CreateFood(foodCnt);
            App.onAppExit += OnExit;
            GameEvents.OnFoodPickup += OnFoodPickupEvent;
        }
        void CreateFood(int foodCnt)
        {
            for (int i = 0; i < foodCnt; i++)
            {
                Food f = new Food();
                if (GetRandomFreePosition(out f.Position, GiveUp))
                {
                    Gameobjects.Add(f);
                }
            }
        }
        void CreateAIPlayer(int AIPlayers)
        {
            for (int i = 0; i < AIPlayers; i++)
            {
                Player AI = new AiPlayer();
                if (GetRandomFreePosition(out AI.Position, GiveUp))
                {
                    Gameobjects.Add(AI);
                    lastMealTracker.Add(AI, 0);
                }
            }
        }
        public override void Update()
        {
            SendGameStateUpdate();
            base.Update();
            RespawnInactiveFood();
            StarvePlayers();
            UpdateGameInfoText();
            if (IsGameOver())
            {
                App.Quit($"Total food collected: {foodCollected}.");
            }
        }
        bool IsGameOver() => lastMealTracker.Count == 0;
        void SendGameStateUpdate()
        {
            Map m = new Map(map);
            // Mark players locations
            foreach (Player p in this.GetGameObjects<Player>())
            {
                m.Nodes[p.Position.x, p.Position.y] = new Map.Node(p.Position, (p is AiPlayer) ? NodeAI : NodeHuman);
            }
            // Mark food location
            foreach (Food f in this.GetGameObjects<Food>())
            {
                m.Nodes[f.Position.x, f.Position.y] = new Map.Node(f.Position, NodeFood);
            }
            GameEvents.GameStateUpdate(m);
        }
        void UpdateGameInfoText()
        {
            if (human is null)
            {
                GameInfo = $"Time: {App.time}, AI:{lastMealTracker.Count}, Food collected:{foodCollected}";
            }
            else if (human.Active)
            {
                GameInfo = $"Time: {App.time}, Score:{playerScore}p, Timer: {(int)System.Math.Clamp(TimeLimit - (App.time.SecondsSinceStart - lastMealTracker[human]), 0, int.MaxValue)}s";
            }
            else
            {
                GameInfo = $"Time: {App.time}, Final Score:{playerScore}, --DEAD-- Press Q to exit.";
            }
        }
        void RespawnInactiveFood()
        {
            Food[] food = GetGameObjects<Food>();
            foreach (Food f in food)
            {
                if (!f.Active)
                {
                   GetRandomFreePosition(out f.Position);
                   f.Active = true;
                }
            }
        }
        void StarvePlayers()
        {
            List<Player> removeUs = new List<Player>();
            foreach (var keyValuePar in lastMealTracker)
            {
                float time = App.time.SecondsSinceStart - keyValuePar.Value;
                if (time > TimeLimit)
                {
                    keyValuePar.Key.Active = false;
                    removeUs.Add(keyValuePar.Key);
                }
            }
            foreach (Player p in removeUs)
            {
                lastMealTracker.Remove(p);
                Destroy(p);
            }
        }

        void OnFoodPickupEvent(Player player, Food f)
        {
            lastMealTracker[player] = App.time.SecondsSinceStart;
            foodCollected++;
            if (!(player is AiPlayer))
            {
                playerScore++;
            }
        }
        protected override void OnExit()
        {
            base.OnExit();
            if (!(map is null))
            {
                map.Clear();
                map = null;
            }
            if (!(lastMealTracker is null))
            {
                lastMealTracker.Clear();
                lastMealTracker = null;
            }
            human = null;
            GameEvents.OnFoodPickup -= OnFoodPickupEvent;
        }
    }
}
