using System;
using ValAPINet;
namespace ValAPINetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = "";
            string password = "";
            string matchID = "";
            Auth au = Websocket.GetAuthLocal(Region.NA);
            MMR mmr = MMR.GetMMR(au);
            Console.WriteLine(Ranks.GetRankFormatted(mmr.Rank));
        }
    }
}