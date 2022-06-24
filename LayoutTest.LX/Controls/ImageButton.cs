using LX;

namespace LayoutTest.LX
{
    internal class ImageButton : PictureBox
    {
        public ImageButton(Image image)
        {
            this.Shape = CornerShape.Oval;
            this.Radius = 16;
            this.UserMouse = UserMode.On;
            this.Style = ColorStyle.All;
            this.Image = image;
            this.Size = 32;
            this.ImagePadding = 4;
            this.ImageAlignment = Alignment.Zoom;
            this.ImageColor = Color.Content;
        }
    }
}
