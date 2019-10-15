
namespace webbeds
{
    public interface ISimpleWebApiConnector
    {
        T Get<T>(string url);
    }
}
