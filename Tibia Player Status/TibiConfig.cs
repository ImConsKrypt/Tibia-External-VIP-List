// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using TibiaExternalVIP;
//
//    var tibiConfig = TibiConfig.FromJson(jsonString);

namespace TibiaExternalVIP
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TibiConfig
    {
        [JsonProperty("Options")]
        public Options Options { get; set; }

        [JsonProperty("Worlds")]
        public string[] Worlds { get; set; }

        [JsonProperty("Players")]
        public Players Players { get; set; }
    }

    public partial class Options
    {
        [JsonProperty("ShowGuilds")]
        public bool ShowGuilds { get; set; }

        [JsonProperty("ShowLevels")]
        public bool ShowLevels { get; set; }

        [JsonProperty("ShowVocations")]
        public bool ShowVocations { get; set; }

        [JsonProperty("UpdateTime")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long UpdateTime { get; set; }
    }

    public partial class Players
    {
        [JsonProperty("Friends")]
        public string[] Friends { get; set; }

        [JsonProperty("Enemies")]
        public string[] Enemies { get; set; }
    }

    public partial class TibiConfig
    {
        public static TibiConfig FromJson(string json) => JsonConvert.DeserializeObject<TibiConfig>(json, TibiaExternalVIP.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TibiConfig self) => JsonConvert.SerializeObject(self, TibiaExternalVIP.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
