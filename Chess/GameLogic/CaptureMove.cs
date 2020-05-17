using Chess.GameLogic.Pieces;

namespace Chess.GameLogic
{
    class CaptureMove : Move
    {
        public Piece captured { get; private set; }
        public CaptureMove(Piece moved, Position origin, Position destination, Piece captured) : base(moved, origin, destination)
        {
            this.captured = PieceFactory.CopyPiece(captured);
        }
        public override void Execute(Board board)
        {
            board.NormalMove(origin, destination);
        }
        public override void Undo(Board board)
        {
            board.CaptureUndo(moved, captured);
        }
    }
}