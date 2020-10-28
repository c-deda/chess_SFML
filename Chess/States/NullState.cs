using SFML.Window;

namespace Chess.States
{
    class NullState : State
    {
        public override void Init() {}
        public override void HandleInput(SFML.Window.MouseButtonEventArgs buttonEventArgs) {}
        public override void HandleInput(MouseWheelScrollEventArgs wheelEventArgs) {}
        public override void Update(SFML.System.Vector2i mousePosition) {}
        public override void Render() {}
    }
}