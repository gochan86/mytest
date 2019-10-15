using webbeds.Register;
using Unity;
using webbeds.Search.Base;
using System.Diagnostics;

namespace Test3
{
    class TestPrintResults
    {
        static void Main(string[] args)
        {
            var _container = IIocContainer.Register();
            var mySearch = _container.Resolve<ISearch>();

            var stopWatch = new Stopwatch();

            stopWatch.Start();
            newSearch(mySearch);
            stopWatch.Stop();
            System.Diagnostics.Debug.WriteLine("  avail time  -> " + stopWatch.ElapsedMilliseconds);

        }

        public static void newSearch(ISearch mySearch)
        {
            var result = mySearch.SearchAvail(destinationId: 1419, nights: 2);

            foreach (var item in result?.hotels)
            {
                System.Diagnostics.Debug.WriteLine(item.Name);

                foreach (var rate in item.hotelRates)
                {
                    System.Diagnostics.Debug.WriteLine("    -> " + rate.boardType + "  -  " + rate.price);
                }
            }
        }
    }
}
