using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xunit;

namespace ImageUtilities.Tests
{
    public class ZoomHandlerTests
    {
        [WpfFact]
        public void Zoom_By2()
        {
            Shared.PrepareZoomImage(out Border border, out ScaleTransform scaleTranform, out TranslateTransform translateTransform);

            ZoomHandler zoomHandler = new ZoomHandler(border);

            zoomHandler.Zoom(2);

            Assert.Equal(2, scaleTranform.ScaleX);
            Assert.Equal(2, scaleTranform.ScaleY);
            Assert.Equal(0, translateTransform.X);
            Assert.Equal(0, translateTransform.Y);
        }
    }
}
