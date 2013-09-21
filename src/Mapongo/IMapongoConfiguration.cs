using MongoDB.Driver;
using System;
using System.Reflection;

namespace ByteCarrot.Mapongo
{
    public interface IMapongoConfiguration
    {
        void Scan(Assembly assembly, Func<Type, bool> func);
        
        void Connect(MongoDatabase database);
    }
}