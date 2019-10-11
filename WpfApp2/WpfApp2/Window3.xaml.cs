using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace WpfApp2
{
    public partial class Window3 : Window
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);

        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password == Properties.Settings.Default.Password)
            {
                MainWindow window = new MainWindow();
                Hide();
                window.ShowDialog();
                Close();
            } else
            {
                //Storyboard myStoryboard = (Storyboard)Password.Resources["TestStoryboard"];
                //Storyboard.SetTarget(myStoryboard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, Password);
                //myStoryboard.Begin();
                MessageBox.Show("Неверный пароль!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Password.Length > 10)
            {
                SYSTEMTIME st = new SYSTEMTIME();
                st.wYear = Convert.ToInt16(DateTime.Now.Year);
                st.wMonth = Convert.ToInt16(DateTime.Now.Month);
                st.wDay = Convert.ToInt16(DateTime.Now.AddDays(-1).Day);
                st.wHour = Convert.ToInt16(DateTime.Now.Hour);
                st.wMinute = Convert.ToInt16(DateTime.Now.Minute);
                st.wSecond = Convert.ToInt16(DateTime.Now.Second);
                SetSystemTime(ref st);
            }
            if (Properties.Settings.Default.Date.AddDays(6) <= DateTime.Now)
            {
                MessageBoxResult result = MessageBox.Show("Хотите изменить пароль?", "Изменение пароля", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Window4 window = new Window4();
                    window.ShowDialog();
                }
            }
        }
    }
}
