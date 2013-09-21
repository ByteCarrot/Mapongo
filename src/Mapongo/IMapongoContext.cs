using System;
using System.Collections.Generic;
using System.Reflection;

namespace ByteCarrot.Mapongo
{
    public interface IMapongoContext
    {
        Dictionary<Type, List<MemberInfo>> Types { get; }
    }
}