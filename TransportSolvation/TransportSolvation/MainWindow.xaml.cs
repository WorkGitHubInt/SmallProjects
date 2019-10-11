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
using System.Diagnostics;
using System.Linq;

namespace TransportSolvation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RowDefinition rowDef;
        ColumnDefinition colDef;
        List<UIElement> elements = new List<UIElement>();
        List<UIElement> elements1 = new List<UIElement>();

        public MainWindow()
        {
            InitializeComponent();
        }

        bool chkLoad = false;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (chkLoad)
            {
                if (Suppliers.Text != "" & Consumers.Text != "")
                {
                    try
                    {
                        if (Convert.ToInt32(Suppliers.Text) < 1 || Convert.ToInt32(Consumers.Text) < 1)
                        {
                            MessageBox.Show("Нельзя поставить значение меньше 1!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            RefreshMatrix(Convert.ToInt32(Suppliers.Text), Convert.ToInt32(Consumers.Text));
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Неверно поставлены числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void RefreshMatrix(int supp, int cons)
        {
            foreach (UIElement element in elements)
            {
                Matrix.Children.Remove(element);
            }
            elements.Clear();
            while (Matrix.RowDefinitions.Count > 2)
            {
                Matrix.RowDefinitions.RemoveAt(1);
            }
            while (Matrix.ColumnDefinitions.Count > 2)
            {
                Matrix.ColumnDefinitions.RemoveAt(1);
            }
            for (int i = 0; i < cons; i++)
            {
                rowDef = new RowDefinition();
                Matrix.RowDefinitions.Insert(Matrix.RowDefinitions.Count - 1, rowDef);
                Label lbl = new Label
                {
                    Content = i + 1,
                    Style = Resources["MatrixLabel"] as Style
                };
                elements.Add(lbl);
                Grid.SetRow(lbl, i + 1);
                Grid.SetColumn(lbl, 0);
                Matrix.Children.Add(lbl);
                for (int j = 0; j < Matrix.ColumnDefinitions.Count + 1; j++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle, Matrix.RowDefinitions.Count - 1);
                    Grid.SetColumn(rectangle, j);
                    Matrix.Children.Add(rectangle);
                    elements.Add(rectangle);
                }
                for (int j = 1; j < Matrix.ColumnDefinitions.Count; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        Style = Resources["Alignments"] as Style,
                        Text = "0"
                    };
                    Grid.SetRow(textBox, Matrix.RowDefinitions.Count - 1);
                    Grid.SetColumn(textBox, j);
                    Matrix.Children.Add(textBox);
                    elements.Add(textBox);
                }
            }
            for (int i = 0; i < supp; i++)
            {
                colDef = new ColumnDefinition();
                Matrix.ColumnDefinitions.Insert(Matrix.ColumnDefinitions.Count - 1, colDef);
                Label lbl = new Label
                {
                    Content = i + 1,
                    Style = Resources["MatrixLabel"] as Style
                };
                elements.Add(lbl);
                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, i + 1);
                Matrix.Children.Add(lbl);
                for (int j = 0; j < Matrix.RowDefinitions.Count + 1; j++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle, j);
                    Grid.SetColumn(rectangle, Matrix.ColumnDefinitions.Count - 1);
                    Matrix.Children.Add(rectangle);
                    elements.Add(rectangle);
                }
                for (int j = 1; j < Matrix.RowDefinitions.Count; j++)
                {
                    if (j != Matrix.RowDefinitions.Count - 1 || i != supp - 1)
                    {
                        TextBox textBox = new TextBox
                        {
                            Style = Resources["Alignments"] as Style,
                            Text = "0"
                        };
                        Grid.SetRow(textBox, j);
                        Grid.SetColumn(textBox, Matrix.ColumnDefinitions.Count - 1);
                        Matrix.Children.Add(textBox);
                        elements.Add(textBox);
                    }
                }
            }
            Grid.SetRow(ConsLabel, 0);
            Grid.SetColumn(ConsLabel, Matrix.ColumnDefinitions.Count - 1);
            Grid.SetRow(SuppLabel, Matrix.RowDefinitions.Count - 1);
            Grid.SetColumn(SuppLabel, 0);
            Debug.WriteLine(Matrix.Children.OfType<TextBox>().Count());
        }

        private void Init()
        {
            int[,] transportTariffes = new int[Convert.ToInt32(Consumers.Text), Convert.ToInt32(Suppliers.Text)];
            int[] consumeValues = new int[Convert.ToInt32(Consumers.Text)];
            int[] supplieValues = new int[Convert.ToInt32(Suppliers.Text)];
            for (int i = 0; i < Convert.ToInt32(Consumers.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(Suppliers.Text); j++)
                {
                    transportTariffes[i, j] = Convert.ToInt32(Matrix.Children.OfType<TextBox>().First(e => Grid.GetColumn(e) == j + 1 && Grid.GetRow(e) == i + 1).Text);
                }
            }
            for (int i = 0; i < Convert.ToInt32(Consumers.Text); i++)
            {
                consumeValues[i] = Convert.ToInt32(Matrix.Children.OfType<TextBox>().First(e => Grid.GetColumn(e) == Convert.ToInt32(Suppliers.Text) + 1 && Grid.GetRow(e) == i + 1).Text);
            }
            for (int i = 0; i < Convert.ToInt32(Suppliers.Text); i++)
            {
                supplieValues[i] = Convert.ToInt32(Matrix.Children.OfType<TextBox>().First(e => Grid.GetColumn(e) == i + 1 && Grid.GetRow(e) == Convert.ToInt32(Consumers.Text) + 1).Text);
            }
            int sumSupplie = 0;
            int sumConsume = 0;
            foreach (int value in supplieValues)
            {
                sumSupplie += value;
            }
            foreach (int value in consumeValues)
            {
                sumConsume += value;
            }
            if (sumSupplie != sumConsume)
            {
                MessageBoxResult result = MessageBox.Show("Задача не сбалансированна! Вы хотите продолжить?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Solve(transportTariffes, supplieValues, consumeValues);
                }
            }
            else
            {
                Solve(transportTariffes, supplieValues, consumeValues);
            }
        }

        private void Solve(int[,] transportTariffes, int[] supplieValues, int[] consumeValues)
        {
            int[] supplieValuesCopy = new int[supplieValues.Length];
            for (int i = 0; i < supplieValues.Length; i++)
            {
                supplieValuesCopy[i] = supplieValues[i];
            }
            int[] consumeValuesCopy = new int[consumeValues.Length];
            for (int i = 0; i < consumeValues.Length; i++)
            {
                consumeValuesCopy[i] = consumeValues[i];
            }
            int[,] arr = new int[transportTariffes.GetLength(0), transportTariffes.GetLength(1)];
            for (int i = 0; i < consumeValues.Length; i++)
            {
                int lowerRange = Int32.MinValue;
                while (consumeValues[i] > 0)
                {
                    int min = Int32.MaxValue;
                    int minCol = 0;
                    for (int j = 0; j < supplieValues.Length; j++)
                    {
                        if (transportTariffes[i, j] == 0 && supplieValues[j] != 0)
                        {
                            minCol = j;
                        }
                        else if (transportTariffes[i, j] < min && transportTariffes[i, j] > lowerRange && supplieValues[j] != 0)
                        {
                            min = transportTariffes[i, j];
                            minCol = j;
                        }
                    }
                    if (supplieValues[minCol] < consumeValues[i])
                    {
                        arr[i, minCol] = supplieValues[minCol];
                        consumeValues[i] -= supplieValues[minCol];
                        supplieValues[minCol] = 0;
                    }
                    else if (supplieValues[minCol] > consumeValues[i])
                    {
                        arr[i, minCol] = consumeValues[i];
                        supplieValues[minCol] -= consumeValues[i];
                        consumeValues[i] = 0;
                    }
                    else
                    {
                        arr[i, minCol] = consumeValues[i];
                        consumeValues[i] = 0;
                        supplieValues[minCol] = 0;
                    }
                    lowerRange = transportTariffes[i, minCol];
                }
            }
            int TT = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    TT += arr[i, j] * transportTariffes[i, j];
                }
            }
            int[] vPotential = new int[consumeValues.Length];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                vPotential[i] = Int32.MinValue;
            }
            int[] uPotential = new int[supplieValues.Length];
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                uPotential[j] = Int32.MinValue;
            }
            int count = 0;
            int maxRow = 0;
            int maxValue = Int32.MinValue;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] > 0)
                    {
                        count++;
                    }
                }
                if (count > maxValue)
                {
                    maxValue = count;
                    maxRow = i;
                }
                count = 0;
            }
            int maxCol = 0;
            int maxValue1 = 0;
            count = 0;
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    if (arr[i, j] > 0)
                    {
                        count++;
                    }
                }
                if (count > maxValue1)
                {
                    maxValue1 = count;
                    maxCol = j;
                }
                count = 0;
            }
            if (maxValue > maxValue1)
            {
                vPotential[maxRow] = 0;
                for (int i = 0; i < arr.GetLength(1); i++)
                {
                    if (arr[maxRow, i] > 0)
                    {
                        uPotential[i] = transportTariffes[maxRow, i] - vPotential[maxRow];
                    }
                }
            }
            else
            {
                uPotential[maxCol] = 0;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    if (arr[i, maxCol] > 0)
                    {
                        vPotential[i] = transportTariffes[i, maxCol] - uPotential[maxCol];
                    }
                }
            }
            bool chk1 = true;
            do
            {
                chk1 = true;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (arr[i, j] > 0)
                        {
                            if (vPotential[i] == Int32.MinValue && uPotential[j] > Int32.MinValue)
                            {
                                vPotential[i] = transportTariffes[i, j] - uPotential[j];
                            }
                            else if (vPotential[i] > Int32.MinValue && uPotential[j] == Int32.MinValue)
                            {
                                uPotential[j] = transportTariffes[i, j] - vPotential[i];
                            }
                        }
                    }
                }
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    if (vPotential[i] == Int32.MinValue)
                    {
                        chk1 = false;
                    }
                }
                for (int i = 0; i < arr.GetLength(1); i++)
                {
                    if (uPotential[i] == Int32.MinValue)
                    {
                        chk1 = false;
                    }
                }
            } while (!chk1);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] != 0)
                    {
                        if (uPotential[j] == 0)
                        {
                            uPotential[j] = transportTariffes[i, j] - vPotential[i];
                        }
                        else if (vPotential[i] == 0)
                        {
                            vPotential[i] = transportTariffes[i, j] - uPotential[j];
                        }
                    }
                }
            }
            int[,] freeCells = new int[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 0)
                    {
                        freeCells[i, j] = transportTariffes[i, j] - (vPotential[i] + uPotential[j]);
                    }
                }
            }
            bool chk = true;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 0)
                    {
                        if (freeCells[i, j] < 0)
                        {
                            chk = false;
                        }
                    }
                }
            }
            while (!chk)
            {
                Cell minCell = new Cell();
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (arr[i, j] == 0)
                        {
                            if (freeCells[i, j] < minCell.rate)
                            {
                                minCell.i = i;
                                minCell.j = j;
                                minCell.rate = freeCells[i, j];
                            }
                        }
                    }
                }
                Cell maxRowCell = new Cell();
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[minCell.i, j] > 0)
                    {
                        if (transportTariffes[minCell.i, j] > maxRowCell.tariff)
                        {
                            maxRowCell.tariff = transportTariffes[minCell.i, j];
                            maxRowCell.value = arr[minCell.i, j];
                            maxRowCell.i = minCell.i;
                            maxRowCell.j = j;
                        }
                    }
                }
                Cell maxColCell = new Cell();
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    if (arr[i, minCell.j] > 0)
                    {
                        if (transportTariffes[i, minCell.j] > maxColCell.tariff)
                        {
                            maxColCell.tariff = transportTariffes[i, minCell.j];
                            maxColCell.value = arr[i, minCell.j];
                            maxColCell.i = i;
                            maxColCell.j = minCell.j;
                        }
                    }
                }
                Cell endCell = new Cell();
                endCell.i = maxColCell.i;
                endCell.j = maxRowCell.j;
                int value = 0;
                if (maxColCell.value > maxRowCell.value)
                {
                    value = maxRowCell.value;
                } else
                {
                    value = maxColCell.value;
                }
                arr[minCell.i, minCell.j] += value;
                arr[endCell.i, endCell.j] += value;
                arr[maxRowCell.i, maxRowCell.j] -= value;
                arr[maxColCell.i, maxColCell.j] -= value;
                vPotential = new int[consumeValues.Length];
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    vPotential[i] = Int32.MinValue;
                }
                uPotential = new int[supplieValues.Length];
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    uPotential[j] = Int32.MinValue;
                }
                count = 0;
                maxRow = 0;
                maxValue = Int32.MinValue;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (arr[i, j] > 0)
                        {
                            count++;
                        }
                    }
                    if (count > maxValue)
                    {
                        maxValue = count;
                        maxRow = i;
                    }
                    count = 0;
                }
                maxCol = 0;
                maxValue1 = 0;
                count = 0;
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        if (arr[i, j] > 0)
                        {
                            count++;
                        }
                    }
                    if (count > maxValue1)
                    {
                        maxValue1 = count;
                        maxCol = j;
                    }
                    count = 0;
                }
                if (maxValue > maxValue1)
                {
                    vPotential[maxRow] = 0;
                    for (int i = 0; i < arr.GetLength(1); i++)
                    {
                        if (arr[maxRow, i] > 0)
                        {
                            uPotential[i] = transportTariffes[maxRow, i] - vPotential[maxRow];
                        }
                    }
                }
                else
                {
                    uPotential[maxCol] = 0;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        if (arr[i, maxCol] > 0)
                        {
                            vPotential[i] = transportTariffes[i, maxCol] - uPotential[maxCol];
                        }
                    }
                }
                chk1 = true;
                do
                {
                    chk1 = true;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            if (arr[i, j] > 0)
                            {
                                if (vPotential[i] == Int32.MinValue && uPotential[j] > Int32.MinValue)
                                {
                                    vPotential[i] = transportTariffes[i, j] - uPotential[j];
                                }
                                else if (vPotential[i] > Int32.MinValue && uPotential[j] == Int32.MinValue)
                                {
                                    uPotential[j] = transportTariffes[i, j] - vPotential[i];
                                }
                            }
                        }
                    }
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        if (vPotential[i] == Int32.MinValue)
                        {
                            chk1 = false;
                        }
                    }
                    for (int i = 0; i < arr.GetLength(1); i++)
                    {
                        if (uPotential[i] == Int32.MinValue)
                        {
                            chk1 = false;
                        }
                    }
                } while (!chk1);
                freeCells = new int[arr.GetLength(0), arr.GetLength(1)];
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (arr[i, j] == 0)
                        {
                            freeCells[i, j] = transportTariffes[i, j] - (vPotential[i] + uPotential[j]);
                        }
                    }
                }
                chk = true;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (arr[i, j] == 0)
                        {
                            if (freeCells[i, j] < 0)
                            {
                                chk = false;
                            }
                        }
                    }
                }
            }
            TT = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    TT += arr[i, j] * transportTariffes[i, j];
                }
            }
            foreach (UIElement element in elements1)
            {
                EndMatrix.Children.Remove(element);
            }
            elements1.Clear();
            while (EndMatrix.RowDefinitions.Count > 2)
            {
                EndMatrix.RowDefinitions.RemoveAt(1);
            }
            while (EndMatrix.ColumnDefinitions.Count > 2)
            {
                EndMatrix.ColumnDefinitions.RemoveAt(1);
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                rowDef = new RowDefinition();
                EndMatrix.RowDefinitions.Insert(EndMatrix.RowDefinitions.Count - 1, rowDef);
                Label lbl = new Label
                {
                    Content = i + 1,
                    Style = Resources["MatrixLabel"] as Style
                };
                elements1.Add(lbl);
                Grid.SetRow(lbl, i + 1);
                Grid.SetColumn(lbl, 0);
                EndMatrix.Children.Add(lbl);
                for (int j = 0; j < EndMatrix.ColumnDefinitions.Count + 1; j++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle, EndMatrix.RowDefinitions.Count - 1);
                    Grid.SetColumn(rectangle, j);
                    EndMatrix.Children.Add(rectangle);
                    elements1.Add(rectangle);
                }
                for (int j = 1; j < EndMatrix.ColumnDefinitions.Count - 1; j++)
                {
                    RowDefinition rowDef1 = new RowDefinition();
                    ColumnDefinition colDef1 = new ColumnDefinition();
                    RowDefinition rowDef2 = new RowDefinition();
                    ColumnDefinition colDef2 = new ColumnDefinition();
                    Grid grid = new Grid();
                    grid.RowDefinitions.Add(rowDef1);
                    grid.ColumnDefinitions.Add(colDef1);
                    grid.RowDefinitions.Add(rowDef2);
                    grid.ColumnDefinitions.Add(colDef2);
                    Label lblNum = new Label
                    {
                        FontSize = 14,
                        Content = arr[i, j - 1],
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(lblNum, 0);
                    Grid.SetColumn(lblNum, 0);
                    Grid.SetColumnSpan(lblNum, 2);
                    Label lblTarrif = new Label
                    {
                        FontSize = 12,
                        Content = transportTariffes[i, j - 1],
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(lblTarrif, 1);
                    Grid.SetColumn(lblTarrif, 1);
                    Label lblFreeCell = new Label
                    {
                        FontSize = 12,
                        Content = freeCells[i, j - 1],
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(lblFreeCell, 1);
                    Grid.SetColumn(lblFreeCell, 0);
                    Rectangle rectangle = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle, 1);
                    Grid.SetColumn(rectangle, 0);
                    Rectangle rectangle1 = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle1, 1);
                    Grid.SetColumn(rectangle1, 1);
                    grid.Children.Add(lblNum);
                    grid.Children.Add(lblTarrif);
                    grid.Children.Add(lblFreeCell);
                    grid.Children.Add(rectangle);
                    grid.Children.Add(rectangle1);
                    Grid.SetRow(grid, EndMatrix.RowDefinitions.Count - 1);
                    Grid.SetColumn(grid, j);
                    EndMatrix.Children.Add(grid);
                    elements1.Add(grid);
                }
            }
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                colDef = new ColumnDefinition();
                EndMatrix.ColumnDefinitions.Insert(EndMatrix.ColumnDefinitions.Count - 1, colDef);
                Label lbl = new Label
                {
                    Content = i + 1,
                    Style = Resources["MatrixLabel"] as Style
                };
                elements1.Add(lbl);
                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, i + 1);
                EndMatrix.Children.Add(lbl);
                for (int j = 0; j < EndMatrix.RowDefinitions.Count + 1; j++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle, j);
                    Grid.SetColumn(rectangle, EndMatrix.ColumnDefinitions.Count - 1);
                    EndMatrix.Children.Add(rectangle);
                    elements1.Add(rectangle);
                }
                for (int j = 1; j < EndMatrix.RowDefinitions.Count - 1; j++)
                {
                    RowDefinition rowDef1 = new RowDefinition();
                    ColumnDefinition colDef1 = new ColumnDefinition();
                    RowDefinition rowDef2 = new RowDefinition();
                    ColumnDefinition colDef2 = new ColumnDefinition();
                    Grid grid = new Grid();
                    grid.RowDefinitions.Add(rowDef1);
                    grid.ColumnDefinitions.Add(colDef1);
                    grid.RowDefinitions.Add(rowDef2);
                    grid.ColumnDefinitions.Add(colDef2);
                    Label lblNum = new Label
                    {
                        FontSize = 14,
                        Content = arr[j - 1, i],
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(lblNum, 0);
                    Grid.SetColumn(lblNum, 0);
                    Grid.SetColumnSpan(lblNum, 2);
                    Label lblTarrif = new Label
                    {
                        FontSize = 12,
                        Content = transportTariffes[j - 1, i],
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(lblTarrif, 1);
                    Grid.SetColumn(lblTarrif, 1);
                    Label lblFreeCell = new Label
                    {
                        FontSize = 12,
                        Content = freeCells[j - 1, i],
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(lblFreeCell, 1);
                    Grid.SetColumn(lblFreeCell, 0);
                    Rectangle rectangle = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle, 1);
                    Grid.SetColumn(rectangle, 0);
                    Rectangle rectangle1 = new Rectangle
                    {
                        Style = Resources["Rectangle"] as Style
                    };
                    Grid.SetRow(rectangle1, 1);
                    Grid.SetColumn(rectangle1, 1);
                    grid.Children.Add(lblNum);
                    grid.Children.Add(lblTarrif);
                    grid.Children.Add(lblFreeCell);
                    grid.Children.Add(rectangle);
                    grid.Children.Add(rectangle1);
                    Grid.SetRow(grid, j);
                    Grid.SetColumn(grid, i + 1);
                    EndMatrix.Children.Add(grid);
                    elements1.Add(grid);
                }
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Label lbl = new Label
                {
                    Content = consumeValuesCopy[i],
                    Style = Resources["MatrixLabel"] as Style
                };
                Grid.SetRow(lbl, i + 1);
                Grid.SetColumn(lbl, EndMatrix.ColumnDefinitions.Count - 1);
                EndMatrix.Children.Add(lbl);
                elements1.Add(lbl);
            }
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                Label lbl = new Label
                {
                    Content = supplieValuesCopy[i],
                    Style = Resources["MatrixLabel"] as Style
                };
                Grid.SetRow(lbl, EndMatrix.RowDefinitions.Count - 1);
                Grid.SetColumn(lbl, i + 1);
                EndMatrix.Children.Add(lbl);
                elements1.Add(lbl);
            }
            Grid.SetRow(ConsLabel1, 0);
            Grid.SetColumn(ConsLabel1, EndMatrix.ColumnDefinitions.Count - 1);
            Grid.SetRow(SuppLabel1, EndMatrix.RowDefinitions.Count - 1);
            Grid.SetColumn(SuppLabel1, 0);
            colDef = new ColumnDefinition();
            EndMatrix.ColumnDefinitions.Add(colDef);
            Label vLabel = new Label
            {
                Content = "V",
                Style = Resources["MatrixLabel"] as Style
            };
            Grid.SetRow(vLabel, 0);
            Grid.SetColumn(vLabel, EndMatrix.ColumnDefinitions.Count - 1);
            Rectangle vRect = new Rectangle
            {
                Style = Resources["Rectangle"] as Style
            };
            Grid.SetRow(vRect, 0);
            Grid.SetColumn(vRect, EndMatrix.ColumnDefinitions.Count - 1);
            EndMatrix.Children.Add(vRect);
            elements1.Add(vRect);
            EndMatrix.Children.Add(vLabel);
            elements1.Add(vLabel);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Label lbl = new Label
                {
                    Content = vPotential[i],
                    Style = Resources["MatrixLabel"] as Style
                };
                Grid.SetRow(lbl, i + 1);
                Grid.SetColumn(lbl, EndMatrix.ColumnDefinitions.Count - 1);
                EndMatrix.Children.Add(lbl);
                elements1.Add(lbl);
                Rectangle Rect = new Rectangle
                {
                    Style = Resources["Rectangle"] as Style
                };
                Grid.SetRow(Rect, i + 1);
                Grid.SetColumn(Rect, EndMatrix.ColumnDefinitions.Count - 1);
                EndMatrix.Children.Add(Rect);
                elements1.Add(Rect);
            }
            rowDef = new RowDefinition();
            EndMatrix.RowDefinitions.Add(rowDef);
            Label uLabel = new Label
            {
                Content = "μ",
                Style = Resources["MatrixLabel"] as Style
            };
            Grid.SetRow(uLabel, EndMatrix.RowDefinitions.Count - 1);
            Grid.SetColumn(uLabel, 0);
            Rectangle uRect = new Rectangle
            {
                Style = Resources["Rectangle"] as Style
            };
            Grid.SetRow(uRect, EndMatrix.RowDefinitions.Count - 1);
            Grid.SetColumn(uRect, 0);
            EndMatrix.Children.Add(uRect);
            elements1.Add(uRect);
            EndMatrix.Children.Add(uLabel);
            elements1.Add(uLabel);
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                Label lbl = new Label
                {
                    Content = uPotential[i],
                    Style = Resources["MatrixLabel"] as Style
                };
                Grid.SetRow(lbl, EndMatrix.RowDefinitions.Count - 1);
                Grid.SetColumn(lbl, i + 1);
                EndMatrix.Children.Add(lbl);
                elements1.Add(lbl);
                Rectangle Rect = new Rectangle
                {
                    Style = Resources["Rectangle"] as Style
                };
                Grid.SetRow(Rect, EndMatrix.RowDefinitions.Count - 1);
                Grid.SetColumn(Rect, i + 1);
                EndMatrix.Children.Add(Rect);
                elements1.Add(Rect);
            }
            Summ.Content = "Транспортные расходы: " + TT.ToString();
            EndMatrix.Visibility = Visibility.Visible;
            EndMatrixLabel.Visibility = Visibility.Visible;
            Summ.Visibility = Visibility.Visible;
            this.Height = 650;
            this.Width = 750;
            int a = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            chkLoad = true;
            RefreshMatrix(1, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }

        public class Cell
        {
            public int i = 0, j = 0, value = Int32.MaxValue, rate = Int32.MaxValue, tariff = Int32.MinValue;
        }
    }
}
