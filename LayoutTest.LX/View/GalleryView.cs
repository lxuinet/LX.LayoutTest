using System;
using System.IO;

using LX;

namespace LayoutTest.LX
{
    internal class GalleryView : Control
    {
        public GalleryView()
        {
            this.AutoHeight = true;
            this.Color = Color.Background;
            this.Padding = 16;
            this.Layout = new GalleryLayout() { Spacing = 16 };

            for (int i = 0; i < 40; i++)
            {
                foreach (var fileName in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "data", "*.png"))
                {
                    var cardView = new CardView() { Source = new DataItem(fileName) };
                    cardView.AddTo(this);
                }
            }
        }
    }
}
