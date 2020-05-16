using Chess.Systems;
using SFML.Window;
using SFML.System;

namespace Chess.ClientStates
{
    abstract class ClientState
    {
        public abstract void Init();
        public abstract void HandleInput(MouseButtonEventArgs buttonEventArgs);
        public abstract void Update(Vector2i mousePosition);
        public abstract void Render();
    }
}