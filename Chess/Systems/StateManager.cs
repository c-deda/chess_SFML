using Chess.ClientStates;

namespace Chess.Systems
{
    class StateManager
    {
        public ClientState activeState { get; private set; }
        private ClientState newState { get; set; }
        private bool stateToChange;

        public void GoToState(ClientState newState)
        {
            this.newState = newState;
            stateToChange = true;
        }
        public void ProcessStateChange()
        {
            if (stateToChange)
            {
                activeState = newState;
                stateToChange = false;
                activeState.Init();
            }
        }
    }
}