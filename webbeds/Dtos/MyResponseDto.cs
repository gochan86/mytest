using System.Collections.Generic;

namespace webbeds.Dtos
{
    public class MyResponseDto
    {
        public List<MyHotelDto> hotels { get; set; }
    }
    public class MyHotelDto
    {
        public string Name { get; set; }
        public List<MyHotelRateDto> hotelRates { get; set; }
    }
    public class MyHotelRateDto
    {
        public string boardType { get; set; }
        public double price { get; set; }
    }
}
