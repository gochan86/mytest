using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using webbeds.Connector.Base;
using webbeds.Dtos;

namespace webbeds.Connector.CheapAwsome
{
    public class CheapAwsomeConnector : IHotelConnector
    {
        private readonly ISimpleWebApiConnector _simpleWebApiConnector;
        public CheapAwsomeConnector(ISimpleWebApiConnector simpleWebApiConnector)
        {
            _simpleWebApiConnector = simpleWebApiConnector;
        }

        public MyResponseDto GetHotels(int destinationId, int nights)
        {
            try
            {
                var url = string.Format(ConfigurationManager.AppSettings["CheapAwsomeEndPoint"], destinationId, nights, ConfigurationManager.AppSettings["SecretAuthenticationCheapAwsome"]);

                var data = _simpleWebApiConnector.Get<List<CheapAwsomeDto>>(url);

                return getDataFromResponse(data, nights);
            }
            catch (Exception)
            {
                //we can log the error here
                return new MyResponseDto();
            }
        }

        public MyResponseDto getDataFromResponse(List<CheapAwsomeDto> data, int nights)
        {
            var myResponseDto = new MyResponseDto();
            myResponseDto.hotels = new List<MyHotelDto>();

            if (data != null)
            {
                foreach (var hotel in data)
                {
                    if (hotel != null)
                    {
                        var myHotelDtoToAdd = new MyHotelDto();

                        if (!String.IsNullOrEmpty(hotel.hotel.name))
                        {
                            myHotelDtoToAdd.Name = hotel.hotel.name;
                        }
                        else
                        {
                            //here we can launch a task or event to save for example in a table of warnings: "warning propertyID = id has no name",
                            //and in another process send a sumary of warnings for example each hour
                            continue;
                        }

                        myHotelDtoToAdd.hotelRates = new List<MyHotelRateDto>();

                        foreach (var rate in hotel.rates)
                        {
                            if (rate != null)
                            {

                                if (rate.value > 0)
                                {
                                    var myHotelRateDtoToAdd = new MyHotelRateDto();

                                    if (rate.rateType == "Stay")
                                    {
                                        myHotelRateDtoToAdd.price = rate.value;
                                    }
                                    else if (rate.rateType == "PerNight")
                                    {
                                        myHotelRateDtoToAdd.price = rate.value * nights;
                                    }

                                    myHotelRateDtoToAdd.boardType = rate.boardType;

                                    myHotelDtoToAdd.hotelRates.Add(myHotelRateDtoToAdd);
                                }
                                else
                                {
                                    //here we can launch a task or event to save for example in a table of warnings: "warning propertyID = id has incorrect price",
                                    //and in another process send a sumary of warnings for example each hour
                                    continue;
                                }
                            }
                        }

                        if (myHotelDtoToAdd.hotelRates.Any())
                        {
                            myResponseDto.hotels.Add(myHotelDtoToAdd);
                        }
                        else
                        {
                            //here we can launch a task or event to save for example in a table of warnings: "warning propertyID = id has no rates",
                            //and in another process send a sumary of warnings for example each hour 
                        }
                    }
                }
            }

            return myResponseDto;
        }
    }
}
