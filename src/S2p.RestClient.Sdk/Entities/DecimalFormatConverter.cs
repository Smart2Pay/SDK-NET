using System;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class DecimalFormatConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal));
        }

        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
        {
            try
            {
                decimal val = Convert.ToDecimal(value);
                writer.WriteValue(val.ToString("0.##"));
            }
            catch (Exception)
            {
                writer.WriteValue(value.ToString());
            }

        }

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
