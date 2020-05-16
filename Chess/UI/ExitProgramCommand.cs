namespace Chess.UI
{
    class ExitProgramCommand : Command
    {
        public override void Execute()
        {
            GameClient.Instance().mainWindow.Close();
        }
    }
}