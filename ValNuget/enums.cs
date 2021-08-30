namespace ValAPINet
{
    public enum Region
    {
        NA,
        EU,
        AP,
        KO
    }
    public class Agent
    {
        public const string Breach = "5F8D3A7F-467B-97F3-062C-13ACF203C006";
        public const string Raze = "f94c3b30-42be-e959-889c-5aa313dba261";
        public const string KAYO = "601DBBE7-43CE-BE57-2A40-4ABD24953621";
        public const string Skye = "6f2a04ca-43e0-be17-7f36-b3908627744d";
        public const string Cypher = "117ed9e3-49f3-6512-3ccf-0cada7e3823b";
        public const string Sova = "320b2a48-4d9b-a075-30f1-1f93a9b638fa";
        public const string Killjoy = "1e58de9c-4950-5125-93e9-a0aee9f98746";
        public const string Viper = "707eab51-4836-f488-046a-cda6bf494859";
        public const string Phoenix = "eb93336a-449b-9c1b-0a54-a891f7921d69";
        public const string Astra = "41fb69c1-4189-7b37-f117-bcaf1e96f1bf";
        public const string Brimstone = "9f0d8ba9-4140-b941-57d3-a7ad57c6b417";
        public const string Yoru = "7f94d92c-4234-0a36-9646-3a87eb8b5c89";
        public const string Sage = "569fdd95-4d10-43ab-ca70-79becc718b46";
        public const string Reyna = "a3bfb853-43b2-7238-a4f1-ad90e9e46bcc";
        public const string Omen = "8e253930-4c05-31dd-1b6c-968525494517";
        public const string Jett = "add6443a-41bd-e414-f6ad-e58d267f4e95";
    }
    public class Ranks
    {
        public static string GetRankFormatted(int rank)
        {
            string[] ranksfor = new string[] { "Unrated", "none", "none", "Iron 1", "Iron 2", "Iron 3", "Bronze 1", "Bronze 2", "Bronze 3", "Silver 1", "Silver 2", "Silver 3", "Gold 1", "Gold 2", "Gold 3", "Platinum 1", "Platinum 2", "Platinum 3", "Diamond 1", "Diamond 2", "Diamond 3", "Immortal", "none", "none", "Radiant" };
            return ranksfor[rank];
        }
    }
}
