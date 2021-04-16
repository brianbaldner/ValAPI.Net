using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class MatchData
    {
        public MatchInfo matchInfo { get; set; }
        public List<Player> players { get; set; }
        public List<object> bots { get; set; }
        public List<Team> teams { get; set; }
        public List<RoundResult> roundResults { get; set; }
        public List<Kill> kills { get; set; }
        public int StatusCode { get; set; }
        public class MatchInfo
        {
            public string matchId { get; set; }
            public string mapId { get; set; }
            public string gamePodId { get; set; }
            public string gameLoopZone { get; set; }
            public string gameServerAddress { get; set; }
            public string gameVersion { get; set; }
            public int gameLengthMillis { get; set; }
            public long gameStartMillis { get; set; }
            public string provisioningFlowID { get; set; }
            public bool isCompleted { get; set; }
            public string customGameName { get; set; }
            public bool forcePostProcessing { get; set; }
            public string queueID { get; set; }
            public string gameMode { get; set; }
            public bool isRanked { get; set; }
            public bool canProgressContracts { get; set; }
            public bool isMatchSampled { get; set; }
            public string seasonId { get; set; }
            public string completionState { get; set; }
            public string platformType { get; set; }
        }

        public class PlatformInfo
        {
            public string platformType { get; set; }
            public string platformOS { get; set; }
            public string platformOSVersion { get; set; }
            public string platformChipset { get; set; }
        }

        public class Stats
        {
            public int score { get; set; }
            public int roundsPlayed { get; set; }
            public int kills { get; set; }
            public int deaths { get; set; }
            public int assists { get; set; }
            public int playtimeMillis { get; set; }
        }

        public class BasicMovement
        {
            public int idleTimeMillis { get; set; }
            public int objectiveCompleteTimeMillis { get; set; }
        }

        public class BasicGunSkill
        {
            public int idleTimeMillis { get; set; }
            public int objectiveCompleteTimeMillis { get; set; }
        }

        public class AdaptiveBots
        {
            public int idleTimeMillis { get; set; }
            public int objectiveCompleteTimeMillis { get; set; }
            public int adaptiveBotAverageDurationMillisAllAttempts { get; set; }
            public int adaptiveBotAverageDurationMillisFirstAttempt { get; set; }
            public object killDetailsFirstAttempt { get; set; }
        }

        public class Ability
        {
            public int idleTimeMillis { get; set; }
            public int objectiveCompleteTimeMillis { get; set; }
            public object grenadeEffects { get; set; }
            public object ability1Effects { get; set; }
            public object ability2Effects { get; set; }
            public object ultimateEffects { get; set; }
        }

        public class BombPlant
        {
            public int idleTimeMillis { get; set; }
            public int objectiveCompleteTimeMillis { get; set; }
        }

        public class DefendBombSite
        {
            public int idleTimeMillis { get; set; }
            public int objectiveCompleteTimeMillis { get; set; }
            public bool success { get; set; }
        }

        public class SettingStatus
        {
            public bool isMouseSensitivityDefault { get; set; }
            public bool isCrosshairDefault { get; set; }
        }

        public class NewPlayerExperienceDetails
        {
            public BasicMovement basicMovement { get; set; }
            public BasicGunSkill basicGunSkill { get; set; }
            public AdaptiveBots adaptiveBots { get; set; }
            public Ability ability { get; set; }
            public BombPlant bombPlant { get; set; }
            public DefendBombSite defendBombSite { get; set; }
            public SettingStatus settingStatus { get; set; }
        }

        public class Player
        {
            public string subject { get; set; }
            public string gameName { get; set; }
            public string tagLine { get; set; }
            public PlatformInfo platformInfo { get; set; }
            public string teamId { get; set; }
            public string partyId { get; set; }
            public string characterId { get; set; }
            public Stats stats { get; set; }
            public object roundDamage { get; set; }
            public int competitiveTier { get; set; }
            public string playerCard { get; set; }
            public string playerTitle { get; set; }
            public int sessionPlaytimeMinutes { get; set; }
            public NewPlayerExperienceDetails newPlayerExperienceDetails { get; set; }
        }

        public class Team
        {
            public string teamId { get; set; }
            public bool won { get; set; }
            public int roundsPlayed { get; set; }
            public int roundsWon { get; set; }
            public int numPoints { get; set; }
        }

        public class PlantLocation
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class DefuseLocation
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class VictimLocation
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public class FinishingDamage
        {
            public string damageType { get; set; }
            public string damageItem { get; set; }
            public bool isSecondaryFireMode { get; set; }
        }

        public class Kill
        {
            public int gameTime { get; set; }
            public int roundTime { get; set; }
            public string killer { get; set; }
            public string victim { get; set; }
            public VictimLocation victimLocation { get; set; }
            public List<string> assistants { get; set; }
            public List<object> playerLocations { get; set; }
            public FinishingDamage finishingDamage { get; set; }
            public int round { get; set; }
        }

        public class Economy
        {
            public int loadoutValue { get; set; }
            public string weapon { get; set; }
            public string armor { get; set; }
            public int remaining { get; set; }
            public int spent { get; set; }
        }

        public class PlayerStat
        {
            public string subject { get; set; }
            public List<Kill> kills { get; set; }
            public List<object> damage { get; set; }
            public int score { get; set; }
            public Economy economy { get; set; }
            public Ability ability { get; set; }
            public bool wasAfk { get; set; }
            public bool wasPenalized { get; set; }
            public bool stayedInSpawn { get; set; }
        }

        public class RoundResult
        {
            public int roundNum { get; set; }
            public string roundResult { get; set; }
            public string roundCeremony { get; set; }
            public string winningTeam { get; set; }
            public int plantRoundTime { get; set; }
            public object plantPlayerLocations { get; set; }
            public PlantLocation plantLocation { get; set; }
            public string plantSite { get; set; }
            public int defuseRoundTime { get; set; }
            public object defusePlayerLocations { get; set; }
            public DefuseLocation defuseLocation { get; set; }
            public List<PlayerStat> playerStats { get; set; }
            public string roundResultCode { get; set; }
            public object playerEconomies { get; set; }
            public object playerScores { get; set; }
        }
        public static MatchData GetMatchData(Auth au, string matchID)
        {
            MatchData ret = new MatchData();
            string url = "https://pd." + au.region + ".a.pvp.net/match-details/v1/matches/" + matchID;
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
            ret = JsonConvert.DeserializeObject<MatchData>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
