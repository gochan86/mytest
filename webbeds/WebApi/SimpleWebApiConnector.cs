using Newtonsoft.Json;
using System; 
using System.Net.Http; 

namespace webbeds
{
    public class SimpleWebApiConnector : ISimpleWebApiConnector
    {

        public T Get<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpResponseMessage response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

                else
                {
                    //we can log the error here
                    return default(T); 
                }
            }
            catch (Exception ex)
            { 
                //we can log the error here
                return default(T);
            }
        }
    }
}
