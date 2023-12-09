﻿using System;
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
        readonly LoginWindow loginWindow = LoginWindow.loginWindow;
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
            LoginCommand = new CommandViewModel(SignIn, CanSignIn);
            SignUpContinueCommand = new CommandViewModel(SignUpContinue, CanSignUpContinue);
            SignUpCommand = new CommandViewModel(SignUp, CanSignUp);
            OpenGitHubCommand = new CommandViewModel(OpenGitHub);
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
            bool passwordsMatch = FirstPasswordForSignUp == SecondPasswordForSignUp;

            if (!passwordsMatch)
            {
                ErrorMessage = "Первый пароль не совпадает с вторым";
            }

            return passwordsMatch;
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

        private void SignIn(object parameter)
        {
            if (IsValidLogin(Login, Password))
            {
                Views.PersonalAccountForm window = new Views.PersonalAccountForm();
                window.Show();
                loginWindow.Close();
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

        private bool LoginExists(string login)
        {
            if (login == "admin")
            {
                ErrorMessage = "Такой пользователь уже существует";
            }

            return true;
        }

        private void OpenGitHub(object parameter)
        {
            Process.Start("https://github.com/Ashurumaru");
        }
    }
}
