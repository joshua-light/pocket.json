using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Pocket.Json
{
    internal static class JsonCodeGen
    {
        public static Func<T> NewObj<T>()
        {
            var type = typeof(T);
            var method = new DynamicMethod("NewInstance_" + type.Namespace,
                type,
                new Type[0]
            );

            var il = method.GetILGenerator();

            il.DeclareLocal(type);

            il.Emit(OpCodes.Newobj, type.GetTypeInfo().DeclaredConstructors.First());
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            return (Func<T>) method.CreateDelegate(typeof(Func<T>));
        }

        public static Func<T, object> ReadField<T>(FieldInfo field)
        {
            var method = new DynamicMethod("ReadField_" + field.Name,
                typeof(object),
                new[] { typeof(T) }
            );

            var il = method.GetILGenerator();

            il.DeclareLocal(typeof(object));

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, field);

            if (field.FieldType.GetTypeInfo().IsValueType)
                il.Emit(OpCodes.Box, field.FieldType);

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);

            il.Emit(OpCodes.Ret);

            return (Func<T, object>) method.CreateDelegate(typeof(Func<T, object>));
        }

        public static Action<T, object> WriteField<T>(FieldInfo field)
        {
            var method = new DynamicMethod("WriteField_" + field.Name,
                typeof(void),
                new[] { typeof(T), typeof(object) }
            );

            var il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Unbox_Any, field.FieldType);

            il.Emit(OpCodes.Stfld, field);

            il.Emit(OpCodes.Ret);

            return (Action<T, object>) method.CreateDelegate(typeof(Action<T, object>));
        }

        public static Append<object> Append(Type type)
        {
            return (Append<object>) typeof(AppendGenerator)
                .GetTypeInfo()
                .GetDeclaredMethod("Generate")
                .MakeGenericMethod(type)
                .Invoke(null, null);
        }

        public static Unwrap<object> Unwrap(Type type)
        {
            return (Unwrap<object>) typeof(UnwrapGenerator)
                .GetTypeInfo()
                .GetDeclaredMethod("Generate")
                .MakeGenericMethod(type)
                .Invoke(null, null);
        }

        internal class AppendGenerator
        {
            public static Append<object> Generate<T>()
            {
                return (x, buffer) => Json<T>.Append((T) x, buffer);
            }
        }

        internal class UnwrapGenerator
        {
            public static Unwrap<object> Generate<T>()
            {
                return x => Json<T>.Unwrap(x);
            }
        }
    }
}