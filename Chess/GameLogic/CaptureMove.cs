using Chess.GameLogic.Pieces;

namespace Chess.GameLogic
{
    class CaptureMove : Move
    {
        public Piece captured { get; private set; }
        public CaptureMove(Piece moved, Position origin, Position destination, Piece captured) : base(moved, origin, destination)
        {
            this.captured = PieceFactory.CreatePiece(captured.position, captured.color, captured.type);
            this.captured.hasMoved = captured.hasMoved;
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