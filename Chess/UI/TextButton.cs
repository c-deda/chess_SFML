using SFML.Graphics;
using SFML.System;

namespace Chess.UI
{
    class TextButton : Button
    {
        private Text _text;

        public TextButton(Vector2f position, Vector2f size, Color idleColor, Color hoverColor) : base(position, size, idleColor, hoverColor) 
        { 
            this._text = new Text();
        }
        public void SetText(Font font, string text, uint characterSize, Color textColor)
        {
            this._text = new Text(text, font, characterSize);
            this._text.Origin = new Vector2f(this._text.GetGlobalBounds().Left, this._text.GetGlobalBounds().Top);
            this._text.FillColor = Color.Black;

            this._text.Position = new Vector2f(
                this._shape.Position.X + (this._shape.Size.X / 2.0f) - (this._text.GetGlobalBounds().Width / 2.0f),
                this._shape.Position.Y + (this._shape.Size.Y / 2.0f) - (this._text.GetGlobalBounds().Height / 2.0f)
            );
        }
        public override void Enable()
        {
            _isEnabled = true;
            _idleColor = new Color(_idleColor.R, _idleColor.B, _idleColor.G, 255);
            _shape.FillColor = _idleColor;
            _text.FillColor = new Color(_text.FillColor.R, _text.FillColor.B, _text.FillColor.B, 255);
        }
        public override void Disable()
        {
            _isEnabled = false;
            _idleColor = new Color(_idleColor.R, _idleColor.B, _idleColor.G, 100);
            _shape.FillColor = _idleColor;
            _text.FillColor = new Color(_text.FillColor.R, _text.FillColor.B, _text.FillColor.B, 100);
        }
        public override void Draw()
        {
            Application.Instance().MainWindow.Draw(_shape);
            Application.Instance().MainWindow.Draw(_text);
        }
    }
}