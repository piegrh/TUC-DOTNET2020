using Inlamningsuppgift2.Game.Players;

namespace Inlamningsuppgift2.Game
{
    public class GameEvents
    {
        /// <summary>
        /// Food pickup event
        /// </summary>
        public static System.Action<Player, Food> OnFoodPickup;
        /// <summary>
        /// Send gamestate to AIs
        /// </summary>
        public static System.Action<Engine.Maps.Map> OnGameStateUpdate;
        /// <summary>
        /// Send food pickup event
        /// </summary>
        /// <param name="p"></param>
        /// <param name="f"></param>
        public static void FoodPickup(Player p, Food f) => OnFoodPickup?.Invoke(p, f);
        /// <summary>
        /// Send gamestate update event
        /// </summary>
        /// <param name="map"></param>
        /// <param name="food"></param>
        public static void GameStateUpdate(Engine.Maps.Map map) => OnGameStateUpdate?.Invoke(map);
    }
}
