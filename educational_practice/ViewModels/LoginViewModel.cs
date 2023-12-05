using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data.Entity;
using educational_practice.Models;
using System.Data.SqlClient;
using System.Data;

namespace educational_practice.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private string login;
        private string password;
        private string errorMessage;
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

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(LoginIn, CanLogin);
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && Password != null;
        }

        private void LoginIn(object parameter)
        {
            if (IsValidLogin(Login, Password))
            {
                MessageBox.Show("Успешный вход!");
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
