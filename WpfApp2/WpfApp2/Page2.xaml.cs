using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChooseMenu.Visibility = Visibility.Hidden;
            Information.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ChooseMenu.Visibility = Visibility.Hidden;
            ButtonList.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            ChooseMenu.Visibility = Visibility.Hidden;
            Language.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            Information.Visibility = Visibility.Hidden;
            ChooseMenu.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            ButtonList.Visibility = Visibility.Hidden;
            ChooseMenu.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            Language.Visibility = Visibility.Hidden;
            ChooseMenu.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e)
        {
            Information.Visibility = Visibility.Hidden;
            ButtonList.Visibility = Visibility.Visible;
        }
    }
}
