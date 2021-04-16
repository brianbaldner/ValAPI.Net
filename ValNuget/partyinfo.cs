using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ValAPINet
{
    public class PartyInfo
    {
        public int StatusCode { get; set; }
        public string ID { get; set; }
        public string MUCName { get; set; }
        public string VoiceRoomID { get; set; }
        public long Version { get; set; }
        public string ClientVersion { get; set; }
        public List<Member> Members { get; set; }
        public string State { get; set; }
        public string PreviousState { get; set; }
        public string StateTransitionReason { get; set; }
        public string Accessibility { get; set; }
        public CustomGameDataobj CustomGameData { get; set; }
        public MatchmakingDataobj MatchmakingData { get; set; }
        public string Name { get; set; }
        public object Invites { get; set; }
        public List<object> Requests { get; set; }
        public DateTime QueueEntryTime { get; set; }
        public ErrorNotificationobj ErrorNotification { get; set; }
        public int RestrictedSeconds { get; set; }
        public List<string> EligibleQueues { get; set; }
        public string PlatformType { get; set; }
        public List<object> QueueIneligibilities { get; set; }
        public CheatDataobj CheatData { get; set; }
        public List<object> XPBonuses { get; set; }
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

        public class Ping
        {
            public int ping { get; set; }
            public string GamePodID { get; set; }
        }

        public class Member
        {
            public string Subject { get; set; }
            public int CompetitiveTier { get; set; }
            public PlayerIdentity PlayerIdentity { get; set; }
            public SeasonalBadgeInfo SeasonalBadgeInfo { get; set; }
            public bool IsOwner { get; set; }
            public int QueueEligibleRemainingGames { get; set; }
            public int QueueEligibleRemainingWins { get; set; }
            public List<Ping> Pings { get; set; }
            public bool IsReady { get; set; }
            public bool IsModerator { get; set; }
            public bool UseBroadcastHUD { get; set; }
            public string PlatformType { get; set; }
        }

        public class Settings
        {
            public string Map { get; set; }
            public string Mode { get; set; }
            public bool UseBots { get; set; }
            public string GamePod { get; set; }
            public object GameRules { get; set; }
        }

        public class Membership
        {
            public object teamOne { get; set; }
            public object teamTwo { get; set; }
            public object teamSpectate { get; set; }
            public object teamOneCoaches { get; set; }
            public object teamTwoCoaches { get; set; }
        }

        public class CustomGameDataobj
        {
            public Settings Settings { get; set; }
            public Membership Membership { get; set; }
            public int MaxPartySize { get; set; }
        }

        public class MatchmakingDataobj
        {
            public string QueueID { get; set; }
            public List<object> PreferredGamePods { get; set; }
        }

        public class ErrorNotificationobj
        {
            public string ErrorType { get; set; }
            public object ErroredPlayers { get; set; }
        }

        public class CheatDataobj
        {
            public string GamePodOverride { get; set; }
            public bool ForcePostGameProcessing { get; set; }
        }
        public static PartyInfo Party(Auth au, string partyID)
        {
            PartyInfo ret = new PartyInfo();
            string url = "https://glz-" + au.region + "-1." + au.region + ".a.pvp.net/parties/v1/parties/" + partyID;
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
            ret = JsonConvert.DeserializeObject<PartyInfo>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
