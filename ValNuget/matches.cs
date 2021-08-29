using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class MatchHistory
    {
        public string Subject { get; set; }
        public int BeginIndex { get; set; }
        public int EndIndex { get; set; }
        public int Total { get; set; }
        public int StatusCode { get; set; }
        public List<Matches> History { get; set; }
        public class Matches
        {
            public string MatchID { get; set; }
            public object GameStartTime { get; set; }
            public string TeamID { get; set; }
        }
        public static MatchHistory GetMatchHistory(Auth au, int startindex, int endindex, string queue, string playerid = "useauth")
        {
            MatchHistory ret = new MatchHistory();
            if (playerid == "useauth")
            {
                playerid = au.subject;
            }
            string paramz = "?startIndex=" + startindex + "&endIndex=" + endindex + "&queue=" + queue;
            RestClient client = new RestClient("https://pd." + au.region + ".a.pvp.net/match-history/v1/history/" + playerid + paramz);
            client.CookieContainer = au.cookies;

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {au.AccessToken}");
            request.AddHeader("X-Riot-Entitlements-JWT", $"{au.EntitlementToken}");
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            request.AddHeader("X-Riot-ClientVersion", $"{au.version}");
            //request.AddJsonBody("{}");
            var responce = client.Execute(request);
            string responcecontent = responce.Content;
            //JObject obj = JObject.FromObject(JsonConvert.DeserializeObject(responce));
            ret = JsonConvert.DeserializeObject<MatchHistory>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
