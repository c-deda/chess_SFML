using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using Chess.Systems;

namespace Chess.UI
{
    class MoveHistory : UIElement
    {
        private Font font = new Font(Application.Instance().AssetManager.Fonts[FontID.MoveHistoryFont]);
        private const int CharacterSize = 24;
        private const int TextElementHeight = 40;
        public RectangleShape BoundingBox {get; private set; }
        private int _scrollPosition;
        private List<Text> _textElements;

        public MoveHistory(Vector2f position, Vector2f size)
        {
            BoundingBox = new RectangleShape(size);
            BoundingBox.Position = position;

            _textElements = new List<Text>();

            _scrollPosition = 0;
        }
        public override void OnScroll(float delta)
        {
            // Scroll Up
            if (delta > 0 && _scrollPosition > 0)
            {
                --_scrollPosition;
            }
            // Scroll Down
            else if (delta < 0 && _textElements.Count - _scrollPosition > BoundingBox.Size.Y / TextElementHeight)
            {
                ++_scrollPosition;
            }
        }
        public override bool Contains(float x, float y)
        {
            return BoundingBox.GetGlobalBounds().Contains(x, y);
        }
        public override void Draw()
        {
            int elementsDrawn = 0;

            Application.Instance().MainWindow.Draw(BoundingBox);

            for (int i = _scrollPosition; i < _textElements.Count && elementsDrawn < BoundingBox.Size.Y / TextElementHeight; ++i)
            {
                SetElementPosition(_textElements[i], elementsDrawn);
                Application.Instance().MainWindow.Draw(_textElements[i]);
                ++elementsDrawn;
            }
        }
        public void AddText(bool newLine, string newText)
        {
            if (newLine)
            {
                Text textToAdd = new Text("", font, CharacterSize);
                textToAdd.Origin = new Vector2f(textToAdd.GetGlobalBounds().Left, textToAdd.GetGlobalBounds().Top);
                textToAdd.FillColor = Color.Black;
                textToAdd.DisplayedString = $"{newText,-15}";
                _textElements.Add(textToAdd);
            }
            else
            {
                _textElements[_textElements.Count-1].DisplayedString += newText;
            }

            // Check to see if we should scroll down
            while (_textElements.Count - _scrollPosition > 12)
            {
                ++_scrollPosition;
            }
        }
        private void SetElementPosition(Text textElement, int yOffsetValue)
        {
            textElement.Position = new Vector2f(BoundingBox.Position.X + 40, BoundingBox.Position.Y + (TextElementHeight * yOffsetValue) + 10);
        }
        public void Reset()
        {
            _textElements.Clear();
            _scrollPosition = 0;
        }
    }
}