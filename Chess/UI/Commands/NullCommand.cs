namespace Chess.UI
{
    class NullCommand : Command
    {
        public override void Execute() {}
        public override void Undo() {}
    }
}