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
    public class MessageViewModel : ViewModelBase, IObserver<Message>
    {
        public ObservableCollection<Message> Messages { get; }

        public MessageViewModel(IObservable<Message> messageObservable)
        {
            Messages = new ObservableCollection<Message>();
            messageObservable.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new Exception("No more messages?");
        }

        public void OnError(Exception error)
        {
            throw new Exception("Error in SignalR Hub connection");
        }

        public void OnNext(Message value)
        {
            Messages.Add(value);
        }
    }
}
