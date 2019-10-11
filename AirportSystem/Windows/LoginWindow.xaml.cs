using System;
using System.Diagnostics;
using System.Windows;

namespace AirportSystem
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AdminDate = DateTime.Today;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordText.Password == Properties.Settings.Default.AdminPassword)
            {
                MainAdminWindow adminWindow = new MainAdminWindow();
                Hide();
                adminWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный пароль администратора!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            MainUserWindow window = new MainUserWindow();
            Hide();
            window.Show();
            Close();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan difference = Properties.Settings.Default.AdminDate.AddDays(10) - DateTime.Today;
            if (Properties.Settings.Default.AdminDate.AddDays(10) <= DateTime.Today)
            {
                MessageBox.Show("Необходимо сменить пароль администратора!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                PasswordWindow passwordWindow = new PasswordWindow();
                passwordWindow.ShowDialog();
                if (passwordWindow.DialogResult == null || !(bool)passwordWindow.DialogResult)
                {
                    MessageBox.Show("Не был изменен пароль администратора! Повторите попытку", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    passwordWindow.ShowDialog();
                }
            }
            else if (Convert.ToInt32(difference.Days) <= 5)
            {
                MessageBox.Show($"Через {difference.Days} дней необходимо сменить пароль администратора!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Choise.Visibility = Visibility.Collapsed;
            LoginForm.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("help.chm");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new AuthorWindow();
            window.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Choise.Visibility = Visibility.Visible;
            LoginForm.Visibility = Visibility.Collapsed;
        }
    }
}
