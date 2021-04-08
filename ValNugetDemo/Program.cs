using System;
using ValAPINet;
namespace ValAPINetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Auth au = Auth.Login("username", "password", Region.NA);
            CompHistory his = CompHistory.GetCompHistory(au, 0, 20);
            MatchData md = MatchData.GetMatchData(au, his.Matches[0].MatchID);
            Console.WriteLine(md.players[0].gameName);
            Console.ReadKey();
        }
    }
}