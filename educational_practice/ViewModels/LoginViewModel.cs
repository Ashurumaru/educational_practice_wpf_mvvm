using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using educational_practice.Models;
using educational_practice.Views;
using System.Windows.Forms;
using System;

namespace educational_practice.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        private string login;
        private string password;
        private string errorMessage;
        private string firstName;
        private string lastName;
        private string middleName;
        private string firstPasswordForSignUp;
        private string secondPasswordForSignUp;

        public LoginView loginWindow = LoginView.loginWindow;
        public static LoginViewModel loginViewModel;
        readonly public DataBaseLogicModel dbLogic = new DataBaseLogicModel($"Data Source={DataBaseConfig.DataSource};Initial Catalog={DataBaseConfig.InitialCatalog};Integrated Security={DataBaseConfig.IntegratedSecurity}");
        public static ThemeMamager themeMamager;
        private UserModel currentUser;

        private Visibility firstStackPanelVisibility = Visibility.Visible;
        private Visibility secondStackPanelVisibility = Visibility.Hidden;
        private bool isSignInTabSelected = true;

        public UserModel CurrentUser
        {
            get => currentUser;
            private set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string MiddleName
        {
            get => middleName;
            set
            {
                middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }

        public string FirstPasswordForSignUp
        {
            get => firstPasswordForSignUp;
            set
            {
                firstPasswordForSignUp = value;
                OnPropertyChanged(nameof(FirstPasswordForSignUp));
            }
        }

        public string SecondPasswordForSignUp
        {
            get => secondPasswordForSignUp;
            set
            {
                secondPasswordForSignUp = value;
                OnPropertyChanged(nameof(SecondPasswordForSignUp));
            }
        }


        public Visibility FirstStackPanelVisibility
        {
            get => firstStackPanelVisibility;
            set
            {
                firstStackPanelVisibility = value;
                OnPropertyChanged(nameof(FirstStackPanelVisibility));
            }
        }

        public Visibility SecondStackPanelVisibility
        {
            get => secondStackPanelVisibility;
            set
            {
                secondStackPanelVisibility = value;
                OnPropertyChanged(nameof(SecondStackPanelVisibility));
            }
        }

        public bool IsSignInTabSelected
        {
            get => isSignInTabSelected;
            set
            {
                isSignInTabSelected = value;
                OnPropertyChanged(nameof(IsSignInTabSelected));
            }
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand SignUpContinueCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }
        public ICommand OpenGitHubCommand { get; private set; }

        public LoginViewModel()
        {
            loginViewModel = this;
            if (themeMamager == null)
                themeMamager = new ThemeMamager();
            LoginCommand = new RelayCommand(SignIn, CanSignIn);
            SignUpContinueCommand = new RelayCommand(SignUpContinue, CanSignUpContinue);
            SignUpCommand = new RelayCommand(SignUp, CanSignUp);
            OpenGitHubCommand = new RelayCommand(OpenGitHub);
        }

        private bool CanSignUpContinue(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(FirstPasswordForSignUp) && !string.IsNullOrWhiteSpace(SecondPasswordForSignUp);
        }

        private bool CanSignUp(object parameter)
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        private bool CanSignIn(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void SignUpContinue(object parameter)
        {
            if (dbLogic.LoginExists(Login))
            {
                ErrorMessage = "Такой пользователь уже существует";
            }
            else if (FirstPasswordForSignUp != SecondPasswordForSignUp)
            {
                ErrorMessage = "Первый пароль не совпадает с вторым";
            }
            else
            {
                ErrorMessage = "";
                SwapVisibility();
            }
        }

        private void SignUp(object parameter)
        {
            dbLogic.CreateUser(Login, FirstPasswordForSignUp, FirstName, LastName, MiddleName);
            SwapTabItemOnSignIn();
            SwapVisibility();
            ClearFields();
            string message = "Пользователь зарегистрирован. Войдите в аккаунт.";
            MessageBoxViewModel messageBox = new MessageBoxViewModel();
            messageBox.ShowMessageBox(message);
        }

        private void SignIn(object parameter)
        {
            if (dbLogic.IsValidLogin(Login, Password))
            {
                CurrentUser = dbLogic.GetUserByLogin(Login);
                PersonalAccountView window = new PersonalAccountView();
                window.Show();
                loginWindow.Close();
            }
            else
            {
                ErrorMessage = "Неправильный логин или пароль";
            }
        }

        private void OpenGitHub(object parameter)
        {
            Process.Start("https://github.com/Ashurumaru");
        }

        public void SwapVisibility()
        {
            FirstStackPanelVisibility = FirstStackPanelVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            SecondStackPanelVisibility = SecondStackPanelVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void SwapTabItemOnSignIn()
        {
            IsSignInTabSelected = true;
        }

        private void ClearFields()
        {
            Login = string.Empty;
            Password = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            MiddleName = string.Empty;
            FirstPasswordForSignUp = string.Empty;
            SecondPasswordForSignUp = string.Empty;
        }

        public ImageBrush SetBackground()
        {
            BitmapImage bitmapImage;
            ImageBrush imageBrush;
            if (themeMamager.GetImage() == null)
            {
                bitmapImage = themeMamager.LoadDefaultImage();
                imageBrush = new ImageBrush(bitmapImage);
                return imageBrush;
            }
            else
            {
                bitmapImage = themeMamager.GetImage();
                imageBrush = new ImageBrush(bitmapImage);
                return imageBrush;
            }
        }
    }
}
