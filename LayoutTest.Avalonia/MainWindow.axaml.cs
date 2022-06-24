using Avalonia.Controls;

using LayoutTest.Avalonia.View;

namespace LayoutTest.Avalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Content = new GalleryView();
        }
    }
}
