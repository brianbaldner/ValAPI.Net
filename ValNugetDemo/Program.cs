using System;
using ValAPINet;
namespace ValAPINetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Auth au = Auth.Login("username", "password", Region.NA);
            Storefront mmrvas = Storefront.GetOffers(au);
            Console.WriteLine(mmrvas.SkinsPanelLayout.SingleItemOffers[0]);
            Console.ReadKey();
        }
    }
}