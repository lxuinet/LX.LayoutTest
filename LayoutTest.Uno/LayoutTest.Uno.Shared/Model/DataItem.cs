using System;
using System.Collections.Generic;
using System.IO;

using Windows.UI.Xaml.Media.Imaging;

namespace LayoutTest
{
    public class DataItem
    {
        public string FileName { get; }
        public string Name { get; }
        public BitmapImage Image { get; }

        public string Description { get; }

        public bool HasLogo
        {
            get
            {
                return FileName.Contains("-logo");
            }
        }

        public bool IsLive
        {
            get
            {
                return FileName.Contains("-live");
            }
        }

        private static Dictionary<string, BitmapImage> _images = new Dictionary<string, BitmapImage>();
        public DataItem(string fileName)
        {
            var fileInfo = new FileInfo(fileName);

            BitmapImage image;
            if (!_images.TryGetValue(fileName, out image))
            {
                var asset = "ms-appx:///Assets/Images/test.png";

                #if WINDOWS_UWP
                asset = fileName;
                #endif

                image = new BitmapImage(new Uri(asset));
                _images[fileName] = image;
            }

            FileName = fileName;
            Image = image;
            Name = fileInfo.Name.Replace("-logo", "").Replace("-live", "").Replace(".png", "");
            Description = $"This file description. File has size: {fileInfo.Length} bytes and was created: {fileInfo.CreationTime.ToString()}";
        }
    }
}
