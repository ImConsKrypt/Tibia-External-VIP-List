using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TEVL.WorldData
{
    /*
     * To parse this JSON data;
     * 
     * using TEVL.WorldData;
     * 
     * WorldData wData = WorldData.FromJson(jsonString);
     * 
     */

    public partial class WData
    {
        [JsonProperty("worlds", NullValueHandling = NullValueHandling.Ignore)]
        public Worlds? Worlds { get; set; }

        [JsonProperty("information", NullValueHandling = NullValueHandling.Ignore)]
        public Information? Information { get; set; }
    }

    public partial class Information
    {
        [JsonProperty("api_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? ApiVersion { get; set; }

        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Timestamp { get; set; }
    }

    public partial class Worlds
    {
        [JsonProperty("world", NullValueHandling = NullValueHandling.Ignore)]
        public World? World { get; set; }
    }

    public partial class World
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }

        [JsonProperty("players_online", NullValueHandling = NullValueHandling.Ignore)]
        public long? PlayersOnline { get; set; }

        [JsonProperty("record_players", NullValueHandling = NullValueHandling.Ignore)]
        public long? RecordPlayers { get; set; }

        [JsonProperty("record_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? RecordDate { get; set; }

        [JsonProperty("creation_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? CreationDate { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public string? Location { get; set; }

        [JsonProperty("pvp_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? PvpType { get; set; }

        [JsonProperty("premium_only", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PremiumOnly { get; set; }

        [JsonProperty("transfer_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? TransferType { get; set; }

        [JsonProperty("world_quest_titles", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? WorldQuestTitles { get; set; }

        [JsonProperty("battleye_protected", NullValueHandling = NullValueHandling.Ignore)]
        public bool? BattleyeProtected { get; set; }

        [JsonProperty("battleye_date", NullValueHandling = NullValueHandling.Ignore)]
        public string? BattleyeDate { get; set; }

        [JsonProperty("game_world_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? GameWorldType { get; set; }

        [JsonProperty("tournament_world_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? TournamentWorldType { get; set; }

        [JsonProperty("online_players", NullValueHandling = NullValueHandling.Ignore)]
        public OnlinePlayer[]? OnlinePlayers { get; set; }
    }

    public partial class OnlinePlayer
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; set; }

        [JsonProperty("vocation", NullValueHandling = NullValueHandling.Ignore)]
        public Vocation? Vocation { get; set; }
    }

    public enum Vocation { None, Druid, ElderDruid, EliteKnight, Knight, MasterSorcerer, Paladin, RoyalPaladin, Sorcerer };

    public partial class WData
    {
        public static WData FromJson(string json) => JsonConvert.DeserializeObject<WData>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this WData self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
        {
            VocationConverter.Singleton,
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
        };
    }

    internal class VocationConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Vocation) || t == typeof(Vocation?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "None":
                    return Vocation.None;
                case "Druid":
                    return Vocation.Druid;
                case "Elder Druid":
                    return Vocation.ElderDruid;
                case "Elite Knight":
                    return Vocation.EliteKnight;
                case "Knight":
                    return Vocation.Knight;
                case "Master Sorcerer":
                    return Vocation.MasterSorcerer;
                case "Paladin":
                    return Vocation.Paladin;
                case "Royal Paladin":
                    return Vocation.RoyalPaladin;
                case "Sorcerer":
                    return Vocation.Sorcerer;
            }
            throw new Exception("Cannot unmarshal type Vocation");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Vocation)untypedValue;
            switch (value)
            {
                case Vocation.None:
                    serializer.Serialize(writer, "None");
                    return;
                case Vocation.Druid:
                    serializer.Serialize(writer, "Druid");
                    return;
                case Vocation.ElderDruid:
                    serializer.Serialize(writer, "Elder Druid");
                    return;
                case Vocation.EliteKnight:
                    serializer.Serialize(writer, "Elite Knight");
                    return;
                case Vocation.Knight:
                    serializer.Serialize(writer, "Knight");
                    return;
                case Vocation.MasterSorcerer:
                    serializer.Serialize(writer, "Master Sorcerer");
                    return;
                case Vocation.Paladin:
                    serializer.Serialize(writer, "Paladin");
                    return;
                case Vocation.RoyalPaladin:
                    serializer.Serialize(writer, "Royal Paladin");
                    return;
                case Vocation.Sorcerer:
                    serializer.Serialize(writer, "Sorcerer");
                    return;
            }
            throw new Exception("Cannot marshal type Vocation");
        }

        public static readonly VocationConverter Singleton = new VocationConverter();
    }


}
