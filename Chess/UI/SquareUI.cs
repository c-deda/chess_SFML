using Chess.GameLogic.Pieces;
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
        private Sprite sprite;
        private RectangleShape shape;
        private Color idleColor;
        private Color highlightColor;

        public SquareUI(Position position, Piece piece, bool isDark)
        {
            if (isDark)
            {
                this.idleColor = DarkColorIdle;
                this.highlightColor = DarkColorHighlight;
            }
            else
            {
                this.idleColor = LightColorIdle;
                this.highlightColor = LightColorHighlight;
            }
            
            // Square
            shape = new RectangleShape(new Vector2f(GlobalConstants.SquareSize, GlobalConstants.SquareSize));
            shape.FillColor = idleColor;
            shape.Position = new Vector2f(GlobalConstants.SquareSize * position.x, GlobalConstants.SquareSize * position.y);
            // Sprite
            sprite = GameClient.Instance().assetManager.GetPieceSprite(piece);
            sprite.Position = shape.Position;
        }
        public void MirrorSquare(Position position)
        {
            shape.Position = new Vector2f(GlobalConstants.SquareSize * position.x, GlobalConstants.SquareSize * position.y);
            sprite.Position = shape.Position;
        }
        public void Select()
        {
            shape.FillColor = highlightColor;
        }
        public void Unselect()
        {
            shape.FillColor = idleColor;
        }
        public bool IsClicked(MouseButtonEventArgs buttonEventArgs)
        {
            return shape.GetGlobalBounds().Contains(buttonEventArgs.X, buttonEventArgs.Y);
        }
        public void UpdateSprite(Piece piece)
        {
            this.sprite = GameClient.Instance().assetManager.GetPieceSprite(piece);
            this.sprite.Position = this.shape.Position;
        }
        public void Draw()
        {
            GameClient.Instance().mainWindow.Draw(shape);
            GameClient.Instance().mainWindow.Draw(sprite);
        }
    }
}