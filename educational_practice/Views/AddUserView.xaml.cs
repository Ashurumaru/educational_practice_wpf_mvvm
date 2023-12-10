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
    /// Логика взаимодействия для AddUserView.xaml
    /// </summary>
    public partial class AddUserView : Window
    {
        private readonly PersonalAccountViewModel viewModel;
        public static AddUserView addUserView;
        public AddUserView()
        {
            InitializeComponent();
            addUserView = this;
            viewModel = new PersonalAccountViewModel();
            DataContext = viewModel;
        }
    }
}
