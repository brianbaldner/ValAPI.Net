using Newtonsoft.Json;
using System;
using System.IO;
using ValAPINet;
namespace ValAPINetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Auth auth = Websocket.GetAuthLocal(Region.NA);
            PregameGetPlayer player = PregameGetPlayer.GetPlayer(auth);
            while (1 == 1)
            {
                PregameGetMatch match = PregameGetMatch.GetMatch(auth, player.MatchID);
                foreach(PregameGetMatch.Player ply in match.AllyTeam.Players)
                {
                    Console.WriteLine(ply.CharacterID);
                }
            }
        }
    }
}