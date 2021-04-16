using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ValAPINet
{
    public class Balance
    {
        public int ValorantPoints { get; set; }
        public int RadianitePoints { get; set; }
        public int FreeAgents { get; set; }
        public int StatusCode { get; set; }
        public static Balance GetBalance(Auth au)
        {
            Balance ret = new Balance();
            string url = "https://pd." + au.region + ".a.pvp.net/store/v1/wallet/" + au.subject;
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
            JObject obj = JObject.FromObject(JsonConvert.DeserializeObject(responcecontent));
            ret.ValorantPoints = obj["Balances"].Value<int>("85ad13f7-3d1b-5128-9eb2-7cd8ee0b5741");
            ret.RadianitePoints = obj["Balances"].Value<int>("e59aa87c-4cbf-517a-5983-6e81511be9b7");
            ret.FreeAgents = obj["Balances"].Value<int>("f08d4ae3-939c-4576-ab26-09ce1f23bb37");
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
