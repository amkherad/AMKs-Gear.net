//using System;
//using AMKsGear.Architecture.Data.Serialization;
//using AMKsGear.Core.Automation.IoC;
//
//namespace AMKsGear.Core.Data.Serialization
//{
//    public static class Serializer
//    {
//        public const string AutoSerializerEngineName = "auto";
//        public const string JsonSerializerEngineName = "json";
//        public const string XmlSerializerEngineName = "xml";
//        public const string BinarySerializerEngineName = "binary";
//
//        public static string DefaultSerializerEngineName { get; set; } = JsonSerializerEngineName;
//
//        public static string Serialize(object obj, params object[] options)
//        {
//            var factory = TypeResolver.CreateInstance<ISerializerFactory>();
//            using (var serializer = factory.CreateSerializer(DefaultSerializerEngineName))
//                return serializer.Serialize(obj, options);
//        }
//        public static string Serialize(object obj, string engine, params object[] options)
//        {
//            var factory = TypeResolver.CreateInstance<ISerializerFactory>();
//            using (var serializer = factory.CreateSerializer(engine ?? DefaultSerializerEngineName))
//                return serializer.Serialize(obj, options);
//        }
//        public static object Deserialize(string source, Type type, params object[] options)
//        {
//            var factory = TypeResolver.CreateInstance<ISerializerFactory>();
//            using (var serializer = factory.CreateSerializer(DefaultSerializerEngineName))
//                return serializer.Deserialize(source, type, options);
//        }
//        public static object Deserialize(string source, string engine, Type type, params object[] options)
//        {
//            var factory = TypeResolver.CreateInstance<ISerializerFactory>();
//            using (var serializer = factory.CreateSerializer(engine ?? DefaultSerializerEngineName))
//                return serializer.Deserialize(source, type, options);
//        }
//        public static T Deserialize<T>(string source, params object[] options)
//        {
//            var factory = TypeResolver.CreateInstance<ISerializerFactory>();
//            using (var serializer = factory.CreateSerializer(DefaultSerializerEngineName))
//                return (T)serializer.Deserialize(source, typeof(T), options);
//        }
//        public static T Deserialize<T>(string source, string engine, params object[] options)
//        {
//            var factory = TypeResolver.CreateInstance<ISerializerFactory>();
//            using (var serializer = factory.CreateSerializer(engine ?? DefaultSerializerEngineName))
//                return (T)serializer.Deserialize(source, typeof(T), options);
//        }
//    }
//}