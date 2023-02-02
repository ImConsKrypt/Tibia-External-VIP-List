using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TEVL.CharacterData
{
    /*
     * To parse this JSON data;
     *
     * using TEVL.CharacterData;
     *
     * CharacterData cData = CharacterData.FromJson(jsonString);
     *
     */
    public partial class CData
    {
        [JsonProperty("characters", NullValueHandling = NullValueHandling.Ignore)]
        public Characters? Characters { get; set; }

        [JsonProperty("information", NullValueHandling = NullValueHandling.Ignore)]
        public Information? Information { get; set; }
    }

    public partial class Characters
    {
        [JsonProperty("character", NullValueHandling = NullValueHandling.Ignore)]
        public Character? Character { get; set; }

        [JsonProperty("achievements", NullValueHandling = NullValueHandling.Ignore)]
        public Achievement[]? Achievements { get; set; }

        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public Death[]? Deaths { get; set; }

        [JsonProperty("account_information", NullValueHandling = NullValueHandling.Ignore)]
        public AccountInformation? AccountInformation { get; set; }

        [JsonProperty("other_characters", NullValueHandling = NullValueHandling.Ignore)]
        public OtherCharacter[]? OtherCharacters { get; set; }
    }

    public partial class AccountInformation
    {
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Created { get; set; }

        [JsonProperty("loyalty_title", NullValueHandling = NullValueHandling.Ignore)]
        public string? LoyaltyTitle { get; set; }
    }

    public partial class Achievement
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("grade", NullValueHandling = NullValueHandling.Ignore)]
        public long? Grade { get; set; }

        [JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Secret { get; set; }
    }

    public partial class Character
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("sex", NullValueHandling = NullValueHandling.Ignore)]
        public string? Sex { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string? Title { get; set; }

        [JsonProperty("unlocked_titles", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnlockedTitles { get; set; }

        [JsonProperty("vocation", NullValueHandling = NullValueHandling.Ignore)]
        public string? Vocation { get; set; }

        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; set; }

        [JsonProperty("achievement_points", NullValueHandling = NullValueHandling.Ignore)]
        public long? AchievementPoints { get; set; }

        [JsonProperty("world", NullValueHandling = NullValueHandling.Ignore)]
        public string? World { get; set; }

        [JsonProperty("residence", NullValueHandling = NullValueHandling.Ignore)]
        public string? Residence { get; set; }

        [JsonProperty("guild", NullValueHandling = NullValueHandling.Ignore)]
        public Guild? Guild { get; set; }

        [JsonProperty("last_login", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastLogin { get; set; }

        [JsonProperty("account_status", NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountStatus { get; set; }
    }

    public partial class Guild
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public string? Rank { get; set; }
    }

    public partial class Death
    {
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Time { get; set; }

        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; set; }

        [JsonProperty("killers", NullValueHandling = NullValueHandling.Ignore)]
        public Killer[]? Killers { get; set; }

        [JsonProperty("assists", NullValueHandling = NullValueHandling.Ignore)]
        public object[]? Assists { get; set; }

        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string? Reason { get; set; }
    }

    public partial class Killer
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("player", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Player { get; set; }

        [JsonProperty("traded", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Traded { get; set; }

        [JsonProperty("summon", NullValueHandling = NullValueHandling.Ignore)]
        public string? Summon { get; set; }
    }

    public partial class OtherCharacter
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("world", NullValueHandling = NullValueHandling.Ignore)]
        public string? World { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }

        [JsonProperty("deleted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Deleted { get; set; }

        [JsonProperty("main", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Main { get; set; }

        [JsonProperty("traded", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Traded { get; set; }
    }

    public partial class Information
    {
        [JsonProperty("api_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? ApiVersion { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Timestamp { get; set; }
    }

    public partial class CData
    {
        public static CData FromJson(string json) => JsonConvert.DeserializeObject<CData>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CData self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
