using Chess.GameLogic.Pieces;

namespace Chess.GameLogic
{
    class Move
    {
        public Piece moved { get; private set; }
        public Position origin { get; private set; }
        public Position destination { get; private set; }

        public Move(Piece moved, Position origin, Position destination)
        {
            this.moved = PieceFactory.CreatePiece(moved.position, moved.color, moved.type);
            this.moved.hasMoved = moved.hasMoved;
            this.origin = origin;
            this.destination = destination;
        }
        public virtual void Execute(Board board)
        {
            board.NormalMove(origin, destination);
        }
        public virtual void Undo(Board board)
        {
            
        }
    }
}