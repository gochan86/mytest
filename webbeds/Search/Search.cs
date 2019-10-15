using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using webbeds.Cache;
using webbeds.Connector.Base;
using webbeds.Dtos;
using webbeds.Search.Base;

namespace webbeds.Search
{
    public class Search : ISearch
    {
        private readonly IHotelConnector _hotelConnector;
        private readonly ICache _cache;
        public Search(IHotelConnector hotelConnector, ICache cache)
        {
            _hotelConnector = hotelConnector;
            _cache = cache;
        }

        public MyResponseDto SearchAvail(int destinationId, int nights)
        {
            var result = new MyResponseDto();
            result.hotels = new List<MyHotelDto>();

            try
            {
                var itemInCache = _cache.Get<MyResponseDto>("search[" + destinationId + "#" + nights + "]");

                if (itemInCache != null) { return itemInCache; }


                var tasks = new List<Task>();

                var connectivityTask = Task.Factory.StartNew(() => CreateCheapAwsomeConnectorTask(destinationId: destinationId, nights: nights, result: result));
                connectivityTask.ContinueWith(t => { }, TaskContinuationOptions.OnlyOnFaulted);
                tasks.Add(connectivityTask);

                Task.WaitAll(tasks.ToArray<Task>(), TimeSpan.FromMilliseconds(int.Parse(ConfigurationManager.AppSettings["SearchTimeOutMilliseconds"])));

                if (result != null & result.hotels.Any())
                {
                    _cache.Set("search[" + destinationId + "#" + nights + "]", result);
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        public void CreateCheapAwsomeConnectorTask(int destinationId, int nights, MyResponseDto result)
        {
            var resultAux = _hotelConnector.GetHotels(destinationId: destinationId, nights: nights);

            if (resultAux.hotels != null)
            {
                result.hotels.AddRange(resultAux.hotels);
            }

        }
    }
}
