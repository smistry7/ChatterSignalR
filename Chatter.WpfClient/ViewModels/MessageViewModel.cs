using Chatter.BusinessLogic.Models;
using Chatter.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.WpfClient.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        public ObservableCollection<Message> Messages { get; }
        private MessageService _messageService;
        public MessageViewModel()
        {
            _messageService = new MessageService();
            Messages = new ObservableCollection<Message>();
        }
        
    }
}
