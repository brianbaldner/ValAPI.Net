using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ValAPINet
{
    public class AccountXP
    {
        public int StatusCode { get; set; }
        public class XpProgress
        {
            public int Level { get; set; }
            public int XP { get; set; }
        }

        public class StartProgress
        {
            public int Level { get; set; }
            public int XP { get; set; }
        }

        public class EndProgress
        {
            public int Level { get; set; }
            public int XP { get; set; }
        }

        public class XPSource
        {
            public string ID { get; set; }
            public int Amount { get; set; }
        }

        public class XpHistory
        {
            public string ID { get; set; }
            public DateTime MatchStart { get; set; }
            public StartProgress StartProgress { get; set; }
            public EndProgress EndProgress { get; set; }
            public int XPDelta { get; set; }
            public List<XPSource> XPSources { get; set; }
            public List<object> XPMultipliers { get; set; }
        }

            public int Version { get; set; }
            public string Subject { get; set; }
            public XpProgress Progress { get; set; }
            public List<XpHistory> History { get; set; }
            public string LastTimeGrantedFirstWin { get; set; }
            public string NextTimeFirstWinAvailable { get; set; }
        public static AccountXP GetOffers(Auth au)
        {
            AccountXP ret = new AccountXP();
            string url = "https://pd." + au.region + ".a.pvp.net/account-xp/v1/players/" + au.subject;
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
            ret = JsonConvert.DeserializeObject<AccountXP>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
