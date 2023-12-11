using educational_practice.CustomControls;
using educational_practice.Models;
using educational_practice.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educational_practice.ViewModels
{
    internal class DataBaseViewModel : BaseViewModel
    {
        private int id;
        private string login;
        private string password;
        private string firstName;
        private string lastName;
        private string middleName;
        private int accessLevel;
        private AddUserView addUserView = AddUserView.addUserView;
        private UpdateUserView updateUserView = UpdateUserView.updateUserView;
        readonly public DataBaseLogicModel dbLogic = new DataBaseLogicModel($"Data Source={DataBaseConfig.DataSource};Initial Catalog={DataBaseConfig.InitialCatalog};Integrated Security={DataBaseConfig.IntegratedSecurity}");

        private ObservableCollection<UserModel> users;
        private UserModel selectedItem;
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
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
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

        public int AccessLevel
        {
            get => accessLevel;
            set
            {
                accessLevel = value;
                OnPropertyChanged(nameof(AccessLevel));
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
        public DataBaseViewModel()
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
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
                ClearFields();
            }
            else
            {
                string message = "Пользователь с таким логином уже существует.";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
        }

        private void OpenAddUserForm(object parameter)
        {
            ClearFields();
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
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
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
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
        }

        private bool CanDeleteUser(object parameter)
        {
            // сюдп логику проверки возможности удаления пользователя
            return true;
        }
    }

}
