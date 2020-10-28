using Chess.GameLogic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Chess.UI
{
    class SquareUI
    {
        private static Color DarkColorIdle = new Color(100, 50, 0, 175);
        private static Color LightColorIdle = new Color(255, 175, 100, 175);
        private static Color DarkColorHighlight = new Color(225, 225, 0, 175);
        private static Color LightColorHighlight = new Color(250, 250, 100, 175);
        private Sprite _sprite;
        private RectangleShape _shape;
        private Color _idleColor;
        private Color _highlightColor;

        public SquareUI(Position position, Piece piece, bool isDark)
        {
            if (isDark)
            {
                this._idleColor = DarkColorIdle;
                this._highlightColor = DarkColorHighlight;
            }
            else
            {
                this._idleColor = LightColorIdle;
                this._highlightColor = LightColorHighlight;
            }
            
            // Square
            _shape = new RectangleShape(new Vector2f(GlobalConstants.SquareSize, GlobalConstants.SquareSize));
            _shape.FillColor = _idleColor;
            _shape.Position = new Vector2f(GlobalConstants.SquareSize * position.X, GlobalConstants.SquareSize * position.Y);
            // Sprite
            _sprite = Application.Instance().AssetManager.GetPieceSprite(piece);
            _sprite.Position = _shape.Position;
        }
        public void MirrorSquare(Position position)
        {
            _shape.Position = new Vector2f(GlobalConstants.SquareSize * position.X, GlobalConstants.SquareSize * position.Y);
            _sprite.Position = _shape.Position;
        }
        public void Select()
        {
            _shape.FillColor = _highlightColor;
        }
        public void Unselect()
        {
            _shape.FillColor = _idleColor;
        }
        public bool IsClicked(MouseButtonEventArgs buttonEventArgs)
        {
            return _shape.GetGlobalBounds().Contains(buttonEventArgs.X, buttonEventArgs.Y);
        }
        public void UpdateSprite(Piece piece)
        {
            this._sprite = Application.Instance().AssetManager.GetPieceSprite(piece);
            this._sprite.Position = this._shape.Position;
        }
        public void Draw()
        {
            Application.Instance().MainWindow.Draw(_shape);
            Application.Instance().MainWindow.Draw(_sprite);
        }
    }
}