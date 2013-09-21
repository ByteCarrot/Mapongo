using LinFu.DynamicProxy;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ByteCarrot.Mapongo
{
    public class MapongoInterceptor : Interceptor
    {
        private readonly object _target;
        private readonly Dictionary<string, MemberInfo> _members;

        public MapongoInterceptor(object target, Dictionary<string, MemberInfo> members)
        {
            _target = target;
            _members = members;
        }

        public override object Intercept(InvocationInfo info)
        {
            if (_members.ContainsKey(info.TargetMethod.Name))
            {
                Debug.WriteLine("AAAA");
            }
            return info.TargetMethod.Invoke(_target, info.Arguments);
        }
    }
}