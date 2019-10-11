using System;
using System.Windows;

namespace WpfApp2
{
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Password1.Password.Length >= 7)
            {
                Properties.Settings.Default.Password = Password1.Password;
                Properties.Settings.Default.Date = DateTime.Now;
                Properties.Settings.Default.Save();
                Close();
            } else
            {
                MessageBox.Show("Пароль содержать не менее 7-ми символов!", "Изменение пароля", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}
