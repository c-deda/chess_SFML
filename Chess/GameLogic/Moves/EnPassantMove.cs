namespace Chess.GameLogic
{
    class EnPassantMove : Move
    {
        public Piece Captured { get; private set; }
        public EnPassantMove(Piece moved, Position origin, Position destination, Piece captured) : base(moved, origin, destination)
        {
            this.Captured = PieceFactory.CopyPiece(captured);
        }
        public override void Execute(Board board)
        {
            board.EnPassantMove(Origin, Destination, Captured);
        }
        public override void Undo(Board board)
        {
            board.CaptureUndo(Moved, Captured, Destination);
        }
        public override string ToString()
        {
            return GetColString(Origin.X) + "x" + GetTileString(Destination);
        }
    }
}