using SFML.Graphics;
using SFML.System;

namespace Chess.UI
{
    class Graphic : UIElement
    {
        private Sprite _sprite;

        public Graphic(Texture texture)
        {
            this._sprite = new Sprite(texture);
        }
        public void SetTexture(Texture newTexture)
        {
            _sprite.Texture = newTexture;
        }
        public void SetPosition(Vector2f position)
        {
            _sprite.Position = position;
        }
        public override void Draw()
        {
            Application.Instance().MainWindow.Draw(_sprite);
        }
    }
}