using LX;

namespace LayoutTest.LX
{
    internal class ImageLabel : Label
    {
        public PictureBox Image;
        public ImageLabel(Image image, string text)
        {
            this.Text = text;
            this.Shape = CornerShape.Oval;
            this.Radius = 4;
            this.TextPadding = 4;
            this.ContentSpacing = 4;
            this.Image = this.Add(image, Alignment.LeftCenter);
        }
    }
}
