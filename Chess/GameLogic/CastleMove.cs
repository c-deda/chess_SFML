using Chess.GameLogic.Pieces;

namespace Chess.GameLogic
{
    class CastleMove : Move
    {
        public Piece rook { get; private set; }
        public Position rookOrigin { get; private set; }
        public Position rookDestination { get; private set; }

        public CastleMove(Piece king, Position kingOrigin, Position kingDestination, Piece rook, 
                          Position rookOrigin, Position rookDestination) : base(king, kingOrigin, kingDestination)
        {
            this.rook = PieceFactory.CopyPiece(rook);
            this.rookOrigin = rookOrigin;
            this.rookDestination = rookDestination;
        }
        public override void Execute(Board board)
        {
            board.CastleMove(origin, destination, rookOrigin, rookDestination);
        }
        public override void Undo(Board board)
        {
            board.CastleUndo(moved, rook, destination, rookDestination);
        }
    }
}