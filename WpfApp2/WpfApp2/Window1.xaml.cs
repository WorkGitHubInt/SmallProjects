using System;
using System.Windows;

namespace WpfApp2
{
    public partial class Window1 : Window
    {
        public bool IsRussian;

        public Window1()
        {
            InitializeComponent();
            if (IsRussian)
            {
                MainFrame.Navigate(new Uri("Page1.xaml", UriKind.Relative));
            }
            else
            {
                MainFrame.Navigate(new Uri("Page2.xaml", UriKind.Relative));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsRussian)
            {
                MainFrame.Navigate(new Uri("Page1.xaml", UriKind.Relative));
            }
            else
            {
                MainFrame.Navigate(new Uri("Page2.xaml", UriKind.Relative));
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (IsRussian)
            {
                MainFrame.Navigate(new Uri("Page1.xaml", UriKind.Relative));
            }
            else
            {
                MainFrame.Navigate(new Uri("Page2.xaml", UriKind.Relative));
            }
        }
    }
}
