using System;
using System.Windows;

namespace AirportSystem
{
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OldPass.Password == Properties.Settings.Default.AdminPassword)
            {
                if (NewPassText.Password.Length < 6)
                {
                    if (NewPassText.Password == RetryPassText.Password)
                    {
                        Properties.Settings.Default.AdminPassword = NewPassText.Password;
                        Properties.Settings.Default.AdminDate = DateTime.Today;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Пароль изменен!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                } else
                {
                    MessageBox.Show("Пароль должен быть не менее 6-ти символов!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else
            {
                MessageBox.Show("Старый пароль введен неверно!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
