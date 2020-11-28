using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xunit;

namespace ImageUtilities.Tests
{
    public class PanHandlerTests
    {
        [WpfFact]
        public void Pan_By100()
        {
            Shared.PrepareZoomImage(out Border border, out ScaleTransform scaleTranform, out TranslateTransform translateTransform);

            PanHandler panHandler = new PanHandler(border);

            panHandler.StartPan(new Point(0, 0));
            panHandler.Pan(new Point(100, 100));
            panHandler.EndPan();

            Assert.Equal(1, scaleTranform.ScaleX);
            Assert.Equal(1, scaleTranform.ScaleY);
            Assert.Equal(100, translateTransform.X);
            Assert.Equal(100, translateTransform.Y);

        }
    }
}
