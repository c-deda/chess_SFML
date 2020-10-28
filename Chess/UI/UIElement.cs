using SFML.System;

namespace Chess.UI
{
    abstract class UIElement
    {
        public virtual void OnHover(float x, float y) { }
        public virtual void OnIdle(float x, float y) { }
        public virtual void OnClick(float x, float y) { }
        public virtual void OnScroll(float delta) { }
        public virtual void Draw() { }
        public virtual bool Contains(float x, float y) { return false; }
    }
}