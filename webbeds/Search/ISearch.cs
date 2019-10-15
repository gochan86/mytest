using webbeds.Dtos;

namespace webbeds.Search.Base
{
    public interface ISearch
    {
        MyResponseDto SearchAvail(int destinationId, int nights);
    }
}
