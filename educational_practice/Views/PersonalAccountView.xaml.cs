using educational_practice.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace educational_practice.Views
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountView.xaml
    /// </summary>
    public partial class PersonalAccountView : Window
    {
        public static PersonalAccountView personalAccountView;
        private PersonalAccountViewModel viewModel;
        public PersonalAccountView()
        {
            personalAccountView = this;
            viewModel = new PersonalAccountViewModel();
            DataContext = viewModel;
            InitializeComponent();
            SetBackgroundGrid();
            cmbBoxStyle.SelectionChanged += ThemeChange;
        }
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cmbBoxStyle.SelectedItem;
            string style = selectedItem.Content.ToString(); 
            var uri = new Uri($"/Styles/{style}.xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            switch (style)
            {
                case "Dark":
                    personalAccountView.BackgroundGrid.Background = viewModel.SetdarkBackground();
                    break;
                case "White":
                    personalAccountView.BackgroundGrid.Background = viewModel.SetWhiteBackground();
                    break;
                case "Original":
                    personalAccountView.BackgroundGrid.Background = viewModel.SetWhiteBackground();
                    break;
            }
        }

        private void SetBackgroundGrid()
        {
            personalAccountView.BackgroundGrid.Background = viewModel.SetBackground();
        }
        private void btn_loadImage_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox_image.IsChecked == true)
            {
                personalAccountView.BackgroundGrid.Background = viewModel.LoadBackground();
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (personalAccountView.BackgroundGrid.Background == null)
                personalAccountView.BackgroundGrid.Background = viewModel.SetBackground();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            personalAccountView.BackgroundGrid.Background = null;

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void btn_maximize_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindowState();
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                SwitchWindowState();
                return;
            }
            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                return;
            }
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed) Window.GetWindow(this).DragMove();
            }
        }


        private void MaximizeWindow()
        {
            Window.GetWindow(this).WindowState = WindowState.Maximized;
        }

        private void RestoreWindow()
        {
            Window.GetWindow(this).WindowState = WindowState.Normal;
        }

        private void SwitchWindowState()
        {
            if (Window.GetWindow(this).WindowState == WindowState.Normal) MaximizeWindow();
            else RestoreWindow();
        }

        private void MinimizeAndDragMove(MouseButtonEventArgs e)
        {
            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                double percentHorizontal = e.GetPosition(this).X / ActualWidth;
                double targetHorizontal = Window.GetWindow(this).RestoreBounds.Width * percentHorizontal;

                double percentVertical = e.GetPosition(this).Y / ActualHeight;
                double targetVertical = Window.GetWindow(this).RestoreBounds.Height * percentVertical;

                Window.GetWindow(this).WindowStyle = WindowStyle.None;
                RestoreWindow();

                var mousePosition = e.GetPosition(this);

                Window.GetWindow(this).Left = mousePosition.X - targetHorizontal;
                Window.GetWindow(this).Top = mousePosition.Y - targetVertical;
            }
            if (e.LeftButton == MouseButtonState.Pressed) Window.GetWindow(this).DragMove();
            Window.GetWindow(this).WindowStyle = WindowStyle.SingleBorderWindow;
        }

        public void WindowStateChanged(WindowState state)
        {
            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                btn_maximize.Content = "\uE923";
                titleBar.Height = 24;
            }
            else if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                btn_maximize.Content = "\uE922";
                titleBar.Height = 32;
            }
        }

        private void TabItem_LogOut_click(object sender, MouseButtonEventArgs e)
        {
            LoginView loginView = LoginView.loginWindow;
            loginView.Show();
            Application.Current.Shutdown();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceDictionary resourceDictionary = Application.Current.Resources;
            string value = cmbBoxStyle.Text;
            switch (value)
            {
                case "Original":
                    SwapStyleToOriginal(resourceDictionary);
                    break;
                case "Dark":
                    SwapStyleToDark(resourceDictionary);
                    break;
                case "White":
                    SwapStyleToWhite(resourceDictionary);
                    break;
                default:
                    string message = "Произошла ошибка.";
                    MessageBoxViewModel messageBox = new MessageBoxViewModel();
                    messageBox.ShowMessageBox(message);
                    break;
            }
        }

        private void SwapStyleToOriginal(ResourceDictionary resourceDictionary)
        {
            resourceDictionary["ColorViolet"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#462AD8"));
            resourceDictionary["ColorPink"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DA34AE"));
            resourceDictionary["ColorMagenta"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A16C1"));
            resourceDictionary["ColorLightPink"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#deabf5"));
            resourceDictionary["ColorPinkMouseOver"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ba50eb"));
            resourceDictionary["ColorText"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ba50eb"));
            resourceDictionary["ColorLogInBorder"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BC2CD8")) { Opacity = 0.4 };
        }

        private void SwapStyleToDark(ResourceDictionary resourceDictionary)
        {
            resourceDictionary["ColorViolet"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#171A8C"));
            resourceDictionary["ColorPink"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8E116B"));
            resourceDictionary["ColorMagenta"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#58077D"));
            resourceDictionary["ColorLightPink"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#80389F"));
            resourceDictionary["ColorPinkMouseOver"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#701A99"));
            resourceDictionary["ColorText"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#701A99"));
            resourceDictionary["ColorLogInBorder"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#760E8C")) { Opacity = 0.4 };
        }

        private void SwapStyleToWhite(ResourceDictionary resourceDictionary)
        {
            resourceDictionary["ColorViolet"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9496EC"));
            resourceDictionary["ColorPink"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ED89D1"));
            resourceDictionary["ColorMagenta"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BD73E0"));
            resourceDictionary["ColorLightPink"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDD0FA"));
            resourceDictionary["ColorPinkMouseOver"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D99CF5"));
            resourceDictionary["ColorText"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D99CF5"));
            resourceDictionary["ColorLogInBorder"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6FD2E")) { Opacity = 0.4 };
        }
    }
}
