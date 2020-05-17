using Chess.GameLogic.Pieces;

namespace Chess.GameLogic
{
    class EnPassantMove : Move
    {
        public Piece captured { get; private set; }
        public EnPassantMove(Piece moved, Position origin, Position destination, Piece captured) : base(moved, origin, destination)
        {
            this.captured = PieceFactory.CopyPiece(captured);
        }
        public override void Execute(Board board)
        {
            board.EnPassantMove(origin, destination, captured);
        }
        public override void Undo(Board board)
        {
            board.EnPassantUndo(moved, captured, destination);
        }
    }
}