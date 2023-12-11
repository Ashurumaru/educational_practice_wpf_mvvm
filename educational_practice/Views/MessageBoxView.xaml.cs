using educational_practice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
