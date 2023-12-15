using educational_practice.ViewModels;
using System.Windows;

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

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
