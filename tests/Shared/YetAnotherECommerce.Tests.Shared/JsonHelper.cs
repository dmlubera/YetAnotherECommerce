using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Text;

namespace YetAnotherECommerce.Tests.Shared
{
    public static class JsonHelper
    {
        public static StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
    }
}