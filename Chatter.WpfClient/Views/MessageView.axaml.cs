using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Chatter.WpfClient.Views
{
    public class MessageView : UserControl
    {
        public MessageView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
