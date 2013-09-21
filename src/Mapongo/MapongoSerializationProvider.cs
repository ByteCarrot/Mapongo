using MongoDB.Bson.Serialization;
using System;

namespace ByteCarrot.Mapongo
{
    public class MapongoSerializationProvider : IBsonSerializationProvider
    {
        private readonly IMapongoContext _context;

        public MapongoSerializationProvider(IMapongoContext context)
        {
            _context = context;
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            return _context.Types.ContainsKey(type) ? new MapongoSerializer(type, _context.Types[type]) : null;
        }
    }
}