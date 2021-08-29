using Newtonsoft.Json;
using System;
using System.IO;
using ValAPINet;
using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace ValAPINetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Auth auth = Auth.Login("bigtaco21", "TempPass01", Region.NA);
            AccountXP xp = AccountXP.GetOffers(auth);
        }
    }
}