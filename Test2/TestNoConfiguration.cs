using Unity;
using webbeds.Register; 
using webbeds.Search.Base;

namespace Test2
{
    class TestNoConfiguration
    {
        static void Main(string[] args)
        {
            var _container = IIocContainer.Register();
            var mySearch = _container.Resolve<ISearch>();

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
