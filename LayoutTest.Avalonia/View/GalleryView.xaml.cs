using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace LayoutTest.Avalonia.View
{
    /// <summary>
    /// Interaction logic for GalleryView.xaml
    /// </summary>
    public partial class GalleryView : UserControl
    {
        private WrapPanel _wrapPanel;
        public GalleryView()
        {

            var viewer = new ScrollViewer();
            this.Content = viewer;

            _wrapPanel = new WrapPanel();
            _wrapPanel.Orientation = Orientation.Horizontal;

            viewer.Content = _wrapPanel;

            for (int i = 0; i < 40; i++)
            {
                foreach (var fileName in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "data", "*.png"))
                {

                    var dataItem = new DataItem(fileName);

                    var cardView = new Button();
                    cardView.MinWidth = 240;
                    cardView.CornerRadius = new CornerRadius(4);
                    cardView.Margin = new Thickness(8);
                    cardView.VerticalAlignment = VerticalAlignment.Top;
                    cardView.HorizontalAlignment = HorizontalAlignment.Center;
                    cardView.Padding = new Thickness(0);
                    _wrapPanel.Children.Add(cardView);

                    var stackPanel = new StackPanel();
                    cardView.Content = stackPanel;

                    var image = new Image();
                    image.Source = dataItem.Image;
                    image.Stretch = Stretch.Fill;
                    image.HorizontalAlignment = HorizontalAlignment.Stretch;
                    stackPanel.Children.Add(image);

                    var text = new TextBlock();
                    text.HorizontalAlignment = HorizontalAlignment.Left;
                    text.TextWrapping = TextWrapping.WrapWithOverflow;
                    text.FontSize = 20;
                    text.Text = dataItem.Name;
                    stackPanel.Children.Add(text);

                    var description = new TextBlock();
                    description.HorizontalAlignment = HorizontalAlignment.Left;
                    description.TextWrapping = TextWrapping.WrapWithOverflow;
                    description.FontSize = 12;
                    description.Text = dataItem.Description;
                    stackPanel.Children.Add(description);


                    var dockPanel = new DockPanel();
                    stackPanel.Children.Add(dockPanel);

                    if (dataItem.HasLogo)
                    {
                        var image1 = new Image();
                        image1.Source = dataItem.Image;
                        image1.Stretch = Stretch.Fill;
                        image1.Width = 100;
                        image1.Height = 100;
                        image1.HorizontalAlignment = HorizontalAlignment.Stretch;
                        dockPanel.Children.Add(image1);
                    }

                    if (dataItem.IsLive)
                    {
                        var button1 = new Button();
                        button1.Content = "Text1";
                        dockPanel.Children.Add(button1);
                    }

                    var button2 = new Button();
                    button2.Content = "Text2";
                    button2.HorizontalAlignment = HorizontalAlignment.Right;
                    dockPanel.Children.Add(button2);

                    var button3 = new Button();
                    button3.Content = "Text3";
                    button3.HorizontalAlignment = HorizontalAlignment.Right;
                    dockPanel.Children.Add(button3);
                }
            }


            this.PropertyChanged += GalleryView_PropertyChanged;
        }

        private void GalleryView_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Contains("Bounds"))
            {
                var parentSpacing = 16;
                var parentWidth = _wrapPanel.Bounds.Width;

                foreach (var button in _wrapPanel.Children.OfType<Button>())
                {
                    button.Width = Math.Floor(parentWidth / Math.Min(Math.Max(Math.Floor(parentWidth / 360), 1), 6)) - parentSpacing;
                }
            }
        }

    }
}
