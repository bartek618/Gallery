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

namespace ImageUtilities
{
    /// <summary>
    /// Interaction logic for ZoomImage.xaml
    /// </summary>
    public partial class ZoomImage : UserControl
    {
        public Point InitialMousePosition { get; private set; }
        public TranslateTransform InitialImagePosition { get; private set; }
        private readonly ZoomHandler _zoomHandler;
        private readonly PanHandler _panHandler;
        public ZoomImage()
        {
            InitializeComponent();

            TransformGroup group = new TransformGroup();
            group.Children.Add(new ScaleTransform());
            group.Children.Add(new TranslateTransform());
            ZImage.RenderTransform = group;

            _zoomHandler = new ZoomHandler(ZBorder, ZImage);
            _panHandler = new PanHandler(ZBorder, ZImage);

            ZImage.MouseDown += ZImage_MouseDown;
            ZImage.MouseUp += ZImage_MouseUp;
            ZImage.MouseMove += ZImage_MouseMove;
        }
        public void LoadImage(string imagePath)
        {
            ZImage.Source = new BitmapImage(new Uri(imagePath));
        }
        public void Zoom(double zoomCoefficient)
        {
            _zoomHandler.Zoom(zoomCoefficient);
        }
        private void ZImage_MouseMove(object sender, MouseEventArgs e)
        {
            _panHandler.Pan(e);
        }
        private void ZImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _panHandler.EndPan();
        }
        private void ZImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _panHandler.StartPan(e);
        }


    }
}
