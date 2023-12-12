using educational_practice.ViewModels;
using System.Windows;

namespace educational_practice.Views
{
    /// <summary>
    /// Логика взаимодействия для MessageBoxView.xaml
    /// </summary>
    public partial class MessageBoxView : Window
    {
        public static MessageBoxView messageBox;

        public MessageBoxView()
        {
            InitializeComponent();
            messageBox = this;
        }

        public void HandleMessageBox(object sender, string message)
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel();
            messageBoxViewModel.Message = message;
            messageBox.DataContext = messageBoxViewModel;
            messageBox.ShowDialog();

        }
    }
}
