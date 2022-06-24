namespace LayoutTest.LX
{
    internal partial class CardView
    {
        private DataItem _source = null;
        public DataItem Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                _image.Image = _source?.Image;
                Text.Text = _source?.Name;
                Description.Text = _source?.Description;
                _logo.Image = _source?.Image;
                _logo.Visible = _source != null && _source.HasLogo;
                _live.Visible = _source != null && _source.IsLive;
            }
        }
    }
}
