using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class CoreGetMatch
    {
        public string MatchID { get; set; }
        public long Version { get; set; }
        public string State { get; set; }
        public string MapID { get; set; }
        public string ModeID { get; set; }
        public string ProvisioningFlow { get; set; }
        public string GamePodID { get; set; }
        public string AllMUCName { get; set; }
        public string TeamMUCName { get; set; }
        public string TeamVoiceID { get; set; }
        public bool IsReconnectable { get; set; }
        public ConnectionDetailsobj ConnectionDetails { get; set; }
        public object PostGameDetails { get; set; }
        public List<Player> Players { get; set; }
        public object MatchmakingData { get; set; }
        public int StatusCode { get; set; }
        public class ConnectionDetailsobj
        {
            public string GameServerHost { get; set; }
            public int GameServerPort { get; set; }
            public long GameServerObfuscatedIP { get; set; }
            public long GameClientHash { get; set; }
            public string PlayerKey { get; set; }
            public string TempMap { get; set; }
            public string TempTeam { get; set; }
        }

        public class PlayerIdentity
        {
            public string Subject { get; set; }
            public string PlayerCardID { get; set; }
            public string PlayerTitleID { get; set; }
            public bool Incognito { get; set; }
        }

        public class SeasonalBadgeInfo
        {
            public string SeasonID { get; set; }
            public int NumberOfWins { get; set; }
            public object WinsByTier { get; set; }
            public int Rank { get; set; }
            public int LeaderboardRank { get; set; }
        }

        public class Player
        {
            public string Subject { get; set; }
            public string TeamID { get; set; }
            public string CharacterID { get; set; }
            public PlayerIdentity PlayerIdentity { get; set; }
            public SeasonalBadgeInfo SeasonalBadgeInfo { get; set; }
        }
        public static CoreGetMatch GetMatch(Auth au, string matchid)
        {
            CoreGetMatch ret = new CoreGetMatch();
            string url = "https://glz-" + au.region + "-1." + au.region + ".a.pvp.net/core-game/v1/matches/" + matchid;
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
            ret = JsonConvert.DeserializeObject<CoreGetMatch>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
