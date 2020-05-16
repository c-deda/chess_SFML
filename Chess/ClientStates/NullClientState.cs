namespace Chess.ClientStates
{
    class NullClientState : ClientState
    {
        public override void Init() {}
        public override void HandleInput(SFML.Window.MouseButtonEventArgs buttonEventArgs) {}
        public override void Update(SFML.System.Vector2i mousePosition) {}
        public override void Render() {}
    }
}