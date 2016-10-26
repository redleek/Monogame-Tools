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

namespace MonogameMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DisplayGrid();
        }

        private void DisplayGrid()
        {
            int size = 15;
            int top = 0;
            int left = 0;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = size; rect.Height = size;
                    rect.Fill = Brushes.Blue;

                    Canvas.SetTop(rect, top);
                    Canvas.SetLeft(rect, left);
                    map.Children.Add(rect);

                    left += (size + 1);
                }

                left = 0;
                top += (size + 1);
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((UIElement)sender);
            HitTestResult result = VisualTreeHelper.HitTest(map, pt);

            if (result != null)
            {
                // Perform action on hit visual object.
                Rectangle rect = (Rectangle)result.VisualHit;
                rect.Fill = Brushes.Green;
            }
        }
    }
}