using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class GetParty
    {
        public string Subject { get; set; }
        public long Version { get; set; }
        public string CurrentPartyID { get; set; }
        public object Invites { get; set; }
        public List<object> Requests { get; set; }
        public PlatformInfoobj PlatformInfo { get; set; }
        public int StatusCode { get; set; }
        public class PlatformInfoobj
        {
            public string platformType { get; set; }
            public string platformOS { get; set; }
            public string platformOSVersion { get; set; }
            public string platformChipset { get; set; }
        }
        public static GetParty Party(Auth au)
        {
            GetParty ret = new GetParty();
            string url = "https://glz-" + au.region + "-1." + au.region + ".a.pvp.net/parties/v1/players/" + au.subject;
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
            ret = JsonConvert.DeserializeObject<GetParty>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
