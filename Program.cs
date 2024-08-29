using System.Diagnostics;

namespace ReflectionOTUSHomework
{
    internal class Program
    {
        private const int SERIALIZED_COUNT = 100000;
        static void Main(string[] args)
        {
            var f = F.Get();
            var time = Stopwatch.StartNew();
            var serialize = "asd";
            for (int i = 0; i < SERIALIZED_COUNT; i++)
            {
               serialize = MyReflection.MySerializerToString(f);
            }
            Console.WriteLine(serialize);
            time.Stop();
            Console.WriteLine("Моя сериализация:");
            Console.WriteLine($"{time.ElapsedMilliseconds} m/s\n");
            time.Reset();
            serialize = "asd";
            time.Start();

            for (int i = 0; i < SERIALIZED_COUNT; i++)
            {
                 serialize = Newtonsoft.Json.JsonConvert.SerializeObject(f);
            }

            time.Stop();

            Console.WriteLine(serialize);
            Console.WriteLine("стандартный механизм (NewtonsoftJson):");
            Console.WriteLine($"{time.ElapsedMilliseconds} m/s\n");
            
            time.Reset();
            F deserializedObj = new F();            
            time.Start();
     
            for (int i = 0; i < SERIALIZED_COUNT; i++)
            {
                deserializedObj = MyReflection.MyDeserializerFromString<F>(serialize);
            }

            time.Stop();

            Console.WriteLine("Моя дессериализация");
            Console.WriteLine($"Полученный объект: {deserializedObj.i1}, {deserializedObj.i2}, {deserializedObj.i3}, {deserializedObj.i4}, {deserializedObj.i5}");
            Console.WriteLine($"{time.ElapsedMilliseconds} m/s\n");
           
            time.Reset();
            deserializedObj = new F();
            time.Start();

            for (int i = 0; i< SERIALIZED_COUNT; i++)
            {
                deserializedObj = Newtonsoft.Json.JsonConvert.DeserializeObject<F>(serialize);
            }
            time.Stop();

            Console.WriteLine("стандартный механизм (NewtonsoftJson):");
            Console.WriteLine($"Полученный объект: {deserializedObj.i1}, {deserializedObj.i2}, {deserializedObj.i3}, {deserializedObj.i4}, {deserializedObj.i5}");
            Console.WriteLine($"{time.ElapsedMilliseconds} m/s\n");
        }
    }
}
