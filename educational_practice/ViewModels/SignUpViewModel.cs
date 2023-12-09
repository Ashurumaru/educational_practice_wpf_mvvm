using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace educational_practice.ViewModels
{
    internal class SignUpViewModel : BaseViewModel
    {
        LoginWindow loginWindow = LoginWindow.loginWindow;
        private string login;
        private string errorMessage;
        private string firstName;
        private string lastName;
        private string middleName;
        private string firstPasswordForSignUp;
        private string secondPasswordForSignUp;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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


        public ICommand SignUpContinueCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }

        public SignUpViewModel()
        {
            SignUpContinueCommand = new CommandViewModel(SignUpContinue, CanSignUpContinue);
            SignUpCommand = new CommandViewModel(SignUp, CanSignUp);
        }

        private bool CanSignUpContinue(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && FirstPasswordForSignUp != null && SecondPasswordForSignUp != null;
        }

        private bool CanSignUp(object parameter)
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        private bool CheckPassword()
        {
            bool passwordsMatch = FirstPasswordForSignUp == SecondPasswordForSignUp;

            if (!passwordsMatch)
            {
                ErrorMessage = "Первый пароль не совпадает с вторым";
            }

            return passwordsMatch;
        }

        private bool LoginExists(string login)
        {
            if (login == "admin")
            {
                ErrorMessage = "Такой пользователь уже существует";
            }

            return true;
        }

        private void SignUpContinue(object parameter)
        {
            if (LoginExists(Login) && CheckPassword())
            {
                ErrorMessage = "";
                SwapVisibility();
            }
        }

        private void SignUp(object parameter)
        {
            MessageBox.Show($"аккаунт зарегистрирован. вы {FirstName} {LastName} {MiddleName}");
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
    }
}
