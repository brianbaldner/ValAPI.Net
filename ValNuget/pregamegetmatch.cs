using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ValAPINet
{
    public class PregameGetMatch
    {
        public string ID { get; set; }
        public long Version { get; set; }
        public List<Team> Teams { get; set; }
        public AllyTeamobj AllyTeam { get; set; }
        public object EnemyTeam { get; set; }
        public List<object> ObserverSubjects { get; set; }
        public List<object> MatchCoaches { get; set; }
        public int EnemyTeamSize { get; set; }
        public int EnemyTeamLockCount { get; set; }
        public string PregameState { get; set; }
        public DateTime LastUpdated { get; set; }
        public string MapID { get; set; }
        public string GamePodID { get; set; }
        public string Mode { get; set; }
        public string VoiceSessionID { get; set; }
        public string MUCName { get; set; }
        public string QueueID { get; set; }
        public string ProvisioningFlowID { get; set; }
        public bool IsRanked { get; set; }
        public long PhaseTimeRemainingNS { get; set; }
        public bool altModesFlagADA { get; set; }
        public int StatusCode { get; set; }
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
            public string CharacterID { get; set; }
            public string CharacterSelectionState { get; set; }
            public string PregamePlayerState { get; set; }
            public int CompetitiveTier { get; set; }
            public PlayerIdentity PlayerIdentity { get; set; }
            public SeasonalBadgeInfo SeasonalBadgeInfo { get; set; }
        }

        public class Team
        {
            public string TeamID { get; set; }
            public List<Player> Players { get; set; }
        }

        public class AllyTeamobj
        {
            public string TeamID { get; set; }
            public List<Player> Players { get; set; }
        }
        public static PregameGetMatch GetMatch(Auth au, string matchid)
        {
            PregameGetMatch ret = new PregameGetMatch();
            string url = "https://glz-" + au.region + "-1." + au.region + ".a.pvp.net/pregame/v1/matches/" + matchid;
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
            ret = JsonConvert.DeserializeObject<PregameGetMatch>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
