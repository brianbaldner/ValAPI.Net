using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class Leaderboard
    {
        public int StatusCode { get; set; }
        public string Deployment { get; set; }
        public string QueueID { get; set; }
        public string SeasonID { get; set; }
        public int TopTierRRThreshold { get; set; }
        public List<Player> Players { get; set; }
        public class Player
        {
            public string PlayerCardID { get; set; }
            public string TitleID { get; set; }
            public bool IsBanned { get; set; }
            public bool IsAnonymized { get; set; }
            public string puuid { get; set; }
            public string gameName { get; set; }
            public string tagLine { get; set; }
            public int leaderboardRank { get; set; }
            public int rankedRating { get; set; }
            public int numberOfWins { get; set; }
            public int competitiveTier { get; set; }
        }
        public static Leaderboard GetLeaderboard(Auth au, Region region, string season = "nonegiven")
        {
            if (season == "nonegiven")
            {
                season = Content.GetSeason(au.region);
            }
            Leaderboard ret = new Leaderboard();
            string url = "https://pd." + au.region + ".a.pvp.net/mmr/v1/leaderboards/affinity/" + region + "/queue/competitive/season/" + season;
            RestClient client = new RestClient(url);
            client.CookieContainer = au.cookies;
            //client.CookieContainer = new CookieContainer();

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {au.AccessToken}");
            request.AddHeader("X-Riot-Entitlements-JWT", $"{au.EntitlementToken}");
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            request.AddHeader("X-Riot-ClientVersion", $"{au.version}");

            var responce = client.Execute(request);
            string responcecontent = responce.Content;
            ret = JsonConvert.DeserializeObject<Leaderboard>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
