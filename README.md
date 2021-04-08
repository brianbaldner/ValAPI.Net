# ValAPI.Net
![Nuget](https://img.shields.io/nuget/v/ValAPI.Net)

A class library for interacting with the Valorant In Game API in the .NET framework.

[Docs](https://github.com/brianbaldner/ValAPI.Net/wiki)

[NuGet](https://www.nuget.org/packages/ValAPI.Net/)

# Example
This example gets the rank of the logged in user. This would return Platinum 1.
```c#
Auth au = Auth.Login(username, password, Region.NA);
MMR mmr = MMR.GetMMR(au);
Console.WriteLine(Ranks.GetRankFormatted(mmr.Rank));
```

This code will lock Omen in the user's current game.
```c#
Auth au = Auth.Login(username, password, Region.NA);
PregameGetPlayer pregame = PregameGetPlayer.GetPlayer(au);
SelectAgent.LockAgent(au, pregame.MatchID, Agent.Omen);
```

This code will write the gamename and tag of all of the players in the match.
```c#
Auth au = Auth.Login(username, password, Region.NA);
MatchData md = MatchData.GetMatchData(au, matchID);
foreach(MatchData.Player ply in md.players)
{
    Console.WriteLine(ply.gameName + "#" + ply.tagLine);
}
```
