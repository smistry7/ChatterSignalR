using Chatter.BusinessLogic.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.WpfClient.ViewModels
{
    public class AddMessageViewModel : ViewModelBase
    {
        public string _text;
        public AddMessageViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Text,
                x => !string.IsNullOrWhiteSpace(x));
            Ok = ReactiveCommand.Create(
                () => new Message
                {
                    Text = Text,
                    SentBy = "Avalonia",
                    SentDate = DateTime.Now,
                    GroupId = 1
                }, okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
        public ReactiveCommand<Unit, Message> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
