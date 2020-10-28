using Chess.States;

namespace Chess.UI
{
    class ResetGameCommand : Command
    {
        private PlayState _playState;
        public ResetGameCommand(PlayState playState)
        {
            _playState = playState;
        }
        public override void Execute()
        {
            _playState.ResetGame();
        }
    }
}