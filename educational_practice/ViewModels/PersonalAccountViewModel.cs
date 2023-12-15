using educational_practice.Models;
using educational_practice.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private string selectedStyle;

        private AddUserView addUserView = AddUserView.addUserView;
        private UpdateUserView updateUserView = UpdateUserView.updateUserView;
        private PersonalAccountView personalAccountView = PersonalAccountView.personalAccountView;
        private ThemeMamager themeMamager = LoginViewModel.themeMamager;
        
        public static PersonalAccountViewModel personalAccount;
        public DataBaseLogicModel dbLogic = new DataBaseLogicModel($"Data Source={DataBaseConfig.DataSource};Initial Catalog={DataBaseConfig.InitialCatalog};Integrated Security={DataBaseConfig.IntegratedSecurity}");


        private ObservableCollection<UserModel> users;
        private UserModel selectedUser;
        private UserModel CurrentUser;

        private Visibility stackPanelVisibility = Visibility.Hidden;
        private BitmapImage selectedImage;

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
                if (IsValidAccessLevel(value))
                {
                    accessLevel = value;
                    OnPropertyChanged(nameof(AccessLevel));
                }
                else
                {
                    accessLevel = 0;
                }
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

        public string SelectedStyle
        {
            get { return selectedStyle; }
            set
            {
                if (selectedStyle != value)
                {
                    selectedStyle = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public BitmapImage SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
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
            get
            {
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

        public ICommand AddUserFormCommand { get; private set; }
        public ICommand UpdateUserFormCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand UpdateUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }
        public ICommand LogOutCommand { get; private set; }
        public ICommand OpenImageFileDialogCommand { get; private set; }

        public PersonalAccountViewModel()
        {
            personalAccount = this;
            LoadCurrentUser();
            VisibilityEditingButton();
            UpdateUserCollection();
            AddUserCommand = new RelayCommand(AddNewUser, CanAddUser);
            UpdateUserCommand = new RelayCommand(UpdateUser, CanUpdateUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
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

        private void UpdateUserCollection()
        {
            Users = new ObservableCollection<UserModel>(dbLogic.GetAllUsers());
        }

        private void AddNewUser(object parameter)
        {
            if (!dbLogic.LoginExists(Login))
            {
                dbLogic.CreateUser(Login, Password, FirstName, LastName, MiddleName);
                UpdateUserCollection();
                string message = "Пользователь был добавлен.";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);

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
                updateUserView =new UpdateUserView();
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

        private bool IsValidAccessLevel(int level)
        {
            return level == 0 || level == 1;
        }

        private void UpdateUser(object parameter)
        {
            if (SelectedUser != null)
            {
                if (IsValidAccessLevel(AccessLevel))
                {
                    SelectedUser.Login = Login;
                    SelectedUser.Password = Password;
                    SelectedUser.FirstName = FirstName;
                    SelectedUser.LastName = LastName;
                    SelectedUser.MiddleName = MiddleName;
                    SelectedUser.AccessLevel = AccessLevel;
                    dbLogic.UpdateUser(SelectedUser);
                    string message = "Пользователь обнавлен.";
                    MessageBoxViewModel messageBox = new MessageBoxViewModel();
                    messageBox.ShowMessageBox(message);
                    UpdateUserCollection();

                }
                else
                {
                    string message = "Уровень доступа может быть только 1 или 0 (0 - user, 1 - admin)";
                    MessageBoxViewModel messageBox = new MessageBoxViewModel();
                    messageBox.ShowMessageBox(message);
                }
            }
            else
            {
                string message = "Перезайдите в форму обновления данных пользователя.";
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

        public ImageBrush LoadBackground()
        {
            BitmapImage bitmapImage;
            ImageBrush imageBrush;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Изображения|*.png;*.jpg;*.jpeg|Все файлы|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return null;
            }
            string filePath = openFileDialog.FileName;
            bitmapImage = themeMamager.LoadImage(filePath);
            imageBrush = new ImageBrush(bitmapImage);
            return imageBrush;
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
