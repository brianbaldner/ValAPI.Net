using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ValAPINet
{
    public class Storefront
    {
        public FeaturedBundleobj FeaturedBundle { get; set; }
        public SkinsPanelLayoutobj SkinsPanelLayout { get; set; }
        public BonusStoreobj BonusStore { get; set; }
        public int StatusCode { get; set; }
        public class Item2
        {
            public string ItemTypeID { get; set; }
            public string ItemID { get; set; }
            public int Amount { get; set; }
        }

        public class Item
        {
            public Item2 ItemInfo { get; set; }
            public int BasePrice { get; set; }
            public string CurrencyID { get; set; }
            public int DiscountPercent { get; set; }
            public bool IsPromoItem { get; set; }
        }

        public class Bundle
        {
            public string ID { get; set; }
            public string DataAssetID { get; set; }
            public string CurrencyID { get; set; }
            public List<Item> Items { get; set; }
        }

        public class FeaturedBundleobj
        {
            public Bundle Bundle { get; set; }
            public int BundleRemainingDurationInSeconds { get; set; }
        }

        public class SkinsPanelLayoutobj
        {
            public List<string> SingleItemOffers { get; set; }
            public int SingleItemOffersRemainingDurationInSeconds { get; set; }
        }

        public class Cost
        {
            [JsonProperty("85ad13f7-3d1b-5128-9eb2-7cd8ee0b5741")]
            public int _85ad13f73d1b51289eb27cd8ee0b5741 { get; set; }
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

        public class DiscountCosts
        {
            [JsonProperty("85ad13f7-3d1b-5128-9eb2-7cd8ee0b5741")]
            public int ValorantPoints { get; set; }
        }

        public class BonusStoreOffer
        {
            public string BonusOfferID { get; set; }
            public Offer Offer { get; set; }
            public int DiscountPercent { get; set; }
            public DiscountCosts DiscountCosts { get; set; }
            public bool IsSeen { get; set; }
        }

        public class BonusStoreobj
        {
            public List<BonusStoreOffer> BonusStoreOffers { get; set; }
            public int BonusStoreRemainingDurationInSeconds { get; set; }
        }
        public static Storefront GetOffers(Auth au)
        {
            Storefront ret = new Storefront();
            string url = "https://pd." + au.region + ".a.pvp.net/store/v2/storefront/" + au.subject;
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
            ret = JsonConvert.DeserializeObject<Storefront>(responcecontent);
            ret.StatusCode = (int)responce.StatusCode;
            return ret;
        }
    }
}
