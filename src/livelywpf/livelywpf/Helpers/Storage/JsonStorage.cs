﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace livelywpf.Helpers
{
    public static class JsonStorage<T>
    {
        public static T LoadData(string filePath)
        {
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filePath))
            {
                var serializer = new JsonSerializer();
                var tmp = (T)serializer.Deserialize(file, typeof(T));

                //if file is corrupted, json can return null.
                if (tmp == null)
                {
                    throw new ArgumentNullException("json null/corrupt");
                }
                else
                {
                    return tmp;
                }
            }
        }

        public static void StoreData(string filePath, T data)
        {
            JsonSerializer serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                //serializer.Converters.Add(new JavaScriptDateTimeConverter());
                NullValueHandling = NullValueHandling.Include
            };

            using (StreamWriter sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
