namespace Chess.GameLogic
{
    class CastleMove : Move
    {
        public Piece Rook { get; private set; }
        public Position RookOrigin { get; private set; }
        public Position RookDestination { get; private set; }

        public CastleMove(Piece king, Position kingOrigin, Position kingDestination, Piece rook, 
                          Position rookOrigin, Position rookDestination) : base(king, kingOrigin, kingDestination)
        {
            this.Rook = PieceFactory.CopyPiece(rook);
            this.RookOrigin = rookOrigin;
            this.RookDestination = rookDestination;
        }
        public override void Execute(Board board)
        {
            board.CastleMove(Origin, Destination, RookOrigin, RookDestination);
        }
        public override void Undo(Board board)
        {
            board.CastleUndo(Moved, Rook, Destination, RookDestination);
        }
        public override string ToString()
        {
            // Queen Side
            if (RookOrigin.X == 0)
            {
                return "0-0-0";
            }
            // King Side
            else
            {
                return "0-0";
            }
        }
    }
}