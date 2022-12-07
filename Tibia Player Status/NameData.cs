/* 
 * To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
 * using NameData;
 *
 * var nData = NData.FromJson(jsonString);
 *
 */

namespace NameData
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class NData
    {
        [JsonProperty("Friends")]
        public string[] Friends { get; set; }

        [JsonProperty("Enemies")]
        public string[] Enemies { get; set; }
    }

    public partial class NData
    {
        public static NData FromJson(string json) => JsonConvert.DeserializeObject<NData>(json, TibiaData.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this NData self) => JsonConvert.SerializeObject(self, TibiaData.Converter.Settings);
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
}
