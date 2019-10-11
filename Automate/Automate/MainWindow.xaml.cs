using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Automate
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int state = 0;
        bool chk = false;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!chk)
            {
                string text = (sender as TextBox).Text;
                if (text.Length > 0)
                {
                    char last = text.Last();
                    switch (state)
                    {
                        case 0:
                            if (last == '_' || Regex.IsMatch(last.ToString(), "[a-zA-z]"))
                            {
                                state = 1;
                                State.Content = "Состояние: 1";
                            }
                            else if (last != ' ')
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 1:
                            if (last == ' ')
                            {
                                state = 2;
                                State.Content = "Состояние: 2";
                            }
                            else if (last == '=')
                            {
                                state = 3;
                                State.Content = "Состояние: 3";
                            }
                            else if (!Regex.IsMatch(last.ToString(), "[a-zA-Z0-9]") && last != '_')
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 2:
                            if (last == '=')
                            {
                                state = 3;
                                State.Content = "Состояние: 3";
                            }
                            else if (last != ' ')
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 3:
                            if (last == ' ')
                            {
                                state = 4;
                                State.Content = "Состояние: 4";
                            }
                            else if (last == '(')
                            {
                                state = 5;
                                State.Content = "Состояние: 5";
                            }
                            else
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 4:
                            if (last == '(')
                            {
                                state = 5;
                                State.Content = "Состояние: 5";
                            }
                            else if (last != ' ')
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 5:
                            if (last == '_' || Regex.IsMatch(last.ToString(), "[a-zA-z]"))
                            {
                                state = 6;
                                State.Content = "Состояние: 6";
                            }
                            else if (last != ' ')
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 6:
                            if (last == ' ')
                            {
                                state = 7;
                                State.Content = "Состояние: 7";
                            }
                            else if (last == ',')
                            {
                                state = 5;
                                State.Content = "Состояние: 5";
                            }
                            else if (last == ')')
                            {
                                state = 8;
                                State.Content = "Состояние: 8";
                            }
                            else if (!Regex.IsMatch(last.ToString(), "[a-zA-Z0-9]") && last != '_')
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 7:
                            if (last == ',')
                            {
                                state = 5;
                                State.Content = "Состояние: 5";
                            }
                            else if (last == ')')
                            {
                                state = 8;
                                State.Content = "Состояние: 8";
                            }
                            else if (last != ' ')
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 8:
                            if (last == ';')
                            {
                                state = 9;
                                State.Content = "Состояние: 9 конец ввода";
                            }
                            else
                            {
                                text = text.Remove(text.Length - 1, 1);
                                SystemSounds.Beep.Play();
                            }
                            break;
                        case 9:
                            text = text.Remove(text.Length - 1, 1);
                            SystemSounds.Beep.Play();
                            break;
                    }
                    chk = true;
                    (sender as TextBox).Text = text;
                    (sender as TextBox).CaretIndex = text.Length;
                    chk = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainText.Text = "";
            state = 0;
            State.Content = "Состояние: 0";
        }

        private void MainText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
            }
        }
    }
}
