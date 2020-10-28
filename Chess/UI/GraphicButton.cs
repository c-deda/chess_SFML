using SFML.Graphics;
using SFML.System;

namespace Chess.UI
{
    class GraphicButton : Button
    {
        private Sprite _sprite;
        private Texture _hoverTexture;
        private Texture _idleTexture;

        public GraphicButton(Vector2f position, Vector2f size, Color idleColor, Color hoverColor) : base(position, size, idleColor, hoverColor)
        {
            _sprite = new Sprite();
            _sprite.Position = this._shape.Position;
        }
        public void SetGraphics(Texture idleTexture, Texture hoverTexture)
        {
            _idleTexture = idleTexture;
            _hoverTexture = hoverTexture;
        }
        public override void OnHover(float x, float y)
        {
            if (_isEnabled)
            {
                _shape.FillColor = _hoverColor;
                _sprite.Texture = _hoverTexture;
            }
        }
        public override void OnIdle(float x, float y)
        {
            _shape.FillColor = _idleColor;
            _sprite.Texture = _idleTexture;
        }
        public override void Enable()
        {
            _isEnabled = true;
            _idleColor = new Color(_idleColor.R, _idleColor.B, _idleColor.G, 255);
            _shape.FillColor = _idleColor;
            _sprite.Color = new Color(255, 255, 255, 255);
        }
        public override void Disable()
        {
            _isEnabled = false;
            _idleColor = new Color(_idleColor.R, _idleColor.B, _idleColor.G, 100);
            _shape.FillColor = _idleColor;
            _sprite.Color = new Color(255, 255, 255, 100);
        }
        public override void Draw()
        {
            Application.Instance().MainWindow.Draw(_shape);
            Application.Instance().MainWindow.Draw(_sprite);
        }
    }
}