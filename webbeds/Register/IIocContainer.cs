using Unity;
using webbeds.Cache;
using webbeds.Connector.Base;
using webbeds.Connector.CheapAwsome;
using webbeds.Search.Base;

namespace webbeds.Register
{
    public static class IIocContainer
    {
        private static UnityContainer _current = new UnityContainer();

        public static UnityContainer Register()
        {
            _current.RegisterType<IHotelConnector, CheapAwsomeConnector>();
            _current.RegisterType<ISimpleWebApiConnector, SimpleWebApiConnector>();
            _current.RegisterType<ISearch, webbeds.Search.Search>();
            _current.RegisterSingleton<ICache, SystemRuntimeCache>();
             
            return _current;
        }
    }
}
