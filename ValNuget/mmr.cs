using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValAPINet
{
    public class MMR
    {
        public string SeasonID { get; set; }
        public int NumberOfWins { get; set; }
        public int NumberOfWinsWithPlacements { get; set; }
        public int NumberOfGames { get; set; }
        public int Rank { get; set; }
        public int CapstoneWins { get; set; }
        public int LeaderboardRank { get; set; }
        public int CompetitiveTier { get; set; }
        public int RankedRating { get; set; }
        public int GamesNeededForRating { get; set; }
        public int TotalWinsNeededForRank { get; set; }
        public static MMR GetMMR(Auth au, string playerid = "useauth")
        {
            MMR ret = new MMR();
            if (playerid == "useauth")
            {
                playerid = au.subject;
            }
            RestClient client = new RestClient("https://pd." + au.region + ".a.pvp.net/mmr/v1/players/" + playerid);
            client.CookieContainer = au.cookies;

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {au.AccessToken}");
            request.AddHeader("X-Riot-Entitlements-JWT", $"{au.EntitlementToken}");
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            request.AddHeader("X-Riot-ClientVersion", $"{au.version}");
            //request.AddJsonBody("{}");
            string responce = client.Execute(request).Content;
            JObject obj = JObject.FromObject(JsonConvert.DeserializeObject(responce));
            string season = obj["LatestCompetitiveUpdate"].Value<string>("SeasonID");
            ret.SeasonID = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<string>("SeasonID");
            ret.NumberOfWins = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("NumberOfWins");
            ret.NumberOfWinsWithPlacements = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("NumberOfWinsWithPlacements");
            ret.NumberOfGames = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("NumberOfGames");
            ret.Rank = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("Rank");
            ret.CapstoneWins = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("CapstoneWins");
            ret.LeaderboardRank = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("LeaderboardRank");
            ret.CompetitiveTier = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("CompetitiveTier");
            ret.RankedRating = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("RankedRating");
            ret.GamesNeededForRating = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("GamesNeededForRating");
            ret.TotalWinsNeededForRank = obj["QueueSkills"]["competitive"]["SeasonalInfoBySeasonID"][season].Value<int>("TotalWinsNeededForRank");
            return ret;
        }
    }
}
