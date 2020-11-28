using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageUtilities.Tests
{
    public class Shared
    {
        public static void PrepareZoomImage(out Border border, out ScaleTransform scaleTranform, out TranslateTransform translateTransform)
        {
            border = new Border
            {
                ClipToBounds = true
            };

            Image image = new Image
            {
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            TransformGroup group = new TransformGroup();
            group.Children.Add(new ScaleTransform());
            group.Children.Add(new TranslateTransform());
            image.RenderTransform = group;

            border.Child = image;

            scaleTranform = (ScaleTransform)((TransformGroup)image.RenderTransform).Children.First(tr => tr is ScaleTransform);
            translateTransform = (TranslateTransform)((TransformGroup)image.RenderTransform).Children.First(tr => tr is TranslateTransform);
        }
    }
}
