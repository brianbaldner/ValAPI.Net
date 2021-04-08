﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValAPINet
{
    public class CompHistory
    {
        public int Version { get; set; }
        public string Subject { get; set; }
        public List<Match> Matches { get; set; }
        public class Match
        {
            public string MatchID { get; set; }
            public string MapID { get; set; }
            public string SeasonID { get; set; }
            public object MatchStartTime { get; set; }
            public int TierAfterUpdate { get; set; }
            public int TierBeforeUpdate { get; set; }
            public int RankedRatingAfterUpdate { get; set; }
            public int RankedRatingBeforeUpdate { get; set; }
            public int RankedRatingEarned { get; set; }
            public int RankedRatingPerformanceBonus { get; set; }
            public string CompetitiveMovement { get; set; }
            public int AFKPenalty { get; set; }
        }
        public static CompHistory GetCompHistory(Auth au, int startindex, int endindex, string queue = "none", string playerid = "useauth")
        {
            CompHistory ret = new CompHistory();
            if (playerid == "useauth")
            {
                playerid = au.subject;
            }
            string paramz = "";
            if(queue == "none")
            {
                paramz = "?startIndex=" + startindex + "&endIndex=" + endindex;
            }
            else
            {
                paramz = "?startIndex=" + startindex + "&endIndex=" + endindex + "&queue=" + queue;
            }
            RestClient client = new RestClient("https://pd." + au.region + ".a.pvp.net/mmr/v1/players/" + playerid + "/competitiveupdates" + paramz);
            client.CookieContainer = au.cookies;

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {au.AccessToken}");
            request.AddHeader("X-Riot-Entitlements-JWT", $"{au.EntitlementToken}");
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            request.AddHeader("X-Riot-ClientVersion", $"{au.version}");
            //request.AddJsonBody("{}");
            string responce = client.Execute(request).Content;
            //JObject obj = JObject.FromObject(JsonConvert.DeserializeObject(responce));
            ret = JsonConvert.DeserializeObject<CompHistory>(responce);
            return ret;
        }
    }
}
