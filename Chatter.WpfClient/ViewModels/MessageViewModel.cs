using Chatter.BusinessLogic.Models;
using Chatter.WpfClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;

namespace Chatter.WpfClient.ViewModels
{
    public class MessageViewModel : ViewModelBase, IObserver<Message>
    {
        public ObservableCollection<Message> Messages { get; }

        private readonly IMessageService _messageService;
        public MessageViewModel(IObservable<Message> messageObservable, IMessageService messageService)
        {
            Messages = new ObservableCollection<Message>();
            _messageService = messageService ?? throw new ArgumentNullException(nameof(IMessageService));
            messageObservable.Subscribe(this);
            SetUpSendButton();
        }

        private void SetUpSendButton()
        {
            var sendEnabled = this.WhenAnyValue(
                x => x.Message,
                x => !string.IsNullOrWhiteSpace(x));
            SendMessage = ReactiveCommand.CreateFromTask(async () =>
            {
                var message = new Message
                {
                    Text = Message,
                    SentBy = "Avalonia",
                    SentDate = DateTime.Now,
                    GroupId = 1
                };
                await _messageService.SendMessage(message).ConfigureAwait(false);
                Message = "";
            });
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
        public ReactiveCommand<Unit, Unit> SendMessage { get; private set; }
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _message, value);
            }
        }
    }
}
