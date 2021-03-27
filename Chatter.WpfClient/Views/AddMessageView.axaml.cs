using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Chatter.WpfClient.Views
{
    public class AddMessageView : UserControl
    {
        public AddMessageView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
