using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Chatter.ConsoleClient
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            HubConnection _connection =  new HubConnectionBuilder()
                .WithUrl("https://localhost:44359/MessageHub")
                .Build();

            _connection.Closed += async (error) =>
            {
                Console.WriteLine("CorBlimey");
                await _connection.StartAsync();
            };
            await _connection.StartAsync();

        }
    }
}
