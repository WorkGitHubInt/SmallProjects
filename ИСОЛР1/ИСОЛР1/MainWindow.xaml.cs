using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ИСОЛР1
{
    public partial class MainWindow : Window
    {
        List<TextBox> VarList = new List<TextBox>();

        public MainWindow()
        {
            InitializeComponent();
            AddVar();
            AddLimit();
        }

        #region Vars
        private void AddVarBtn_Click(object sender, RoutedEventArgs e)
        {
            AddVar();
            RefreshLimits();
        }

        private void AddVar()
        {
            TextBox txtBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 18,
                Width = 30,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 4, 0, 0),

            };
            txtBox.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);
            Label label = new Label
            {
                Content = "x" + VarList.Count(),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 26,
                Width = 22,
                Margin = new Thickness(0, 0, 0, 0)
            };
            VarBox.Children.Add(txtBox);
            VarBox.Children.Add(label);
            VarList.Add(txtBox);
            //<TextBox HorizontalAlignment="Left" Height="18" Margin="0,4,0,0" TextWrapping="Wrap" Text="-4" VerticalAlignment="Top" Width="20"
            //<Label Content="x1" HorizontalAlignment="Left" Margin="-9,0,0,0" VerticalAlignment="Top" Height="26" Width="21"/>
        }

        private void RefreshFunction()
        {
            string function = "P=";
            int i = 0;
            foreach (TextBox txt in VarList)
            {
                if (i == 0)
                {
                    function += txt.Text + "x" + i;
                }
                else
                {
                    if (txt.Text.Contains("-"))
                    {
                        function += txt.Text + "x" + i;
                    }
                    else
                    {
                        function += "+" + txt.Text + "x" + i;
                    }
                }
                i++;
            }
            function += "->";
            Function.Content = function;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshFunction();
        }

        private void DeleteVarBtn_Click(object sender, RoutedEventArgs e)
        {
            VarBox.Children.RemoveAt(VarBox.Children.Count - 1);
            VarBox.Children.RemoveAt(VarBox.Children.Count - 1);
            VarList.RemoveAt(VarList.Count - 1);
            RefreshLimits();
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MaxMin.Text.Contains("max"))
            {
                MaxMin.Text = "min";
            }
            else
            {
                MaxMin.Text = "max";
            }
        }
        #endregion

        #region Limits
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddLimit();
        }

        private void AddLimit()
        {
            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            int i = 0;
            foreach (TextBox text in VarList)
            {
                TextBox txt = new TextBox
                {
                    Height = 18,
                    Width = 30,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(4, 4, 0, 0)
                };
                panel.Children.Add(txt);
                Label label = new Label
                {
                    Content = "x" + i,
                    Height = 26,
                    Width = 22
                };
                panel.Children.Add(label);
                i++;
            }
            TextBlock textBlock = new TextBlock
            {
                Text = "<=",
                Margin = new Thickness(0, 5, 0, 0)
            };
            textBlock.PreviewMouseDown += TextBlock_PreviewMouseDown_1;
            panel.Children.Add(textBlock);
            TextBox txt1 = new TextBox
            {
                Height = 18,
                Margin = new Thickness(4, 4, 0, 0),
                TextWrapping = TextWrapping.Wrap,
                Width = 20
            };
            panel.Children.Add(txt1);
            LimitBox.Children.Add(panel);
            //<TextBox HorizontalAlignment="Left" Height="18" Margin="4,4,0,0" TextWrapping="Wrap" Text="-4" VerticalAlignment="Top" Width="20"/>
            //        <Label Content="x1" HorizontalAlignment="Left" Margin="0,0,0,0" VerticalAlignment="Top" Height="26" Width="21"/>
            //        <TextBlock Text="&lt;=" Margin="0,5,0,0"></TextBlock>
            //        <TextBox HorizontalAlignment="Left" Height="18" Margin="4,4,0,0" TextWrapping="Wrap" Text="-4" VerticalAlignment="Top" Width="20"/>
            //LimitBox.Children.Add(txt);
            //BoxList.Add(txt);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            LimitBox.Children.RemoveAt(LimitBox.Children.Count - 1);
        }

        private void TextBlock_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if ((sender as TextBlock).Text.Contains("<"))
            {
                (sender as TextBlock).Text = ">=";
            }
            else if ((sender as TextBlock).Text.Contains(">"))
            {
                (sender as TextBlock).Text = "=";
            }
            else
            {
                (sender as TextBlock).Text = "<=";
            }
        }

        private void RefreshLimits()
        {
            LimitBox.Children.Clear();
            AddLimit();
        }
        #endregion

        private void SolveBtn_Click(object sender, RoutedEventArgs e)
        {
            Init();
            SolveSimp();
        }

        #region Simplex
        private void SolveSimp()
        {
            double[,] matrix = Init();
            int lenght0 = matrix.GetLength(0);
            int lenght1 = matrix.GetLength(1);
            bool chk = false;
            do
            {
                chk = false;
                for (int j = 0; j < lenght1; j++)
                {
                    if (matrix[lenght0 - 1, j] < 0)
                    {
                        chk = true;
                    }
                }
                if (chk)
                {
                    int minCol = 0;
                    double min = matrix[lenght0 - 1, minCol];
                    for (int j = 0; j < lenght1; j++)
                    {
                        if (matrix[lenght0 - 1, j] < min)
                        {
                            min = matrix[lenght0 - 1, j];
                            minCol = j;
                        }
                    }
                    int minRow = 0;
                    double minNum = SafeDivision(matrix[0, lenght1 - 1], matrix[0, minCol]);
                    for (int i = 0; i < lenght0 - 1; i++)
                    {
                        if (SafeDivision(matrix[i, lenght1 - 1], matrix[i, minCol]) < minNum)
                        {
                            minRow = i;
                            minNum = SafeDivision(matrix[i, lenght1 - 1], matrix[i, minCol]);
                        }
                    }
                    double num = matrix[minRow, minCol];
                    for (int i = 0; i < lenght0; i++)
                    {
                        double num1 = matrix[i, minCol];
                        if (i != minRow)
                        {
                            for (int j = 0; j < lenght1; j++)
                            {
                                matrix[i, j] -= (matrix[minRow, j] * num1) / num;
                            }
                        }
                    }
                    for (int j = 0; j < lenght1; j++)
                    {
                        matrix[minRow, j] /= num;
                    }
                }
            } while (chk);
            OutputMatrix(matrix);
            OutputAnswer(matrix);
        }

        private double[,] Init()
        {
            double[,] arr;
            List<double> vars = new List<double>();
            foreach (TextBox txt in VarList)
            {
                if (MaxMin.Text.Contains("min"))
                {
                    vars.Add(Convert.ToInt32(txt.Text));
                } else
                {
                    vars.Add(Convert.ToInt32(txt.Text) * -1);
                }
            }
            List<List<double>> limits = new List<List<double>>();
            int n = 0;
            foreach (StackPanel panel in LimitBox.Children)
            {
                List<double> cortege = new List<double>();
                cortege.Add(0);
                foreach (TextBox txt in panel.Children.OfType<TextBox>())
                {
                    //if (((TextBlock)panel.Children[panel.Children.Count - 2]).Text.Contains(">"))
                    //{
                    //    cortege.Add(Convert.ToInt32(txt.Text) * -1);
                    //} else
                    //{
                        cortege.Add(Convert.ToInt32(txt.Text));
                    //}
                }
                double lastNum = cortege[cortege.Count - 1];
                cortege.RemoveAt(cortege.Count - 1);
                for (int i = 0; i < LimitBox.Children.Count; i++)
                {
                    if (((TextBlock)panel.Children[panel.Children.Count - 2]).Text.Contains("<"))
                    {
                        if (i == n)
                        {
                            cortege.Add(1);
                        }
                        else
                        {
                            cortege.Add(0);
                        }
                    } else if (((TextBlock)panel.Children[panel.Children.Count - 2]).Text.Contains(">"))
                    {
                        if (i == n)
                        {
                            cortege.Add(-1);
                        }
                        else
                        {
                            cortege.Add(0);
                        }
                    }
                }
                cortege.Add(lastNum);
                limits.Add(cortege);
                n++;
            }
            List<double> lastCortege = new List<double>();
            lastCortege.Add(1);
            for (int i = 0; i < limits[0].Count - 1; i++)
            {
                try
                {
                    lastCortege.Add(vars[i]);
                }
                catch
                {
                    lastCortege.Add(0);
                }
            }
            limits.Add(lastCortege);
            arr = new double[limits.Count, limits[0].Count];
            for (int i = 0; i < limits.Count; i++)
            {
                for (int j = 0; j < limits[i].Count; j++)
                {
                    arr[i, j] = limits[i][j];
                }
            }
            return arr;
        }

        private double SafeDivision(double Num, double Denom)
        {
            double Answer;
            try
            {
                Answer = Num / Denom;
                if (Answer < 0)
                {
                    Answer = Double.MaxValue;
                }
            }
            catch
            {
                Answer = Double.MaxValue;
            }
            return Answer;
        }

        private void OutputMatrix(double[,] arr)
        {
            List<List<double>> lsts = new List<List<double>>();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                lsts.Add(new List<double>());
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    lsts[i].Add(arr[i, j]);
                }
            }
            lst.ItemsSource = lsts;
        }

        private void OutputAnswer(double[,] arr)
        {
            Ans.Children.Clear();
            for (int j = 0; j < arr.GetLength(1) - 1; j++)
            {
                int oneCount = 0;
                int numCount = 0;
                int oneRow = 0;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    if (arr[i, j] != 0)
                    {
                        numCount++;
                        if (arr[i, j] == 1)
                        {
                            oneCount++;
                            oneRow = i;
                        }
                    }
                }
                if (oneCount == 1 && numCount == 1)
                {
                    if (j == 0)
                    {
                        Label lbl = new Label
                        {
                            Content = "P=" + arr[oneRow, arr.GetLength(1) - 1]
                        };
                        Ans.Children.Add(lbl);
                    }
                    else
                    {
                        Label lbl = new Label
                        {
                            Content = "x" + j + "=" + arr[oneRow, arr.GetLength(1) - 1]
                        };
                        Ans.Children.Add(lbl);
                    }
                }
                else
                {
                    if (j == 0)
                    {
                        Label lbl = new Label
                        {
                            Content = "P=0"
                        };
                        Ans.Children.Add(lbl);
                    }
                    else
                    {
                        Label lbl = new Label
                        {
                            Content = "x" + j + "=0"
                        };
                        Ans.Children.Add(lbl);
                    }
                }
            }
        }
        #endregion

    }
}
