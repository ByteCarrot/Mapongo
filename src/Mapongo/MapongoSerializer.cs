using LinFu.DynamicProxy;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteCarrot.Mapongo
{
    public class MapongoSerializer : IBsonIdProvider, IBsonDocumentSerializer
    {
        private static readonly ProxyFactory Factory = new ProxyFactory();

        private readonly Type _type;
        private readonly Dictionary<string, MemberInfo> _members;
        private readonly BsonClassMapSerializer _serializer;

        public MapongoSerializer(Type type, IEnumerable<MemberInfo> members)
        {
            _type = type;
            _members = members.ToDictionary(x => x.Name);
            _serializer = new BsonClassMapSerializer(BsonClassMap.LookupClassMap(type));
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        {
            var obj = _serializer.Deserialize(bsonReader, nominalType, options);
            return Factory.CreateProxy(nominalType, new MapongoInterceptor(obj, _members));
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            var obj = _serializer.Deserialize(bsonReader, nominalType, actualType, options);
            return Factory.CreateProxy(actualType, new MapongoInterceptor(obj, _members));
        }

        public IBsonSerializationOptions GetDefaultSerializationOptions()
        {
            return _serializer.GetDefaultSerializationOptions();
        }

        public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            _serializer.Serialize(bsonWriter, nominalType, value, options);
        }

        public BsonSerializationInfo GetMemberSerializationInfo(string memberName)
        {
            return _serializer.GetMemberSerializationInfo(memberName);
        }

        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            return _serializer.GetDocumentId(document, out id, out idNominalType, out idGenerator);
        }

        public void SetDocumentId(object document, object id)
        {
            _serializer.SetDocumentId(document, id);
        }
    }
}