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
        public ScaleTransform ScaleTranform { get; private set; }
        public TranslateTransform TranslateTransform { get; private set; }
        double ScaleX
        {
            get { return ScaleTranform.ScaleX; }
            set
            {
                if (value >= 1)
                {
                    ScaleTranform.ScaleX = value;
                }
                else
                {
                    ResetZoom();
                }
            }
        }
        private double ScaleY
        {
            get { return ScaleTranform.ScaleY; }
            set
            {
                if (value >= 1)
                {
                    ScaleTranform.ScaleY = value;
                }
                else
                {
                    ResetZoom();
                }
            }
        }
        private double TranslateX
        {
            get { return TranslateTransform.X; }
            set
            {
                double MaxTranslation = (ZBorder.ActualWidth + ZImage.ActualWidth * ScaleX) / 2 - 50;
                if (Math.Abs(value) < MaxTranslation)
                {
                    TranslateTransform.X = value;
                }
                else
                {
                    TranslateTransform.X = Math.Sign(value) * MaxTranslation;
                }
            }
        }
        private double TranslateY
        {
            get { return TranslateTransform.Y; }
            set
            {
                double MaxTranslation = (ZBorder.ActualHeight + ZImage.ActualHeight * ScaleY) / 2 - 50;
                if (Math.Abs(value) < MaxTranslation)
                {
                    TranslateTransform.Y = value;
                }
                else
                {
                    TranslateTransform.Y = Math.Sign(value) * MaxTranslation;
                }
            }
        }
        public Point InitialMousePosition { get; private set; }
        public TranslateTransform InitialImagePosition { get; private set; }

        public ZoomImage()
        {
            InitializeComponent();

            TransformGroup group = new TransformGroup();
            group.Children.Add(new ScaleTransform());
            group.Children.Add(new TranslateTransform());
            ZImage.RenderTransform = group;

            ScaleTranform = (ScaleTransform)((TransformGroup)ZImage.RenderTransform)
                .Children.First(tr => tr is ScaleTransform);

            TranslateTransform = (TranslateTransform)((TransformGroup)ZImage.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);

            ZImage.MouseDown += ZImage_MouseDown;
            ZImage.MouseUp += ZImage_MouseUp;
            ZImage.MouseMove += ZImage_MouseMove;
        }

        private void ZImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (ZImage.IsMouseCaptured)
            {
                Vector v = InitialMousePosition - e.GetPosition(ZBorder);
                TranslateX = InitialImagePosition.X - v.X;
                TranslateY = InitialImagePosition.Y - v.Y;
            }
        }

        private void ZImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ZImage.ReleaseMouseCapture();
        }

        private void ZImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ZImage.CaptureMouse();

            InitialMousePosition = e.GetPosition(ZBorder);

            InitialImagePosition = new TranslateTransform(TranslateX, TranslateY);
        }

        public void Zoom(double zoomCoefficient)
        {
            ScaleX *= zoomCoefficient;
            ScaleY *= zoomCoefficient;
            TranslateX *= zoomCoefficient;
            TranslateY *= zoomCoefficient;

        }
        private void ResetScaleTransformation()
        {
            ScaleX = 1;
            ScaleY = 1;
        }
        private void ResetTranslateTransformation()
        {
            TranslateX = 0;
            TranslateY = 0;
        }
        public void ResetZoom()
        {
            ResetScaleTransformation();
            ResetTranslateTransformation();
        }
        public void LoadImage(string imagePath)
        {
            ZImage.Source = new BitmapImage(new Uri(imagePath));
        }


    }
}
