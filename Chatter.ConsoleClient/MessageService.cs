using Chatter.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.ConsoleClient
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _http;
        public MessageService(IConfiguration configuration)
        {
            var apiUrl = configuration["api_url"];
            _http = new HttpClient { BaseAddress = new Uri(apiUrl) };
        }
        public async Task<bool> SendMessage(Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            var response = await _http.PostAsync("/Message/SendMessage",
                new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
