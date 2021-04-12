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
            UserPresence pre = UserPresence.GetPresence();
            string res = JsonConvert.SerializeObject(pre.presences[0]);
            Console.WriteLine(res);
        }
    }
}