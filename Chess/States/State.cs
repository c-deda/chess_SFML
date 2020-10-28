using SFML.Window;
using SFML.System;

namespace Chess.States
{
    abstract class State
    {
        public abstract void Init();
        public abstract void HandleInput(MouseButtonEventArgs buttonEventArgs);
        public abstract void HandleInput(MouseWheelScrollEventArgs wheelEventArgs);
        public abstract void Update(Vector2i mousePosition);
        public abstract void Render();
    }
}