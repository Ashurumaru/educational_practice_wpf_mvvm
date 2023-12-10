using educational_practice.Models;
using educational_practice.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace educational_practice.ViewModels
{
    internal class PersonalAccountViewModel : BaseViewModel
    {
        public event EventHandler<string> MessageBoxShow;
        public event EventHandler AddNewUserShow;
        readonly public DataBaseLogicModel dbLogic = new DataBaseLogicModel($"Data Source={DataBaseConfig.DataSource};Initial Catalog={DataBaseConfig.InitialCatalog};Integrated Security={DataBaseConfig.IntegratedSecurity}");
        private string errorMessage; 
        private string fio;
        private string message;
        private ObservableCollection<UserModel> users;
        // логика получения уровня доступа через функцию в bdlogic которая запрашивает уровень доступа текущего пользователя
        //
        public ObservableCollection<UserModel> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public int Id
        {
            get => Users.FirstOrDefault().Id;
            set
            {
                foreach (var user in Users)
                {
                    user.Id = value;
                }
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Login
        {
            get => Users.FirstOrDefault()?.Login;
            set
            {
                foreach (var user in Users)
                {
                    user.Login = value;
                }
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => Users.FirstOrDefault()?.Password;
            set
            {
                foreach (var user in Users)
                {
                    user.Password = value;
                }
                OnPropertyChanged(nameof(Password));
            }
        }
        public string FIO
        {
            get => $"{FirstName} {LastName} {MiddleName}";
            set
            {
                fio = value;
                OnPropertyChanged(nameof(fio));
            }
        }

        public string FirstName
        {
            get => Users.FirstOrDefault()?.FirstName;
            set
            {
                foreach (var user in Users)
                {
                    user.FirstName = value;
                }
                OnPropertyChanged(nameof(FirstName));
            }
        }


        public string LastName
        {
            get => Users.FirstOrDefault()?.LastName;
            set
            {
                foreach (var user in Users)
                {
                    user.LastName = value;
                }
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string MiddleName
        {
            get => Users.FirstOrDefault()?.MiddleName;
            set
            {
                foreach (var user in Users)
                {
                    user.MiddleName = value;
                }
                OnPropertyChanged(nameof(MiddleName));
            }
        }

        public int AccessLevel
        {
            get => Users.FirstOrDefault().AccessLevel;
            set
            {
                foreach (var user in Users)
                {
                    user.AccessLevel = value;
                }
                OnPropertyChanged(nameof(AccessLevel));
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

        public RelayCommand AddUserFormCommand;
        public RelayCommand AddUserCommand;
        public RelayCommand UpdateUserCommand;
        public RelayCommand DeleteUserCommand;
        public PersonalAccountViewModel()
        {
            Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());

            Users.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Id));
                OnPropertyChanged(nameof(Login));
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(MiddleName));
                OnPropertyChanged(nameof(AccessLevel));
                OnPropertyChanged(nameof(ErrorMessage));
            };
            AddUserFormCommand = new RelayCommand(OpenAddUserWindow);
            AddUserCommand = new RelayCommand(AddNewUser, CanAddUser);
            UpdateUserCommand = new RelayCommand(UpdateUser, CanUpdateUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
        }

        private void AddNewUser(object parameter)
        {
            UserModel newUser = GetUserDataFromDialog(); 
            dbLogic.CreateUser(newUser.Login, newUser.Password, newUser.FirstName, newUser.LastName, newUser.MiddleName);
            Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
            message = "Пользователь был добавлен.";
        }

        private void OpenAddUserWindow(object parameter)
        {
            AddUserView addUserView = new AddUserView();
            addUserView.ShowDialog(); 
        }

        private UserModel GetUserDataFromDialog()
        {
            UserModel newUser = new UserModel
            {
                Login = Login,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                AccessLevel = 0
            };

            return newUser;
        }

        private bool CanAddUser(object parameter)
        {
            if (dbLogic.LoginExists(Login))
            {
                ErrorMessage = "Такой пользователь уже существует";
                MessageBoxShow.Invoke(this, message);
                return false;
            }
            return true;
        }

        private void UpdateUser(object parameter)
        {
            //dbLogic.UpdateUser();
            //Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
        }

        private bool CanUpdateUser(object parameter)
        {
            // сюда логику проверки возможности обновления пользователя
            return true;
        }

        private void DeleteUser(object parameter)
        {
            //dbLogic.DeleteUser(Id);
            //Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
        }

        private bool CanDeleteUser(object parameter)
        {
            // сюдп логику проверки возможности удаления пользователя
            return true;
        }
    }
}
