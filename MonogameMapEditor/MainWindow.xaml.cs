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
using MonoGame_Tools.MapTools;

namespace MonogameMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Layer> layerListItems;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Put default values into form items
        /// </summary>
        private void InitialiseItems()
        {
            // Add default layer item to layer list.
            layerListItems = new List<Layer>();
            layerListItems.Add(new Layer() { Name = "Layer 1" });
            layerListView.ItemsSource = layerListItems;
        }

        #region Event Handlers
        private void map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TransformTile(sender, e);
        }

        private void map_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        private void TransformTile(object sender, MouseButtonEventArgs e)
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

        // TODO: improve formatting and scale it to window
        /*
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
        */
    }
}