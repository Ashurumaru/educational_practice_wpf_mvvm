﻿using educational_practice.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using educational_practice.Models;

namespace educational_practice.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public static LoginView loginWindow;
        private readonly LoginViewModel viewModel;

        public LoginView()
        {
            InitializeComponent();
            loginWindow = this;
            viewModel = new LoginViewModel();
            DataContext = viewModel;
            SetBackgroundGrid();
        }

        private void SetBackgroundGrid()
        {
            loginWindow.BackgroundGrid.Background = viewModel.SetBackground();
        }
        private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            viewModel.Password = passwordBox.Password;
        }

        private void PasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            viewModel.FirstPasswordForSignUp = passwordBox.Password;
        }

        private void PasswordBox3_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            viewModel.SecondPasswordForSignUp = passwordBox.Password;
        }
        
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void ChevronLeft_Click(object sender, MouseButtonEventArgs e)
        {
                viewModel.SwapVisibility();
        }
    }
}
