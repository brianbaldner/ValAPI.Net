using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class Username
    {
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string GameName { get; set; }
        public string TagLine { get; set; }
        public int StatusCode { get; set; }
        public static Username GetUsername(Auth au, string playerid = null)
        {
            Username ret = new Username();
            if (playerid == null)
            {
                playerid = au.subject;
            }
            string url = "https://pd." + au.region + ".a.pvp.net/name-service/v2/players";
            RestClient client = new RestClient(url);
            client.CookieContainer = au.cookies;
            //client.CookieContainer = new CookieContainer();

            RestRequest request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", $"Bearer {au.AccessToken}");
            request.AddHeader("X-Riot-Entitlements-JWT", $"{au.EntitlementToken}");
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            request.AddHeader("X-Riot-ClientVersion", $"{au.version}");
            List<string> ls = new List<string>();
            ls.Add(playerid);
            request.AddJsonBody(ls);
            var responce = client.Execute(request);
            string responcecontent = responce.Content;
            List<Username> list = JsonConvert.DeserializeObject<List<Username>>(responcecontent);
            list[0].StatusCode = (int)responce.StatusCode;
            ret = list[0];
            //ret.responce = responce;
            return ret;
        }

public static List<Username> GetUsername(Auth au, List<string> playerids)
        {
            string url = "https://pd." + au.region + ".a.pvp.net/name-service/v2/players";
            RestClient client = new RestClient(url);
            client.CookieContainer = au.cookies;
            //client.CookieContainer = new CookieContainer();

            RestRequest request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", $"Bearer {au.AccessToken}");
            request.AddHeader("X-Riot-Entitlements-JWT", $"{au.EntitlementToken}");
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            request.AddHeader("X-Riot-ClientVersion", $"{au.version}");
            request.AddJsonBody(playerids);
            var responce = client.Execute(request);
            string responcecontent = responce.Content;
            List<Username> list = JsonConvert.DeserializeObject<List<Username>>(responcecontent);
            list[0].StatusCode = (int)responce.StatusCode;
            //ret.responce = responce;
            return list;
        }

    }
}
