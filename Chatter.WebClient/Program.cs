using Chatter.APIClient;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            var http = new HttpClient { BaseAddress = new Uri("https://localhost:44359") };
            var chatterHttpClient = new ApiClientFactory(http).BuildChatterApi();

            builder.Services.AddScoped(sp => chatterHttpClient);

            await builder.Build().RunAsync();
        }
    }
}
