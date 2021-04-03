using Chatter.BusinessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.WpfClient.Services
{
    public class MessageService : IMessageService
    {
        private HttpClient _http;
        public MessageService(string apiUrl)
        {
            _http = new HttpClient()
            {
                BaseAddress = new Uri(apiUrl)
            };

        }
        public async Task SendMessage(Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            await _http.PostAsync("/Message/SendMessage", new StringContent(json, Encoding.UTF8, "application/json"));
        }
        public async Task<IEnumerable<Message>> GetMessages()
        {
            var response = await _http.GetAsync("Message/GetMessages");
            return JsonConvert.DeserializeObject<IEnumerable<Message>>(await response.Content.ReadAsStringAsync());
        }


    }
}

