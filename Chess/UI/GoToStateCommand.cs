using Chess.ClientStates;

namespace Chess.UI
{
    class GoToStateCommand : Command
    {
        private ClientState newState;

        public GoToStateCommand(ClientState newState)
        {
            this.newState = newState;
        }
        public override void Execute()
        {
            GameClient.Instance().stateManager.GoToState(newState);
        }
    }
}