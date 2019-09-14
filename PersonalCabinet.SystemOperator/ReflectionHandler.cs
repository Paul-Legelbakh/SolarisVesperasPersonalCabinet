using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PersonalCabinet.DAL
{
    public static class ReflectionHandler
    {
        public static Type GetClassFromInterface(Type interfaceType)
            => (from type in Assembly.GetExecutingAssembly().GetTypes()
                where !type.IsInterface && !type.IsAbstract
                where interfaceType.IsAssignableFrom(type)
                select type)
            .FirstOrDefault();
    }
}
