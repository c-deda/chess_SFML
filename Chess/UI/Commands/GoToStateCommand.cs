using Chess.States;

namespace Chess.UI
{
    class GoToStateCommand : Command
    {
        private State _newState;

        public GoToStateCommand(State newState)
        {
            this._newState = newState;
        }
        public override void Execute()
        {
            Application.Instance().StateManager.GoToState(_newState);
        }
    }
}