using MongoDB.Bson.Serialization;
using System;

namespace ByteCarrot.Mapongo
{
    public class MapongoClassMap : BsonClassMap
    {
        public MapongoClassMap(Type classType) : base(classType)
        {
            AutoMap();
        }
    }
}