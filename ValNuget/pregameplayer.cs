using Newtonsoft.Json;
using RestSharp;

namespace ValAPINet
{
    public class PregameGetPlayer
    {
        public string Subject { get; set; }
        public string MatchID { get; set; }
        public long Version { get; set; }
        public int StatusCode { get; set; }
        public static PregameGetPlayer GetPlayer(Auth au, string playerid = "useauth")
        {
            PregameGetPlayer ret = new PregameGetPlayer();
            if (playerid == "useauth")
            {
                playerid = au.subject;
            }
            string url = "https://glz-" + au.region + "-1." + au.region + ".a.pvp.net/pregame/v1/players/" + playerid;
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
            ret = JsonConvert.DeserializeObject<PregameGetPlayer>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
