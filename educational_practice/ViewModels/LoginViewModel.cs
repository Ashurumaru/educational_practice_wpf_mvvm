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

        public ICommand LoginCommand { get; private set; }
        public ICommand SignUpContinueCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }
        public ICommand OpenGitHubCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(LoginIn, CanLogin);
            //SignUpContinueCommand = new ViewModelCommand(LoginIn, CanSignUpContinue);
            //SignUpCommand = new ViewModelCommand(LoginIn, CanSignUp);
            OpenGitHubCommand = new ViewModelCommand(OpenGitHub);
        }

        private bool CanSignUpContinue(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && Password != null && Password != null;
        }

        private bool CanSignUp(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Login);
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && Password != null;
        }

        private void LoginIn(object parameter)
        {
            if (IsValidLogin(Login, Password))
            {
                Views.PersonalAccountForm window = new Views.PersonalAccountForm();
                window.Show();
                Window ActivatedWindow = LoginWindow.ActivatedWindow;
                ActivatedWindow.Close();
            }
            else
            {
                ErrorMessage = "Неправильный логин или пароль";
            }
        }

        private bool IsValidLogin(string login, string password)
        {
            bool validUser;
            using (var connection = new SqlConnection("Data Source=DESKTOP-A83RV0A\\MSSQLSERVER04;Initial Catalog=mvvm_project;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where login=@Login and [password]=@Password";
                command.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        private void OpenGitHub(object parameter)
        {
            Process.Start("https://github.com/Ashurumaru");
        }
    }
}
