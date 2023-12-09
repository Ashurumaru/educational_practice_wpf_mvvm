using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using System.Data.Entity;
using educational_practice.Models;
using System.Data.SqlClient;
using System.Data;
using educational_practice.Views;

namespace educational_practice.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        readonly DataBaseLogicModel dbLogic = new DataBaseLogicModel("Data Source=DESKTOP-A83RV0A\\MSSQLSERVER04;Initial Catalog=mvvm_project;Integrated Security=True");
        readonly LoginView loginWindow = LoginView.loginWindow;
        private string login;
        private string password;
        private string errorMessage;
        private string firstName;
        private string lastName;
        private string middleName; 
        private string firstPasswordForSignUp;
        private string secondPasswordForSignUp;

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

        public ICommand LoginCommand { get; private set; }
        public ICommand SignUpContinueCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }
        public ICommand OpenGitHubCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(SignIn, CanSignIn);
            SignUpContinueCommand = new RelayCommand(SignUpContinue, CanSignUpContinue);
            SignUpCommand = new RelayCommand(SignUp, CanSignUp);
            OpenGitHubCommand = new RelayCommand(OpenGitHub);
        }

        private bool CanSignUpContinue(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && FirstPasswordForSignUp != null && SecondPasswordForSignUp != null;
        }

        private bool CanSignUp(object parameter)
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        private bool CanSignIn(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && Password != null;
        }

        private bool CheckPassword()
        {
            return FirstPasswordForSignUp == SecondPasswordForSignUp;
        }

        private void SignUpContinue(object parameter)
        {
            if (!dbLogic.LoginExists(Login))
            {
                ErrorMessage = "Такой пользователь уже существует";
            }
            else if (CheckPassword())
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
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel();
            messageBoxViewModel.Message = $"Пользователь зарегистрирован. Войдите в аккаунт.";
            MessageBoxView messageBox = new MessageBoxView();
            messageBox.DataContext = messageBoxViewModel;
            messageBox.ShowDialog();
        }

        public void SwapVisibility()
        {
            if (loginWindow.SecondStackPanel.Visibility == Visibility.Visible)
            {
                loginWindow.FirstStackPanel.Visibility = Visibility.Visible;
                loginWindow.SecondStackPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                loginWindow.FirstStackPanel.Visibility = Visibility.Hidden;
                loginWindow.SecondStackPanel.Visibility = Visibility.Visible;
            }
        }

        private void SignIn(object parameter)
        {
            if (dbLogic.IsValidLogin(Login, Password))
            {
                Views.PersonalAccountView window = new Views.PersonalAccountView();
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
    }
}