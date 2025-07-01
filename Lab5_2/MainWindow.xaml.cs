using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArchimedeanTree
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawTree(); 
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            DrawTree();
        }

        private void DrawTree()
        {
            MyCanvas.Children.Clear();

            try
            {
                double A = double.Parse(ABox.Text);
                double B = double.Parse(BBox.Text);
                int K = int.Parse(KBox.Text);     

                Point root = new Point(MyCanvas.ActualWidth / 2, MyCanvas.ActualHeight);
                DrawBranch(root, -90, A, B, K); 
            }
            catch
            {
                MessageBox.Show("Введіть коректні значення (A, B, K).");
            }
        }

        private void DrawBranch(Point start, double angle, double height, double width, int level)
        {
            if (level == 0)
                return;

            double rad = angle * Math.PI / 180;
            Point end = new Point(
                start.X + height * Math.Cos(rad),
                start.Y + height * Math.Sin(rad)
            );

            RectangleGeometry rect = new RectangleGeometry(new Rect(end.X - width / 2, end.Y, width, height));
            RotateTransform rotate = new RotateTransform(angle, end.X, end.Y);
            Path path = new Path
            {
                Data = rect,
                Fill = Brushes.SaddleBrown,
                RenderTransform = rotate
            };
            MyCanvas.Children.Add(path);

            double scale = 0.7;

            double newHeight = height * scale;
            double newWidth = width * scale;

            double delta = 30;

            DrawBranch(end, angle - delta, newHeight, newWidth, level - 1);

            DrawBranch(end, angle + delta, newHeight, newWidth, level - 1);
        }
    }
}
