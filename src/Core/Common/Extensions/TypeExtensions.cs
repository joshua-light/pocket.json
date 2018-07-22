﻿using System;
using System.Reflection;

namespace Pocket.Json
{
    internal static class TypeExtensions
    {
        public static bool IsGeneric(this Type self, Type genericType)
        {
            var type = self.GetTypeInfo();
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }
    }
}