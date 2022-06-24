using System.Linq;

using LX;

namespace LayoutTest.LX
{
    internal class MainForm : Control
    {
        public MainForm()
        {
            this.AddToRoot(Alignment.Fill);
            this.Color = Color.Background;
            this.Layout = new VerticalList();

            var mainView = new GalleryView();
            mainView.AddTo(this, Alignment.Fill);

            var toolsView = new ToolsView();
            toolsView.AddTo(this, Alignment.TopFill);

            toolsView.Filter.OnTextChanged += delegate
            {
                var text = toolsView.Filter.Text.ToLower();
                foreach (CardView cardView in mainView.Controls.OfType<CardView>())
                {
                    cardView.Visible = text == "" || cardView.Text.Text.ToLower().Contains(text) || cardView.Description.Text.Contains(text);
                }
            };

        }
    }
}
