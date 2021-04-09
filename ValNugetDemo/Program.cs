using System;
using System.IO;
using ValAPINet;
namespace ValAPINetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Auth au = Auth.Login("username", "password", Region.NA);
            GetParty gp = GetParty.Party(au);
            PartyInfo pi = PartyInfo.Party(au, gp.CurrentPartyID);
            Console.WriteLine(pi.Members[0].Subject);
        }
    }
}