using System;

namespace ByteCarrot.Mapongo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MongoReferenceAttribute : Attribute
    {
    }
}