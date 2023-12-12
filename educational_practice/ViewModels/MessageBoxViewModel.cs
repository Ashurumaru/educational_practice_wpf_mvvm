using educational_practice.Views;
using System.Windows.Input;

namespace educational_practice.ViewModels
{
    internal class MessageBoxViewModel : BaseViewModel
    {
        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand CloseCommand { get; private set; }
        public MessageBoxViewModel()
        {
            CloseCommand = new RelayCommand(CloseMessageBox);
        }

        private void CloseMessageBox(object parameter)
        {
            MessageBoxView messageBox = MessageBoxView.messageBox;
            messageBox.Close();
        }

        public void ShowMessageBox(string message)
        {
            MessageBoxViewModel viewModel = new MessageBoxViewModel { Message = message };
            MessageBoxView view = new MessageBoxView { DataContext = viewModel };
            view.ShowDialog();
        }
    }
}