using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BezierCurveApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawBezierCurve(); 
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            DrawBezierCurve();
        }

        private void DrawBezierCurve()
        {
            MyCanvas.Children.Clear();

            try
            {
                Point P1 = new Point(double.Parse(X1Box.Text), double.Parse(Y1Box.Text));
                Point P2 = new Point(double.Parse(X2Box.Text), double.Parse(Y2Box.Text));
                Point P3 = new Point(double.Parse(X3Box.Text), double.Parse(Y3Box.Text));
                Point P4 = new Point(double.Parse(X4Box.Text), double.Parse(Y4Box.Text));

                DrawLine(P1, P2, Brushes.LightGray);
                DrawLine(P2, P3, Brushes.LightGray);
                DrawLine(P3, P4, Brushes.LightGray);

                Polyline bezierLine = new Polyline
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2
                };

                const int segments = 100;
                for (int i = 0; i <= segments; i++)
                {
                    double t = i / (double)segments;
                    Point B = BezierPoint(P1, P2, P3, P4, t);
                    bezierLine.Points.Add(B);
                }

                MyCanvas.Children.Add(bezierLine);

                DrawPoint(P1, Brushes.Red);
                DrawPoint(P2, Brushes.Green);
                DrawPoint(P3, Brushes.Green);
                DrawPoint(P4, Brushes.Red);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні числові значення для координат.", "Помилка");
            }
        }

        private Point BezierPoint(Point p0, Point p1, Point p2, Point p3, double t)
        {
            double x = Math.Pow(1 - t, 3) * p0.X +
                       3 * Math.Pow(1 - t, 2) * t * p1.X +
                       3 * (1 - t) * t * t * p2.X +
                       Math.Pow(t, 3) * p3.X;

            double y = Math.Pow(1 - t, 3) * p0.Y +
                       3 * Math.Pow(1 - t, 2) * t * p1.Y +
                       3 * (1 - t) * t * t * p2.Y +
                       Math.Pow(t, 3) * p3.Y;

            return new Point(x, y);
        }

        private void DrawLine(Point p1, Point p2, Brush color)
        {
            Line line = new Line
            {
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                Stroke = color,
                StrokeThickness = 1
            };
            MyCanvas.Children.Add(line);
        }

        private void DrawPoint(Point p, Brush color)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 6,
                Height = 6,
                Fill = color
            };
            Canvas.SetLeft(ellipse, p.X - 3);
            Canvas.SetTop(ellipse, p.Y - 3);
            MyCanvas.Children.Add(ellipse);
        }
    }
}
