using System;
using ValAPINet;
namespace ValAPINetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Auth au = Auth.Login("bigtaco21", "Epicbrian05", Region.NA);
            Storefront mmrvas = Storefront.GetOffers(au);
            Console.WriteLine(mmrvas.SkinsPanelLayout.SingleItemOffers[0]);
            Console.ReadKey();
        }
    }
}