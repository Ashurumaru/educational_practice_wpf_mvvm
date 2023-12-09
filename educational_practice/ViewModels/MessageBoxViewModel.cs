using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Input;
using educational_practice.Views;

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

        public ICommand CloseCommand { get; }

        public MessageBoxViewModel()
        {
            CloseCommand = new RelayCommand(Close);
        }

        private void Close(object parameter)
        {
            MessageBoxView messageBox = MessageBoxView.messageBox;
            messageBox.Close();
        }
    }
}