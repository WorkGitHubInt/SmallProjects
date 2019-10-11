using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HeightRect_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void WidthRect_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = true;
            }
        }

        private void WidthRect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainCanvas.Children.Clear();
                if (Convert.ToInt32(WidthRect.Text) > 480 && Convert.ToInt32(WidthRect.Text) > 0)
                {
                    Window2 window1 = new Window2();
                    if ((bool)RussianBtn.IsChecked)
                    {
                        window1.IsRussian = true;
                    } else
                    {
                        window1.IsRussian = false;
                    }
                    window1.IsWidth = true;
                    window1.ShowDialog();
                }
                else
                {
                    if (Convert.ToInt32(HeightRect.Text) > 380 && Convert.ToInt32(HeightRect.Text) > 0)
                    {
                        Window2 window1 = new Window2();
                        if ((bool)RussianBtn.IsChecked)
                        {
                            window1.IsRussian = true;
                        }
                        else
                        {
                            window1.IsRussian = false;
                        }
                        window1.IsWidth = false;
                        window1.ShowDialog();
                    } else
                    {
                        Rectangle rect = new Rectangle
                        {
                            Margin = new Thickness(10, 10, 10, 10),
                            Width = Convert.ToInt32(WidthRect.Text),
                            Height = Convert.ToInt32(HeightRect.Text),
                        };
                        if (Convert.ToInt32(WidthRect.Text) > Convert.ToInt32(HeightRect.Text))
                        {
                            rect.Fill = new SolidColorBrush(Colors.Blue);
                        }
                        else if (Convert.ToInt32(WidthRect.Text) < Convert.ToInt32(HeightRect.Text))
                        {
                            rect.Fill = new SolidColorBrush(Colors.Red);
                        }
                        else
                        {
                            rect.Fill = new SolidColorBrush(Colors.Green);
                        }
                        MainCanvas.Children.Add(rect);
                    }
                }
            } else if (e.Key == Key.O)
            {
                WidthRect.Text = "";
                HeightRect.Text = "";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            if (!(bool)RussianBtn.IsChecked)
            {
                window.IsRussian = false;
                window.Title = "Help";
            }
            else
            {
                window.IsRussian = true;
                window.Title = "Справка";
            }
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            if (!(bool)RussianBtn.IsChecked)
            {
                window.IsRussian = false;
                window.Title = "Help";
            } else
            {
                window.IsRussian = true;
                window.Title = "Справка";
            }
            window.Show();
        }

        private void EnglishBtn_Checked(object sender, RoutedEventArgs e)
        {
            Item.Header = "Help";
            WidthLbl.Content = "Width";
            HeightLbl.Content = "Height";
            LanguageLbl.Content = "Language selection";
            Title = "Rectangle";
        }

        private void RussianBtn_Checked(object sender, RoutedEventArgs e)
        {
            Item.Header = "Справка";
            WidthLbl.Content = "Ширина";
            HeightLbl.Content = "Высота";
            LanguageLbl.Content = "Выбор языка";
            Title = "Прямоугольник";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RussianBtn.IsChecked = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Window1 window = new Window1();
                if (!(bool)RussianBtn.IsChecked)
                {
                    window.IsRussian = false;
                    window.Title = "Help";
                }
                else
                {
                    window.IsRussian = true;
                    window.Title = "Справка";
                }
                window.Show();
            }
        }
    }
}
