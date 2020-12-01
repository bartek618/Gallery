using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ImageUtilities
{
    public class ZoomHandler
    {
        private readonly Border _border;
        private readonly Image _image;
        private readonly ScaleTransform _scaleTranform;
        private readonly TranslateTransform _translateTransform;
        private double ScaleX
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

        public ScaleTransform ScaleTranform => _scaleTranform;

        public ZoomHandler(Border border)
        {
            _border = border;
            _image = (Image)border.Child;

            _scaleTranform = (ScaleTransform)((TransformGroup)_image.RenderTransform)
                .Children.First(tr => tr is ScaleTransform);

            _translateTransform = (TranslateTransform)((TransformGroup)_image.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
        }
        public void Zoom(double zoomCoefficient)
        {
            ScaleX *= zoomCoefficient;
            ScaleY *= zoomCoefficient;
            TranslateX *= zoomCoefficient;
            TranslateY *= zoomCoefficient;

        }
        public void ResetZoom()
        {
            ResetScaleTransformation();
            ResetTranslateTransformation();
        }
        private void ResetTranslateTransformation()
        {
            TranslateX = 0;
            TranslateY = 0;
        }
        private void ResetScaleTransformation()
        {
            ScaleX = 1;
            ScaleY = 1;
        }
    }
}
