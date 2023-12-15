using educational_practice.ViewModels;
using System.Windows;
using System.Windows.Forms;

namespace educational_practice.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateUserView.xaml
    /// </summary>
    public partial class UpdateUserView : Window
    {
        private readonly PersonalAccountViewModel viewModel = PersonalAccountViewModel.personalAccount;
        public static UpdateUserView updateUserView;
        public UpdateUserView()
        {
            InitializeComponent();
            updateUserView = this;
            DataContext = viewModel;
        }

        public void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
