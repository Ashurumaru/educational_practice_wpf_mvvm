using educational_practice.ViewModels;
using System.Windows;

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
    }
}
