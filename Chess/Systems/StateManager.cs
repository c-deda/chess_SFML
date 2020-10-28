using Chess.States;

namespace Chess.Systems
{
    class StateManager
    {
        public State activeState { get; private set; }
        private State newState { get; set; }
        private bool stateToChange;

        public void GoToState(State newState)
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