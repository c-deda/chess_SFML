using SFML.Graphics;
using SFML.System;

namespace Chess.UI
{
    class Button
    {
        private static Command nullCommand = new NullCommand();
        private Command command;
        private Text text;
        private RectangleShape shape;
        private Color idleColor;
        private Color hoverColor;
        private bool isEnabled;

        // Buttons with text
        public Button(Font font, string text, uint characterSize, Color textColor, Color idleColor, Color hoverColor, Vector2f position, Vector2f size)
        {
            InitColors(idleColor, hoverColor);
            InitShape(position, size);
            InitText(font, text, characterSize, textColor);
            command = nullCommand;
            isEnabled = true;
        }
        private void InitColors(Color idleColor, Color hoverColor)
        {
            this.idleColor = idleColor;
            this.hoverColor = hoverColor;
        }
        private void InitShape(Vector2f position, Vector2f size)
        {
            this.shape = new RectangleShape(size);
            this.shape.Position = position;
            this.shape.FillColor = idleColor;
        }
        private void InitText(Font font, string text, uint characterSize, Color textColor)
        {
            this.text = new Text(text, font, characterSize);
            this.text.Origin = new Vector2f(this.text.GetGlobalBounds().Left, this.text.GetGlobalBounds().Top);
            this.text.FillColor = Color.Black;

            this.text.Position = new Vector2f(
                this.shape.Position.X + (this.shape.Size.X / 2.0f) - (this.text.GetGlobalBounds().Width / 2.0f),
                this.shape.Position.Y + (this.shape.Size.Y / 2.0f) - (this.text.GetGlobalBounds().Height / 2.0f)
            );
        }
        public void SetOutLine(uint outlineThickness, Color outlineColor)
        {
            this.shape.OutlineThickness = outlineThickness;
            this.shape.OutlineColor = outlineColor;
        }
        public void Hover()
        {
            if (isEnabled)
            {
                shape.FillColor = hoverColor;
            }
        }
        public void Idle()
        {
            shape.FillColor = idleColor;
        }
        public void Click()
        {
            if (isEnabled)
            {
                command.Execute();
            }
        }
        public void Draw()
        {
            GameClient.Instance().mainWindow.Draw(shape);
            GameClient.Instance().mainWindow.Draw(text);
        }
        public bool Contains(float x, float y)
        {
            return shape.GetGlobalBounds().Contains(x, y);
        }
        public void Enable()
        {
            isEnabled = true;
            idleColor = new Color(idleColor.R, idleColor.B, idleColor.G, 255);
            shape.FillColor = idleColor;
            text.FillColor = new Color(text.FillColor.R, text.FillColor.B, text.FillColor.B, 255);
        }
        public void Disable()
        {
            isEnabled = false;
            idleColor = new Color(idleColor.R, idleColor.B, idleColor.G, 100);
            shape.FillColor = idleColor;
            text.FillColor = new Color(text.FillColor.R, text.FillColor.B, text.FillColor.B, 100);
        }
        public void SetCommand(Command command)
        {
            this.command = command;
        }
    }
}