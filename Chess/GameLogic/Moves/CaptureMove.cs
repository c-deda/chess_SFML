namespace Chess.GameLogic
{
    class CaptureMove : Move
    {
        public Piece Captured { get; private set; }
        public CaptureMove(Piece moved, Position origin, Position destination, Piece captured) : base(moved, origin, destination)
        {
            this.Captured = PieceFactory.CopyPiece(captured);
        }
        public override void Execute(Board board)
        {
            board.CaptureMove(Origin, Destination);
        }
        public override void Undo(Board board)
        {
            board.CaptureUndo(Moved, Captured, Destination);
        }
        public override string ToString()
        {
            if (Moved.Type == PieceType.Pawn)
            {
                return GetColString(Origin.X) + "x" + GetTileString(Destination);
            }
            else
            {
                return Moved.ToString() + "x" + GetTileString(Destination);
            }
        }
    }
}