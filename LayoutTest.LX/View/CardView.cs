using System;

using LX;

namespace LayoutTest.LX
{
    internal partial class CardView : Control
    {
        private Image _emptyImage = null;
        private Image _liveImage = Image.LoadIcon(24, 276);
        private Image _rate1Image = Image.LoadIcon(20, 296);
        private Image _rate2Image = Image.LoadIcon(20, 295);

        public Label Text;
        public Label Description;

        private PictureBox _image;
        private PictureBox _logo;
        private ImageLabel _live;

        public CardView()
        {
            this.Layout = new VerticalList();
            this.AutoHeight = true;
            this.UserMouse = UserMode.On;
            this.CanFocus = true;
            this.Color = Color.Surface;
            this.Style = ColorStyle.All | ColorStyle.HoveredIfChildHoverd;
            this.Shape = CornerShape.Oval;
            this.Radius = 4;
            this.Ranges.MinimumWidth = 240;

            _image = this.Add(_emptyImage, Alignment.TopFill);
            _image.ImageAlignment = Alignment.Fill;
            _image.OnSizeChanged += delegate 
            { 
                _image.Height = _image.Width * _image.Image.Height / _image.Image.Width; 
            };

            var hPanel = new Control();
            hPanel.Padding = 8;
            hPanel.AutoHeight = true;
            hPanel.Layout = new HorizontalList(8);
            hPanel.AddTo(this, Alignment.TopFill);

            _logo = hPanel.Add(_emptyImage, Alignment.TopLeft);
            _logo.Top = 16;
            _logo.AutoSize = false;
            _logo.Scale = 3;
            _logo.Size = 48;
            _logo.Shape = CornerShape.Oval;
            _logo.Radius = 24;
            _logo.UserHorizontalScroll = UserMode.None;
            _logo.UserVerticalScroll = UserMode.None;
            _logo.Visible = false;

            var vPanel = new Control();
            vPanel.AutoHeight = true;
            vPanel.Layout = new VerticalList();
            vPanel.AddTo(hPanel, Alignment.TopFill);

            Text = vPanel.Add("", Alignment.TopFill);
            Text.MultiLine = true;
            Text.WordWrap = true;
            Text.Visible = false;
            Text.Font = Font.Subtitle;
            Text.OnTextChanged += delegate 
            { 
                Text.Visible = Text.Text != ""; 
            };

            Description = vPanel.Add("", Alignment.TopFill);
            Description.MultiLine = true;
            Description.WordWrap = true;
            Description.Font = Font.Caption;
            Description.TextColor = Color.Content.Auto(75);
            Description.Visible = false;
            Description.OnTextChanged += delegate 
            { 
                Description.Visible = Description.Text != ""; 
            };

            var tools = new Control();
            tools.Padding = 8;
            tools.AutoHeight = true;
            tools.HorizontalScrollBar.Height = 2;
            tools.Layout = new HorizontalList(8);
            tools.AddTo(vPanel, Alignment.TopFill);

            _live = new ImageLabel(_liveImage, "LIVE");
            _live.Visible = false;
            _live.Color = Color.Selected;
            _live.AddTo(tools, Alignment.LeftCenter);

            var rate1 = new ImageTextButton(_rate1Image, "");
            rate1.AddTo(tools, Alignment.RightCenter);
            rate1.OnClick += delegate
            {
                rate1.Text = (Convert.ToInt32(rate1.Text != "" ? rate1.Text : "0") + 1).ToString();
            };

            var rate2 = new ImageTextButton(_rate2Image, "");
            rate2.AddTo(tools, Alignment.RightCenter);
            rate2.OnClick += delegate
            {
                rate2.Text = (Convert.ToInt32(rate2.Text != "" ? rate2.Text : "0") + 1).ToString();
            };
        }

        protected override void DoFocusedChanged()
        {
            base.DoFocusedChanged();

            var timer = _live.GetTimer("RotateTimer");
            if (!Focused && timer != null)
            {
                timer.Stop();
            }
            if (Focused && timer == null && _live.Visible)
            {
                timer = _live.StartTimer("RotateTimer");
                timer.Tick += (object sender, Timer e) =>
                {
                    e.Interval = this.IsShown ? 17 : 1000;
                    _live.Image.Rotation += timer.ElapsedMilliseconds * 360 / 1000;
                };
            }
        }

    }
}
