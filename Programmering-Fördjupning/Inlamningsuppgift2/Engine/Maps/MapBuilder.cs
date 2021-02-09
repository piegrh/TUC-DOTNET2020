namespace Engine.Maps
{
    public abstract class MapBuilder
    {
        /// <summary>
        /// Creates an array of <see cref="GameObject"/>s from a given <see cref="Map"/>.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract GameObject[] Build(Map map);
    }
}
