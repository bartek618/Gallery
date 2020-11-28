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
        private Border _border;
        private Image _image;
        public ScaleTransform ScaleTranform { get; private set; }
        public TranslateTransform TranslateTransform { get; private set; }
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
        public ZoomHandler(Border border, Image image)
        {
            _border = border;
            _image = image;

            ScaleTranform = (ScaleTransform)((TransformGroup)image.RenderTransform)
                .Children.First(tr => tr is ScaleTransform);

            TranslateTransform = (TranslateTransform)((TransformGroup)image.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
        }
        public void Zoom(double zoomCoefficient)
        {
            ScaleX *= zoomCoefficient;
            ScaleY *= zoomCoefficient;

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
        private void ResetScaleTransformation()
        {
            ScaleX = 1;
            ScaleY = 1;
        }
    }
}
