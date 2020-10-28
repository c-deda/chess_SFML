using SFML.Graphics;
using SFML.System;

namespace Chess.UI
{
    abstract class Button : UIElement
    {
        private static Command nullCommand = new NullCommand();
        protected Command _command;
        protected RectangleShape _shape;
        protected Color _idleColor;
        protected Color _hoverColor;
        protected bool _isEnabled;

        // Buttons with text
        public Button(Vector2f position, Vector2f size, Color idleColor, Color hoverColor)
        {
            this._idleColor = idleColor;
            this._hoverColor = hoverColor;
            _command = nullCommand;
            _isEnabled = true;

            InitShape(position, size);
        }
        private void InitShape(Vector2f position, Vector2f size)
        {
            this._shape = new RectangleShape(size);
            this._shape.Position = position;
            this._shape.FillColor = _idleColor;
        }
        public void SetBorder(uint outlineThickness, Color outlineColor)
        {
            _shape.Size = new Vector2f(_shape.Size.X - (outlineThickness * 2), _shape.Size.Y - (outlineThickness * 2));
            _shape.Position = new Vector2f(_shape.Position.X + outlineThickness, _shape.Position.Y + outlineThickness);
            _shape.OutlineThickness = outlineThickness;
            _shape.OutlineColor = outlineColor;
        }
        public override void OnHover(float x, float y)
        {
            if (_isEnabled)
            {
                _shape.FillColor = _hoverColor;
            }
        }
        public override void OnIdle(float x, float y)
        {
            _shape.FillColor = _idleColor;
        }
        public override void OnClick(float x, float y)
        {
            if (_isEnabled)
            {
                _command.Execute();
            }
        }
        public override void Draw()
        {
            Application.Instance().MainWindow.Draw(_shape);
        }
        public override bool Contains(float x, float y)
        {
            return _shape.GetGlobalBounds().Contains(x, y);
        }
        public virtual void Enable()
        {
            _isEnabled = true;
            _idleColor = new Color(_idleColor.R, _idleColor.B, _idleColor.G, 255);
            _shape.FillColor = _idleColor;
        }
        public virtual void Disable()
        {
            _isEnabled = false;
            _idleColor = new Color(_idleColor.R, _idleColor.B, _idleColor.G, 100);
            _shape.FillColor = _idleColor;
        }
        public void SetCommand(Command command)
        {
            this._command = command;
        }
    }
}