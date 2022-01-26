using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Chatter.APIClient
{
    public interface IChatterApi 
    {
        [Get("/message/getmessages")]
        Task<IEnumerable<Message>> GetMessages();
        [Get("/message/getmessages")]
        Task<ApiResponse<IEnumerable<Message>>> GetMessagesResponse();

        [Get("/message/getmessage")]
        Task<Message> GetMessage(int id);

        [Get("/message/getmessage")]
        Task<ApiResponse<Message>> GetMessageResponse(int id);
        [Post("/message/sendmessage")]
        Task<int> SendMessage(Message message);

        [Post("/message/sendmessage")]
        Task<ApiResponse<int>> SendMessageResponse(Message message);

    }

    public class Message
    {
        public int MessageId { get; set; }
        public string? Text { get; set; }
        public DateTime SentDate { get; set; }
        public string? SentBy { get; set; }
        public int GroupId { get; set; }
        public override string ToString()
        {
            return $"{this.SentBy}: {Text}";
        }

    }
}