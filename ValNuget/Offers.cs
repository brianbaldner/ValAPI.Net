using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class StoreOffers
    {
        public List<Offer> Offers { get; set; }
        public List<UpgradeCurrencyOffer> UpgradeCurrencyOffers { get; set; }
        public int StatusCode { get; set; }
        public class Cost
        {
            [JsonProperty("85ad13f7-3d1b-5128-9eb2-7cd8ee0b5741")]
            public int ValorantPoints { get; set; }
        }

        public class Reward
        {
            public string ItemTypeID { get; set; }
            public string ItemID { get; set; }
            public int Quantity { get; set; }
        }

        public class Offer
        {
            public string OfferID { get; set; }
            public bool IsDirectPurchase { get; set; }
            public string StartDate { get; set; }
            public Cost Cost { get; set; }
            public List<Reward> Rewards { get; set; }
        }

        public class UpgradeCurrencyOffer
        {
            public string OfferID { get; set; }
            public string StorefrontItemID { get; set; }
        }
        public static StoreOffers GetOffers(Auth au)
        {
            StoreOffers ret = new StoreOffers();
            string url = "https://pd." + au.region + ".a.pvp.net/store/v1/offers/";
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
            ret = JsonConvert.DeserializeObject<StoreOffers>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
