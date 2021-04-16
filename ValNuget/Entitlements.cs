using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class ItemEntitlements
    {
        public string ItemTypeID { get; set; }
        public List<Entitlement> Entitlements { get; set; }
        public int StatusCode { get; set; }
        public class Entitlement
        {
            public string ItemID { get; set; }
            public string InstanceID { get; set; }
        }
        public static ItemEntitlements GetItemEntitlements(Auth au, string itemID)
        {
            ItemEntitlements ret = new ItemEntitlements();
            string url = "https://pd." + au.region + ".a.pvp.net/store/v1/entitlements/" + au.subject + "/" + itemID;
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
            ret = JsonConvert.DeserializeObject<ItemEntitlements>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
