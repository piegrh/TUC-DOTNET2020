using System.Collections.Generic;

namespace Engine
{
    public static class CollisionDetectionHelper 
    {
        /// <summary>
        /// Checks if <paramref name="GameObjects"/> contains any active <see cref="GameObject"/>s that implements the <see cref="ICollider"/> interface.
        /// </summary>
        /// <param name="GameObjects"></param>
        /// <param name="colliedWith"></param>
        /// <returns><see langword=""="True"/> if a ICollider was found, otherwise <see langword=""="null"/></returns>
        public static bool ContainsActiveCollider(List<GameObject> GameObjects, out GameObject colliedWith)
        {
            colliedWith = null;
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject temp = GameObjects[i];
                if (temp is ICollider && temp.Active)
                {
                    colliedWith = temp;
                    return true;
                }
            }
            return false;
        }
    }
}
