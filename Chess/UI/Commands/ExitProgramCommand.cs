namespace Chess.UI
{
    class ExitProgramCommand : Command
    {
        public override void Execute()
        {
            Application.Instance().MainWindow.Close();
        }
    }
}