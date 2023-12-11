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
        private AddUserView addUserView = AddUserView.addUserView;
        private UpdateUserView updateUserView = UpdateUserView.updateUserView;
        public event EventHandler<string> MessageBoxShow = delegate { };
        public event EventHandler OpenAddUserWindowRequested;
        readonly public DataBaseLogicModel dbLogic = new DataBaseLogicModel($"Data Source={DataBaseConfig.DataSource};Initial Catalog={DataBaseConfig.InitialCatalog};Integrated Security={DataBaseConfig.IntegratedSecurity}");
        private string errorMessage; 
        private ObservableCollection<UserModel> users;
        private UserModel selectedItem;
        // логика получения уровня доступа через функцию в bdlogic которая запрашивает уровень доступа текущего пользователя
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
                foreach (UserModel user in Users)
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
                foreach (UserModel user in Users)
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
                foreach (UserModel user in Users)
                {
                    user.Password = value;
                }
                OnPropertyChanged(nameof(Password));
            }
        }

        public string FirstName
        {
            get => Users.FirstOrDefault()?.FirstName;
            set
            {
                foreach (UserModel user in Users)
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
                foreach (UserModel user in Users)
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
                foreach (UserModel user in Users)
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
                foreach (UserModel user in Users)
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

        public UserModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public RelayCommand AddUserFormCommand { get; private set; }
        public RelayCommand UpdateUserFormCommand { get; private set; }
        public RelayCommand AddUserCommand { get; private set; }
        public RelayCommand UpdateUserCommand { get; private set; }
        public RelayCommand DeleteUserCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
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
            AddUserCommand = new RelayCommand(AddNewUser, CanAddUser);
            UpdateUserCommand = new RelayCommand(UpdateUser, CanUpdateUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
            CloseCommand = new RelayCommand(CloseForm);
            UpdateUserFormCommand = new RelayCommand(OpenUpdateUserWindow);
            AddUserFormCommand = new RelayCommand(OpenAddUserForm);
        }

        private void CloseForm(object parameter)
        {
            addUserView.Close();
        }

        private void AddNewUser(object parameter)
        {
            if (UserExists())
            {
                dbLogic.CreateUser(Login, Password, FirstName, LastName, MiddleName);
                Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
                string message = "Пользователь был добавлен.";
                MessageBoxShow.Invoke(this, message);
                ClearFields();
            }
            else
            {
                string message = "Пользователь с таким логином уже существует.";
                MessageBoxShow.Invoke(this, message);
            }
        }

        private void OpenAddUserForm(object parameter)
        {
            addUserView = new AddUserView();
            addUserView.ShowDialog();
        }

        private void OpenUpdateUserWindow(object parameter)
        {
            updateUserView = new UpdateUserView();
            updateUserView.ShowDialog();
        }

        private bool UserExists()
        {
            if (dbLogic.LoginExists(Login))
            {
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            Login = String.Empty;
            Password = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            MiddleName = String.Empty;
        }

        private bool CanAddUser(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName); 
        }




        private void UpdateUser(object parameter)
        {
            if (SelectedItem != null)
            {
                UserModel SelectedUser = (UserModel)SelectedItem;
                //dbLogic.UpdateUser(SelectedUser);
                //Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
            }
            else
            {
                string message = "Выберете Выберите элемент для удаления.";
                MessageBoxShow.Invoke(this, message);
            }

        }

        private bool CanUpdateUser(object parameter)
        {
            // сюда логику проверки возможности обновления пользователя
            return true;
        }

        private void DeleteUser(object parameter)
        {
            if (SelectedItem != null)
            {
                dbLogic.DeleteUser(Id);
                Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
            }
            else
            {
                string message = "Выберете Выберите элемент для удаления.";
                MessageBoxShow.Invoke(this, message);
            }
        }

        private bool CanDeleteUser(object parameter)
        {
            // сюдп логику проверки возможности удаления пользователя
            return true;
        }
    }
}
