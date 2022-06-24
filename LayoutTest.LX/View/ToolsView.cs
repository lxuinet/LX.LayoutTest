using System;

using LX;

namespace LayoutTest.LX
{
    internal class ToolsView : Control
    {
        public TextBox Filter;
        public ToolsView()
        {
            this.Shadow = ShadowStyle.Bottom2;
            this.Color = Color.Primary;
            this.HorizontalScrollBar.Height = 2;
            this.AutoHeight = true;
            this.Padding = 8;
            this.Layout = new VerticalGallery();

            #region theme
            {
                var themePanel = new Control();
                themePanel.Layout = new HorizontalList();
                themePanel.AutoSize = true;
                themePanel.AddTo(this, Alignment.TopLeft);

                themePanel.Add("Change theme:", Alignment.LeftCenter);
                themePanel.Add("Light", Alignment.LeftCenter);

                var skinChanger = new CheckBox();
                skinChanger.View = CheckBoxView.Toggle;
                skinChanger.ContentColor = Color.Secondary;
                skinChanger.ContentStyle = ColorStyle.Normal | ColorStyle.Hovered | ColorStyle.Downed;
                skinChanger.AddTo(themePanel, Alignment.LeftCenter);

                skinChanger.OnCheckedChanged += delegate
                {
                    Window.Skin = skinChanger.Checked ? new BlackSkin() : (Skin)new WhiteSkin();
                };
                skinChanger.Checked = true;

                themePanel.Add("Dark", Alignment.LeftCenter);

                themePanel.Add("   ", Alignment.LeftCenter);
            }
            #endregion

            #region font scale
            {
                var scalePanel = new Control();
                scalePanel.Layout = new HorizontalList();
                scalePanel.AutoSize = true;
                scalePanel.AddTo(this, Alignment.TopLeft);

                scalePanel.Add("Font scale:", Alignment.LeftCenter);
                var scaleSlider = new Slider();
                scaleSlider.ContentSpacing = 0;
                scaleSlider.Width = 150;
                scaleSlider.Maximum = 3;
                scaleSlider.Value = 1;
                scaleSlider.Orientation = SliderOrientation.HorizontalStep;
                scaleSlider.AddTo(scalePanel, Alignment.LeftCenter);
                scaleSlider.OnValueChanged += delegate
                {
                    Window.FontScale = scaleSlider.Value == 0 ? FontScale.Small : scaleSlider.Value == 1 ? FontScale.Normal : scaleSlider.Value == 2 ? FontScale.Large : FontScale.Biggest;
                };

                scalePanel.Add("   ", Alignment.LeftCenter);
            }
            #endregion

            #region zoom
            {
                var zoomPanel = new Control();
                zoomPanel.Layout = new HorizontalList();
                zoomPanel.AutoSize = true;
                zoomPanel.AddTo(this, Alignment.TopLeft);

                zoomPanel.Add("Zoom:", Alignment.LeftCenter);

                var zoomOut = new ImageButton(Image.LoadIcon(24, 343));
                zoomOut.AddTo(zoomPanel, Alignment.LeftCenter);
                zoomOut.OnClick += delegate
                {
                    Window.ZoomOut();
                };

                var zoomIn = new ImageButton(Image.LoadIcon(24, 342));
                zoomIn.AddTo(zoomPanel, Alignment.LeftCenter);
                zoomIn.OnClick += delegate
                {
                    Window.ZoomIn();
                };

                Window.OnScaleChanged += delegate
                {
                    zoomOut.Enabled = Window.Scale != Scale.Percent50;
                    zoomIn.Enabled = Window.Scale != Scale.Percent250;
                };

                zoomPanel.Add("   ", Alignment.LeftCenter);
            }
            #endregion

            #region filter
            {
                var filterPanel = new Control();
                filterPanel.Layout = new HorizontalList();
                filterPanel.AutoSize = true;
                filterPanel.AddTo(this, Alignment.TopLeft);

                var filterText = filterPanel.Add("Filter: ", Alignment.LeftCenter);
                filterText.Color = Color.Parent;
                filterText.BorderSize = 1;
                filterText.UserMouse = UserMode.On;
                filterText.CanSelection = false;
                filterText.Add(Image.LoadIcon(20, 237), Alignment.LeftCenter);

                var clearFilter = new ImageButton(Image.LoadIcon(24, 1243));
                clearFilter.ImagePadding = 2;
                clearFilter.Radius = 12;
                clearFilter.Size = 24;
                clearFilter.Visible = false;
                clearFilter.AddTo(filterText, Alignment.RightCenter);

                Filter = new TextBox();
                Filter.BorderSize = 0;
                Filter.Ranges.MinimumWidth = 0;
                Filter.Ranges.MaximumWidth = 200;
                Filter.AutoWidth = true;
                Filter.AddTo(filterText, Alignment.RightCenter);

                Filter.OnTextChanged += delegate
                {
                    clearFilter.Visible = Filter.Text != "";
                };

                filterText.OnClick += delegate
                {
                    Filter.Focused = true;
                };

                clearFilter.OnClick += delegate
                {
                    Filter.Text = "";
                    Filter.Focused = true;
                };

                filterPanel.Add("   ", Alignment.LeftCenter);
            }
            #endregion

            #region fps
            {
                var fpsPanel = new Control();
                fpsPanel.Layout = new HorizontalList();
                fpsPanel.AutoSize = true;
                fpsPanel.AddTo(this, Alignment.TopLeft);

                var fps = new ImageTextButton(Image.LoadIcon(105), "FPS: 0");
                fps.Image.ImageAlignment = Alignment.Zoom;
                fps.UserMouse = UserMode.None;
                fps.Color = Color.Checked;
                fps.Font = Font.Button;
                fps.AddTo(fpsPanel, Alignment.LeftCenter);

                fpsPanel.Add("   ", Alignment.LeftCenter);

                var fpsCounter = 0;
                var fpsAverage = 0;
                var fpsTime = DateTime.Now;

                OnDraw += (object sender, ControlDrawEventArgs e) =>
                {
                    if (e.State == DrawState.Back)
                    {
                        fpsCounter++;
                        if ((DateTime.Now - fpsTime).TotalSeconds >= 1)
                        {
                            fps.Text = $"FPS: {fpsCounter}";
                            fpsAverage = fpsCounter;
                            fpsCounter = 0;
                            fpsTime = DateTime.Now;
                        }
                    }
                };

                fps.StartTimer("Heart").Tick += (object sender, Timer e) =>
                {
                    var speed = 1.1 - Math.Round(Math.Max(1, Math.Min(500, fpsAverage)) / 500.0, 1);
                    double time = 1000 * speed;
                    var value = e.TotalElapsedMilliseconds - (int)(e.TotalElapsedMilliseconds / time) * time;
                    fps.Image.ImagePadding = value > time * 0.9 ? 0 : value > time * 0.8 ? 2 : value > time * 0.7 ? 0 : 2;
                };
            }
            #endregion
        }
    }
}
