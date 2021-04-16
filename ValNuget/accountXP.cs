using RestSharp;

namespace ValAPINet
{
    public class AccountXP
    {
        public int StatusCode { get; set; }
        public string response;
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
            //ret = JsonConvert.DeserializeObject<AccountXP>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            ret.response = responcecontent;
            return ret;
        }
    }
}
