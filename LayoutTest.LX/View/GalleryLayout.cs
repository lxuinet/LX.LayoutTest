using System;
using System.Linq;

using LX;

namespace LayoutTest.LX
{
    internal class GalleryLayout : VerticalGallery
    {
        public override void Update(Control control)
        {
            base.Update(control);

            var parentSpacing = this.Spacing;
            var parentWidth = control.Width + parentSpacing - control.PaddingHorizontal;

            foreach (CardView cardView in control.Controls.OfType<CardView>())
            {
                cardView.Width = Math.Floor(parentWidth / Math.Min(Math.Max(Math.Floor(parentWidth / 360), 1), 6)) - parentSpacing;
            }
        }
    }
}
