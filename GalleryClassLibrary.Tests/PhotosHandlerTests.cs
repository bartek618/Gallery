using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GalleryClassLibrary.Tests
{
    public class PhotosHandlerTests
    {
        [Fact]
        public void LoadPhotos_Add5Photos()
        {
            PhotosHandler photosHandler = new PhotosHandler();

            string[] paths = new string[] { "1.jpg", "2.png", "3.txt", "4.exe", "5" };

            photosHandler.LoadPhotos(paths);

            Assert.Equal(2, photosHandler.Photos.Count);

            for (int i = 0; i < photosHandler.Photos.Count; i++)
            {
                Assert.Equal(paths[i], photosHandler.Photos[i].Path);
            }
        }
    }
}
