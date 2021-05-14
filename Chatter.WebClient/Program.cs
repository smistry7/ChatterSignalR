using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.Modal;
using Chatter.WebClient.Services;

namespace Chatter.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44359") });
            builder.Services.AddBlazoredModal();
            builder.Services.AddSingleton<IHubConnectionProvider, HubConnectionProvider>();
            await builder.Build().RunAsync();
        }
    }
}
