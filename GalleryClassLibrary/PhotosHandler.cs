﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryClassLibrary
{
    public class PhotosHandler
    {
        public ObservableCollection<Photo> Photos { get; set; } = new ObservableCollection<Photo>();
        public void LoadPhotos(string[] paths)
        {
            Photos.Clear();
            foreach (string path in paths)
            {
                Photos.Add(new Photo { Path = path });
            }
        }
    }
}
