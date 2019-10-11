using System;
using System.Windows;
using static System.Windows.Rect;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Rectangles
{
    public class OpaqueClickableImage : Image
    {
        protected override HitTestResult HitTestCore(
    PointHitTestParameters hitTestParameters)
        {
            try
            {
                // Get value of current pixel
                var source = (BitmapSource)Source;
                var x = (int)(hitTestParameters.HitPoint.X /
                    ActualWidth * source.PixelWidth);
                var y = (int)(hitTestParameters.HitPoint.Y /
                    ActualHeight * source.PixelHeight);
                var pixels = new byte[4];
                source.CopyPixels(new Int32Rect(x, y, 1, 1), pixels, 4, 0);
                // Check alpha channel
                if (pixels[3] < 10)
                {
                    return null;
                }
                else
                {
                    return new PointHitTestResult(this, hitTestParameters.HitPoint);
                }
            } catch
            {
                return null;
            }
        }

        protected override GeometryHitTestResult HitTestCore(
            GeometryHitTestParameters hitTestParameters)
        {
            // Do something similar here, possibly checking every pixel within
            // the hitTestParameters.HitGeometry.Bounds rectangle
            return base.HitTestCore(hitTestParameters);
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Rectangle rect;
        Rectangle rect1;

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            if (rect != null)
            {
                MainCanvas.Children.Remove(rect);
            }
            if (rect1 != null)
            {
                MainCanvas.Children.Remove(rect1);
            }
            try
            {
                if (Convert.ToDouble(TopLeftX.Text.Trim()) > 0 && Convert.ToDouble(TopLeftY.Text.Trim()) > 0 && Convert.ToDouble(TopLeftX1.Text.Trim()) > 0 && Convert.ToDouble(TopLeftY1.Text.Trim()) > 0 && Convert.ToDouble(BottomRightX.Text.Trim()) > 0 && Convert.ToDouble(BottomRightX1.Text.Trim()) > 0 && Convert.ToDouble(BottomRightY.Text.Trim()) > 0 && Convert.ToDouble(BottomRightY1.Text.Trim()) > 0 && Convert.ToDouble(TopLeftX.Text.Trim()) <= 399 && Convert.ToDouble(TopLeftY.Text.Trim()) <= 299 && Convert.ToDouble(TopLeftX1.Text.Trim()) <= 399 && Convert.ToDouble(TopLeftY1.Text.Trim()) <= 299 && Convert.ToDouble(BottomRightX.Text.Trim()) <= 399 && Convert.ToDouble(BottomRightY.Text.Trim()) <= 299 && Convert.ToDouble(BottomRightX1.Text.Trim()) <= 399 && Convert.ToDouble(BottomRightY1.Text.Trim()) <= 299)
                {
                    Rect rectA = new Rect
                    {
                        Location = new Point(Convert.ToDouble(TopLeftX.Text.Trim()), Convert.ToDouble(TopLeftY.Text.Trim())),
                        Size = new Size(Convert.ToDouble(BottomRightX.Text.Trim()) - Convert.ToDouble(TopLeftX.Text.Trim()), Convert.ToDouble(BottomRightY.Text.Trim()) - Convert.ToDouble(TopLeftY.Text.Trim()))
                    };
                    Rect rectB = new Rect
                    {
                        Location = new Point(Convert.ToDouble(TopLeftX1.Text.Trim()), Convert.ToDouble(TopLeftY1.Text.Trim())),
                        Size = new Size(Convert.ToDouble(BottomRightX1.Text.Trim()) - Convert.ToDouble(TopLeftX1.Text.Trim()), Convert.ToDouble(BottomRightY1.Text.Trim()) - Convert.ToDouble(TopLeftY1.Text.Trim()))
                    };
                    rect = new Rectangle
                    {
                        Margin = new Thickness(rectA.TopLeft.X, rectA.TopLeft.Y, 0, 0),
                        Width = rectA.BottomRight.X - rectA.TopLeft.X,
                        Height = rectA.BottomRight.Y - rectA.TopLeft.Y,
                    };
                    rect1 = new Rectangle
                    {
                        Margin = new Thickness(rectB.TopLeft.X, rectB.TopLeft.Y, 0, 0),
                        Width = rectB.BottomRight.X - rectB.TopLeft.X,
                        Height = rectB.BottomRight.Y - rectB.TopLeft.Y
                    };
                    double length = 0;
                    if (rectA.IntersectsWith(rectB))
                    {
                        Rect inRect = Intersect(rectA, rectB);
                        if (inRect.Width == 0)
                        {
                            length = inRect.Height;
                        }
                        else if (inRect.Height == 0)
                        {
                            length = inRect.Width;
                        }
                    }
                    Length.Content = "Длина соприкосновения: " + length;
                    rect.Fill = new SolidColorBrush(Colors.Blue);
                    rect1.Fill = new SolidColorBrush(Colors.Red);
                    MainCanvas.Children.Add(rect);
                    MainCanvas.Children.Add(rect1);
                } else
                {
                    MessageBox.Show("Введенные значения должны быть больше 0! Максимально разрешенные координаты X=399 и Y=299.");
                }
            } catch
            {
                MessageBox.Show("Что-то пошло не так! Проверьте введеные значения!");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text.Trim());
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Клик!");
        }
    }
}
