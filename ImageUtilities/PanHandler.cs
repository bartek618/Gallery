using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageUtilities
{
    class PanHandler
    {
        public ScaleTransform ScaleTranform { get; private set; }
        public TranslateTransform TranslateTransform { get; private set; }
        private Border _border;
        private Image _image;
        public Point InitialMousePosition { get; private set; }
        public TranslateTransform InitialImagePosition { get; private set; }
        public PanHandler(Border border, Image image)
        {
            _border = border;
            _image = image;

            ScaleTranform = (ScaleTransform)((TransformGroup)image.RenderTransform)
    .Children.First(tr => tr is ScaleTransform);

            TranslateTransform = (TranslateTransform)((TransformGroup)image.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
        }
        private double TranslateX
        {
            get { return TranslateTransform.X; }
            set
            {
                double MaxTranslation = (_border.ActualWidth + _image.ActualWidth * ScaleX) / 2 - 50;
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
                double MaxTranslation = (_border.ActualHeight + _image.ActualHeight * ScaleY) / 2 - 50;
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
        private double ScaleX
        {
            get { return ScaleTranform.ScaleX; }
 
        }
        private double ScaleY
        {
            get { return ScaleTranform.ScaleY; }
        }
        public void StartPan(MouseButtonEventArgs e)
        {
            _image.CaptureMouse();

            InitialMousePosition = e.GetPosition(_border);

            InitialImagePosition = new TranslateTransform(TranslateX, TranslateY);
        }
        public void Pan(MouseEventArgs e)
        {
            if (_image.IsMouseCaptured)
            {
                Vector v = InitialMousePosition - e.GetPosition(_border);
                TranslateX = InitialImagePosition.X - v.X;
                TranslateY = InitialImagePosition.Y - v.Y;
            }
        }
        public void EndPan()
        {
            _image.ReleaseMouseCapture();
        }
    }
}
