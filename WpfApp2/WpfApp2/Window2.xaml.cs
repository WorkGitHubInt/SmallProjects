using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp2
{
    public partial class Window2 : Window
    {
        public bool IsRussian;
        public bool IsWidth;
        bool wasActivated = false;
        public Window2()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Window1 window = new Window1();
            if (!IsRussian)
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
            Close();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            if (!wasActivated)
            {
                if (IsWidth)
                {
                    if (IsRussian)
                    {
                        Text1.Inlines.Add("Максимально допустимое значение ширины 480! Подробнее можно ознакомиться в ");
                        TextBlock block = new TextBlock
                        {
                            Text = "справке",
                            TextDecorations = TextDecorations.Underline,
                            Foreground = new SolidColorBrush(Colors.Blue),
                        };
                        block.MouseLeftButtonUp += TextBlock_MouseLeftButtonUp;
                        Text1.Inlines.Add(block);
                        Title = "Ошибка";
                    }
                    else
                    {
                        Text1.Inlines.Add("Maximum available width value is 480! More can be found in ");
                        TextBlock block = new TextBlock
                        {
                            Text = "help",
                            TextDecorations = TextDecorations.Underline,
                            Foreground = new SolidColorBrush(Colors.Blue),
                        };
                        block.MouseLeftButtonUp += TextBlock_MouseLeftButtonUp;
                        Text1.Inlines.Add(block);
                        Title = "Error";
                    }
                }
                else
                {
                    if (IsRussian)
                    {
                        Text1.Inlines.Add("Максимально допустимое значение высоты 380! Подробнее можно ознакомиться в ");
                        TextBlock block = new TextBlock
                        {
                            Text = "справке",
                            TextDecorations = TextDecorations.Underline,
                            Foreground = new SolidColorBrush(Colors.Blue),
                        };
                        block.MouseLeftButtonUp += TextBlock_MouseLeftButtonUp;
                        Text1.Inlines.Add(block);
                        Title = "Ошибка";
                    }
                    else
                    {
                        Text1.Inlines.Add("Maximum available height value is 380! More can be found in ");
                        TextBlock block = new TextBlock
                        {
                            Text = "help",
                            TextDecorations = TextDecorations.Underline,
                            Foreground = new SolidColorBrush(Colors.Blue),
                        };
                        block.MouseLeftButtonUp += TextBlock_MouseLeftButtonUp;
                        Text1.Inlines.Add(block);
                        Title = "Error";
                    }
                }
            }
            wasActivated = true;
        }
    }
}
