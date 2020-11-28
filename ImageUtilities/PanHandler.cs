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
    public class PanHandler
    {
        private readonly Border _border;
        private readonly Image _image;
        public ScaleTransform _scaleTranform;
        public TranslateTransform _translateTransform;
        private double TranslateX
        {
            get { return _translateTransform.X; }
            set
            {
                double MaxTranslation = (_border.ActualWidth + _image.ActualWidth * ScaleX) / 2 - 50;
                if (Math.Abs(value) < MaxTranslation)
                {
                    _translateTransform.X = value;
                }
                else
                {
                    _translateTransform.X = Math.Sign(value) * MaxTranslation;
                }
            }
        }
        private double TranslateY
        {
            get { return _translateTransform.Y; }
            set
            {
                double MaxTranslation = (_border.ActualHeight + _image.ActualHeight * ScaleY) / 2 - 50;
                if (Math.Abs(value) < MaxTranslation)
                {
                    _translateTransform.Y = value;
                }
                else
                {
                    _translateTransform.Y = Math.Sign(value) * MaxTranslation;
                }
            }
        }
        private double ScaleX
        {
            get { return _scaleTranform.ScaleX; }
 
        }
        private double ScaleY
        {
            get { return _scaleTranform.ScaleY; }
        }
        private Point _initialMousePosition;
        private TranslateTransform _initialImagePosition;
        public PanHandler(Border border)
        {
            _border = border;
            _image = (Image)border.Child;

            _scaleTranform = (ScaleTransform)((TransformGroup)_image.RenderTransform)
                .Children.First(tr => tr is ScaleTransform);

            _translateTransform = (TranslateTransform)((TransformGroup)_image.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
        }
        public void StartPan(Point mousePostion)
        {
            _image.CaptureMouse();

            _initialMousePosition = mousePostion;

            _initialImagePosition = new TranslateTransform(TranslateX, TranslateY);
        }
        public void Pan(Point mousePostion)
        {
            if (_image.IsMouseCaptured)
            {
                Vector v = _initialMousePosition - mousePostion;
                TranslateX = _initialImagePosition.X - v.X;
                TranslateY = _initialImagePosition.Y - v.Y;
            }
        }
        public void EndPan()
        {
            _image.ReleaseMouseCapture();
        }
    }
}
