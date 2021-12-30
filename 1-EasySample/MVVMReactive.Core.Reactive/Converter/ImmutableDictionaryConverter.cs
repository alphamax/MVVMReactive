using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.Core.Reactive.Converter
{
    public class ImmutableDictionaryConverter<Key, Value> : JsonConverter<ImmutableDictionary<Key, Value>>
    {
        public override ImmutableDictionary<Key, Value> ReadJson(JsonReader reader, Type objectType, ImmutableDictionary<Key, Value> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var result = hasExistingValue ? existingValue : new ImmutableDictionary<Key, Value>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray)
                {
                    return result;
                }

                if (reader.TokenType == JsonToken.StartObject)
                {
                    result = AddObjectToDictionary(reader, result, serializer);
                }
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, ImmutableDictionary<Key, Value> dictionary, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (var key in dictionary.Keys)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Key");
                serializer.Serialize(writer, key);
                writer.WritePropertyName("Value");
                serializer.Serialize(writer, dictionary[key]);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        public override bool CanRead => true;
        public override bool CanWrite => true;

        private ImmutableDictionary<Key, Value> AddObjectToDictionary(JsonReader reader, ImmutableDictionary<Key, Value> result, JsonSerializer serializer)
        {
            Key key = default(Key);
            Value value = default(Value);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject && key != null)
                {
                    return result.Set((Key)key, (Value)value);
                }

                var propertyName = reader.Value.ToString();
                if (propertyName == "Key")
                {
                    reader.Read();
                    key = serializer.Deserialize<Key>(reader);
                }
                else if (propertyName == "Value")
                {
                    reader.Read();
                    value = serializer.Deserialize<Value>(reader);
                }
            }
            return result;
        }
    }
}