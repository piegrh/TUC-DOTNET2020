using System.Collections.Generic;
namespace Engine
{
    public abstract class GameWorld 
    {
        public Vector2 Size { get; set; }
        public string GameInfo = "";
        Grid<GameObject> worldSpace;
        /// <summary>
        /// returns a <see cref="List{T}"/> of all the <see cref="GameObject"/> in this <see cref="GameWorld"/>
        /// </summary>
        public List<GameObject> Gameobjects { get; private set; }
        public GameWorld()
        {
            Gameobjects = new List<GameObject>();
            App.onAppExit += OnExit;
        }
        public virtual void Update()
        {
            UpdateWorldSpace();
            foreach (GameObject gameObject in Gameobjects)
            {
                if (!gameObject.Active)
                {
                    continue;
                }
                gameObject.Update();
                if (!(gameObject is DynamicObject))
                {
                    continue;
                }
                MoveDynamicGameObject(gameObject as DynamicObject);
            }
        }
        void MoveDynamicGameObject(DynamicObject gameObject)
        {
            Vector2 previousPosition = gameObject.Position;
            gameObject.Position += Utils.DirectionToVector2((gameObject as IMovable).Dir);
            if (gameObject.Position == previousPosition)
            {
                return; // Did not move.
            }
            Utils.Clamp(ref gameObject.Position, Vector2.Zero, Size); // prevent gameobjects from going out of bounds.
            if (ContainsActiveCollider(worldSpace.Get(gameObject.Position), out GameObject otherGameObject))
            {
                Collision(gameObject, otherGameObject);
                gameObject.Position = previousPosition;
            }
            // Update worldspace.
            worldSpace.Get(previousPosition).Remove(gameObject);
            worldSpace.Get(gameObject.Position).Add(gameObject);
        }
        void UpdateWorldSpace()
        {
            if (worldSpace is null)
            {
                worldSpace = new Grid<GameObject>(Size);
            }
            else
            {
                worldSpace.Clear();
            }
            foreach (GameObject go in Gameobjects)
            {
                if (go is ICollider)
                {
                    worldSpace.Get(go.Position).Add(go);
                }
            }
        }
        bool PositionIsFree(Vector2 p)
        {
            List<GameObject> gameObjectList = worldSpace == null ? Gameobjects : worldSpace.Get(p);
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                GameObject g = gameObjectList[i];
                if (g is ICollider && p == g.Position && g.Active)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Looks for a free position.
        /// </summary>
        /// <param name="p">Asign the free positon found to p</param>
        /// <param name="giveUp">Number of tries before giving up</param>
        /// <returns><see langword="True"/> if a free position was found, otherwise, <see langword="False"/></returns>
        public bool GetRandomFreePosition(out Vector2 p, int giveUp = 25)
        {
            int cnt = 0;
            while (cnt++ < giveUp)
            {
                p = new Vector2(App.rnd.Next(0, Size.x), App.rnd.Next(0, Size.y));
                if (PositionIsFree(p))
                {
                    return true;
                }
            }
            p = Vector2.Zero;
            return false;
        }
        static void Collision(GameObject a, GameObject b)
        {
            (b as ICollider)?.OnCollision(a);
            (a as ICollider)?.OnCollision(b);
        }
        protected virtual void OnExit()
        {
            if (!(worldSpace is null))
            {
                worldSpace.Clear();
                worldSpace = null;
            }
            if (!(Gameobjects is null))
            {
                Gameobjects.Clear();
                Gameobjects = null;
            }
            App.onAppExit -= OnExit;
        }
        /// <summary>
        /// Gets an array of <see cref="GameObject"/> of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Gameobject type</typeparam>
        /// <param name="world">Gameworld</param>
        /// <returns></returns>
        public T[] GetGameObjects<T>() where T : GameObject
        {
            List<T> list = new List<T>();
            for (int i = 0; i < Gameobjects.Count; i++)
            {
                if (Gameobjects[i] is T)
                {
                    list.Add(item: Gameobjects[i] as T);
                }
            }
            return list.ToArray();
        }
        protected virtual void Destroy(GameObject g)
        {
            Gameobjects.Remove(g);
            g.OnDestroy();
        }
        /// <summary>
        /// Checks if <paramref name="GameObjects"/> contains any active <see cref="GameObject"/>s that implements the <see cref="ICollider"/> interface.
        /// </summary>
        /// <param name="GameObjects"></param>
        /// <param name="activeCollider"></param>
        /// <returns><see langword=""="True"/> if a ICollider was found, otherwise <see langword=""="null"/></returns>
        protected bool ContainsActiveCollider(List<GameObject> GameObjects, out GameObject activeCollider)
        {
            activeCollider = null;
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject temp = GameObjects[i];
                if (temp is ICollider && temp.Active)
                {
                    activeCollider = temp;
                    return true;
                }
            }
            return false;
        }
    }
}
