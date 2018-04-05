using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Networking
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value, Encoding encodingType, string contentType = "application/json")
            : base(JsonConvert.SerializeObject(value), encodingType, contentType)
        {
        }
    }
}
