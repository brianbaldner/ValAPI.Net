using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ValAPINet
{
    public class Content
    {
        public class Character
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Map
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Chroma
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Skin
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class SkinLevel
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Attachment
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Equip
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Theme
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class GameMode
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Spray
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class SprayLevel
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Charm
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class CharmLevel
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class PlayerCard
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class PlayerTitle
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class StorefrontItem
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string AssetName { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Season
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public bool IsEnabled { get; set; }
            public bool IsActive { get; set; }
            public bool DevelopmentOnly { get; set; }
        }

        public class CompetitiveSeason
        {
            public string ID { get; set; }
            public string SeasonID { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public bool DevelopmentOnly { get; set; }
        }
        public List<Character> Characters { get; set; }
        public List<Map> Maps { get; set; }
        public List<Chroma> Chromas { get; set; }
        public List<Skin> Skins { get; set; }
        public List<SkinLevel> SkinLevels { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Equip> Equips { get; set; }
        public List<Theme> Themes { get; set; }
        public List<GameMode> GameModes { get; set; }
        public List<Spray> Sprays { get; set; }
        public List<SprayLevel> SprayLevels { get; set; }
        public List<Charm> Charms { get; set; }
        public List<CharmLevel> CharmLevels { get; set; }
        public List<PlayerCard> PlayerCards { get; set; }
        public List<PlayerTitle> PlayerTitles { get; set; }
        public List<StorefrontItem> StorefrontItems { get; set; }
        public List<Season> Seasons { get; set; }
        public List<CompetitiveSeason> CompetitiveSeasons { get; set; }
        public int StatusCode { get; set; }
        public static Content GetContent(Region re)
        {
            RestClient versionclient = new RestClient("https://valorant-api.com/v1/version");
            RestRequest versionrequest = new RestRequest(Method.GET);
            string json = versionclient.Execute(versionrequest).Content;
            var version = JsonConvert.DeserializeObject(json);
            JToken versionobj = JObject.FromObject(version);
            JToken versiondata = JObject.FromObject(versionobj["data"]);

            //Get ID list
            string versionformat = versiondata["branch"].Value<string>() + "-shipping-" + versiondata["buildVersion"].Value<string>() + "-" + versiondata["version"].Value<string>().Substring(versiondata["version"].Value<string>().Length - 6);

            Content ret = new Content();
            RestClient client = new RestClient("https://shared." + re + ".a.pvp.net/content-service/v2/content");

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            request.AddHeader("X-Riot-ClientVersion", $"{versionformat}");
            //request.AddJsonBody("{}");
            var responce = client.Execute(request);
            string responcecontent = responce.Content;
            ret = JsonConvert.DeserializeObject<Content>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
        public static string GetSeason(Region re)
        {
            Content con = GetContent(re);
            string season = "";
            foreach (CompetitiveSeason seasn in con.CompetitiveSeasons)
            {
                if (seasn.StartTime.CompareTo(DateTime.UtcNow) < 0 && seasn.EndTime.CompareTo(DateTime.UtcNow) > 0)
                {
                    season = seasn.SeasonID;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return season;
        }

    }
}
