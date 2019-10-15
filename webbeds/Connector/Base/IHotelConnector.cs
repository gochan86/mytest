using webbeds.Dtos;

namespace webbeds.Connector.Base
{
    public interface IHotelConnector
    {
        MyResponseDto GetHotels(int destinationId, int nights);
    }
}
