using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteCarrot.Mapongo
{
    public class MapongoConfiguration : IMapongoConfiguration, IMapongoContext
    {
        private MongoDatabase _database;
        
        public Dictionary<Type, List<MemberInfo>> Types { get; private set; }

        public void Scan(Assembly assembly, Func<Type, bool> func)
        {
            var types = assembly.GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract && (!typeof (Array).IsAssignableFrom(x) && 
                            !typeof (Enum).IsAssignableFrom(x)) && func(x));

            Types = new Dictionary<Type, List<MemberInfo>>();

            foreach (var type in types)
            {
                var list = ScanProperties(type);
                if (list.Count == 0)
                {
                    continue;
                }

                Types.Add(type, list);
            }
        }

        public void Connect(MongoDatabase database)
        {
            _database = database;
        }

        private static List<MemberInfo> ScanProperties(Type type)
        {
            var list = new List<MemberInfo>();
            var members = type.GetMembers();
            foreach (var member in members)
            {
                var attribute = member.GetCustomAttribute<MongoReferenceAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                list.Add(member);
            }
            return list;
        }
    }
}