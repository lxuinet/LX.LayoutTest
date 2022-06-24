using LX;
namespace LayoutTest.LX
{
    internal class ImageTextButton : ImageLabel
    {
        public ImageTextButton(Image image, string text) : base(image, text)
        {
            this.UserMouse = UserMode.On;
            this.Style = ColorStyle.All;
            this.CanSelection = false;
        }
    }
}
