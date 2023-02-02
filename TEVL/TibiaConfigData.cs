using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TEVL.TibiaConfig
{
    

    public partial class ConfData
    {
        [JsonProperty("Options", NullValueHandling = NullValueHandling.Ignore)]
        public Options? Options { get; set; }

        [JsonProperty("Players", NullValueHandling = NullValueHandling.Ignore)]
        public Players? Players { get; set; }

        [JsonProperty("Worlds", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? Worlds { get; set; }
    }

    public partial class Options
    {
        [JsonProperty("ShowLevel", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowLevel { get; set; }

        [JsonProperty("ShowVocation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowVocation { get; set; }

        [JsonProperty("ShowGuild", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowGuild { get; set; }

        [JsonProperty("UpdateTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? UpdateTime { get; set; }

        [JsonProperty("UpdateCheck", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UpdateCheck { get; set; }
    }

    public partial class Players
    {
        [JsonProperty("Friends", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? Friends { get; set; }

        [JsonProperty("Foes", NullValueHandling = NullValueHandling.Ignore)]
        public object[]? Foes { get; set; }

        [JsonProperty("Guild", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? Guild { get; set; }
    }

    public partial class ConfData
    {
        public static ConfData FromJson(string json) => JsonConvert.DeserializeObject<ConfData>(json, TibiaConfig.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ConfData self) => JsonConvert.SerializeObject(self, TibiaConfig.Converter.Settings);
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
