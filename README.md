![# ValAPI.Net](https://raw.githubusercontent.com/brianbaldner/ValAPI.Net/main/banner.jpg)

[![Nuget](https://img.shields.io/nuget/v/ValAPI.Net)](https://www.nuget.org/packages/ValAPI.Net/)

A class library for interacting with the Valorant In Game API in the .NET framework.

[Docs](https://github.com/brianbaldner/ValAPI.Net/wiki)

## Usage
Use Nuget to download the package to your project. Make sure to include `using ValAPINet;` at the top of your code. The documentation is found in the [wiki](https://github.com/brianbaldner/ValAPI.Net/wiki), but I reccomend using the docsv2 folder in the ValorantClientAPI documentation found at [https://github.com/RumbleMike/ValorantClientAPI](https://github.com/RumbleMike/ValorantClientAPI).
## Example
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
This code does the same thing as the code before but doesn't require a password. The game has to be running though.
```c#
Auth au = Websocket.GetAuthLocal(Region.NA);
MatchData md = MatchData.GetMatchData(au, matchID);
foreach(MatchData.Player ply in md.players)
{
    Console.WriteLine(ply.gameName + "#" + ply.tagLine);
}
```
## Contributing
If you have anything you want to add, please contribute. Every endpoint makes this project better. 
## Issues and Support
If you need help or want to report bugs, reach out on discord at bigtaco#4761.


## Credits
This would not be possible without RumbleMike's documentation, find him at [@ValorLeaks](https://twitter.com/ValorLeaks) and [@RumbleMikee](https://twitter.com/RumbleMikee). 
