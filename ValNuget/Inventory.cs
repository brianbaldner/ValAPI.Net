using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class Inventory
    {
        public class Gun
        {
            public string ID { get; set; }
            public string SkinID { get; set; }
            public string SkinLevelID { get; set; }
            public string ChromaID { get; set; }
            public List<object> Attachments { get; set; }
            public string CharmInstanceID { get; set; }
            public string CharmID { get; set; }
            public string CharmLevelID { get; set; }
        }

        public class Spray
        {
            public string EquipSlotID { get; set; }
            public string SprayID { get; set; }
            public object SprayLevelID { get; set; }
        }

        public class PlayerCardObj
        {
            public string ID { get; set; }
        }

        public class PlayerTitleObj
        {
            public string ID { get; set; }
        }

        public string Subject { get; set; }
        public int Version { get; set; }
        public List<Gun> Guns { get; set; }
        public List<Spray> Sprays { get; set; }
        public PlayerCardObj PlayerCard { get; set; }
        public PlayerTitleObj PlayerTitle { get; set; }
        public int StatusCode { get; set; }
        public static Inventory GetInventory(Auth au)
        {
            Inventory ret = new Inventory();
            string url = "https://pd." + au.region + ".a.pvp.net/personalization/v2/players/" + au.subject + "/playerloadout";
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
            ret = JsonConvert.DeserializeObject<Inventory>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            //ret.responce = responce;
            return ret;
        }
    }
}
