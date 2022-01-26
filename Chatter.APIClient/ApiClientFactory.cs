using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Chatter.APIClient
{
    public class ApiClientFactory
    {
        private readonly HttpClient client;

        public ApiClientFactory(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:50989");
            this.client = client;
        }
        public IChatterApi BuildChatterApi()
        {
            var serialiser = new NewtonsoftJsonContentSerializer(new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            return RestService.For<IChatterApi>(client, new RefitSettings
            {
                ContentSerializer = serialiser
            });
        }
    }
}
