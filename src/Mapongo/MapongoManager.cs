using MongoDB.Bson.Serialization;
using System;
using System.Linq;

namespace ByteCarrot.Mapongo
{
    public class MapongoManager : IMapongoManager
    {
        private MapongoConfiguration _configuration;

        public void Configure(Action<IMapongoConfiguration> action)
        {
            _configuration = new MapongoConfiguration();
            action(_configuration);

            foreach (var kvp in _configuration.Types)
            {
                var map = BsonClassMap.GetRegisteredClassMaps().SingleOrDefault(x => x.ClassType == kvp.Key);
                if (map == null)
                {
                    map = new MapongoClassMap(kvp.Key);
                    kvp.Value.ForEach(x => map.UnmapProperty(x.Name));
                    BsonClassMap.RegisterClassMap(map);
                }
                else if (!map.IsFrozen)
                {
                    kvp.Value.ForEach(x => map.UnmapProperty(x.Name));
                }
            }

            BsonSerializer.RegisterSerializationProvider(new MapongoSerializationProvider(_configuration));
        }
    }
}