using educational_practice.Models;
using educational_practice.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace educational_practice.ViewModels
{
    internal class PersonalAccountViewModel : BaseViewModel
    {
        private int id;
        private string login;
        private string password;
        private string firstName;
        private string lastName;
        private string middleName;
        private int accessLevel;
        private string CurrentFio;
        private AddUserView addUserView = AddUserView.addUserView;
        private UpdateUserView updateUserView = UpdateUserView.updateUserView;
        private PersonalAccountView personalAccountView = PersonalAccountView.personalAccountView;
        private LoginViewModel LoginViewModel = LoginViewModel.loginViewModel;
        readonly public DataBaseLogicModel dbLogic = new DataBaseLogicModel($"Data Source={DataBaseConfig.DataSource};Initial Catalog={DataBaseConfig.InitialCatalog};Integrated Security={DataBaseConfig.IntegratedSecurity}");
        private ObservableCollection<UserModel> users;
        private UserModel selectedUser;
        private UserModel CurrentUser;
        Visibility stackPanelVisibility = Visibility.Hidden;
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

        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public Visibility StackPanelVisibility
        {
            get => stackPanelVisibility;
            set
            {
                stackPanelVisibility = value;
                OnPropertyChanged(nameof(StackPanelVisibility));
            }
        }

        public int CurrentAccessLevel
        {
            get => CurrentUser.AccessLevel;
        }
        string currentAccessLevelView = "Уровень доступа: ";
        public string CurrentAccessLevelView
        {
            get {
                if (CurrentAccessLevel == 1)
                    return currentAccessLevelView += "Администратор";
                else
                    return currentAccessLevelView += "Пользователь";
            }
        }

        public string FIO
        {
            get => $"ФИО: {CurrentUser.FirstName} {CurrentUser.LastName} {CurrentUser.MiddleName}";
        }

        public RelayCommand AddUserFormCommand { get; private set; }
        public RelayCommand UpdateUserFormCommand { get; private set; }
        public RelayCommand AddUserCommand { get; private set; }
        public RelayCommand UpdateUserCommand { get; private set; }
        public RelayCommand DeleteUserCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }  
        public RelayCommand LogOutCommand { get; private set; }
        public PersonalAccountViewModel()
        {
            LoadCurrentUser();
            VisibilityEditingButton();
            Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
            AddUserCommand = new RelayCommand(AddNewUser, CanAddUser);
            UpdateUserCommand = new RelayCommand(UpdateUser, CanUpdateUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            CloseCommand = new RelayCommand(CloseForm);
            UpdateUserFormCommand = new RelayCommand(OpenUpdateUserWindow);
            AddUserFormCommand = new RelayCommand(OpenAddUserForm);
            LogOutCommand = new RelayCommand(LogOut);
        }
        private void LogOut(object parameter)
        {
            LoginView window = new LoginView();
            window.Show();
            personalAccountView.Close();
        }
        private void LoadCurrentUser()
        {
            LoginViewModel loginViewModel = LoginViewModel.loginViewModel;
            CurrentUser = loginViewModel.CurrentUser;
        }

        private void VisibilityEditingButton()
        {
            if (CurrentAccessLevel == 1)
            {
                StackPanelVisibility = Visibility.Visible;
            }
        }


        private void CloseForm(object parameter)
        {
            addUserView?.Close();
            updateUserView?.Close();
        }

        private void AddNewUser(object parameter)
        {
            if (!dbLogic.LoginExists(Login))
            {
                dbLogic.CreateUser(Login, Password, FirstName, LastName, MiddleName);
                Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
                string message = "Пользователь был добавлен.";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
                addUserView?.Close();
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
            addUserView = new AddUserView();
            addUserView.ShowDialog();
        }

        private void OpenUpdateUserWindow(object parameter)
        {
            if (SelectedUser != null)
            {
                Login = SelectedUser.Login;
                Password = SelectedUser.Password;
                FirstName = SelectedUser.FirstName;
                LastName = SelectedUser.LastName;
                MiddleName = SelectedUser.MiddleName;
                AccessLevel = SelectedUser.AccessLevel;
                updateUserView = new UpdateUserView();
                updateUserView.ShowDialog();
            }
            else
            {
                string message = "Выберите пользователя для изменения.";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
        }

        private bool CanAddUser(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName); 
        }

        private bool CanUpdateUser(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(AccessLevel.ToString());
        }

        private void UpdateUser(object parameter)
        {
            if (SelectedUser != null)
            {        
                dbLogic.UpdateUser(SelectedUser);
                Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
                updateUserView?.Close();
            }
            else
            {
                string message = "Выберите пользователя для изменения.";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
        }

        private void DeleteUser(object parameter)
        {
            if (SelectedUser != null)
            {
                if (SelectedUser.AccessLevel != 1)
                {
                    dbLogic.DeleteUser(SelectedUser.Id);
                    Users.Remove(SelectedUser);
                    string message = "Пользователь удален.";
                    MessageBoxViewModel messageBox = new MessageBoxViewModel();
                    messageBox.ShowMessageBox(message);
                }
                else
                {
                    string message = "Нельзя удалить пользователя с уровнем доступа администратор.";
                    MessageBoxViewModel messageBox = new MessageBoxViewModel();
                    messageBox.ShowMessageBox(message);
                }
            }
            else
            {
                string message = "Выберите пользователя для удаления.";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
        }
    }
}
