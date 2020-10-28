using System.Collections.Generic;

namespace Chess.GameLogic
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public ChessColor Color { get; private set; }
        public PieceType Type { get; private set; }
        public bool HasMoved { get; set; }
        public List<Move> ValidMoves { get; set; }

        public Piece(Position position, ChessColor color, PieceType type)
        {
            this.Position = position;
            this.Color = color;
            this.Type = type;
            ValidMoves = new List<Move>();
        }
        public void UpdatePosition(Position destination)
        {
            if (!HasMoved)
            {
                HasMoved = true;
            }
            
            this.Position = destination;
        }
        public bool AddIfEmpty(Board board, int newX, int newY)
        {
            if (newX >= 0 && newX < GlobalConstants.BoardLength && newY >= 0 && newY < GlobalConstants.BoardLength)
            {
                if (board.GetPieceAt(newX,newY) == null)
                {
                    this.ValidMoves.Add(new Move(this, this.Position, new Position(newX, newY)));
                    return true;
                }
            }
            
            return false;
        }
        public bool AddIfNotAlly(Board board, int newX, int newY)
        {
            if (newX >= 0 && newX < GlobalConstants.BoardLength && newY >= 0 && newY < GlobalConstants.BoardLength)
            {
                if (board.GetPieceAt(newX,newY) != null)
                {
                    if (board.GetPieceAt(newX,newY).Color != this.Color)
                    {
                        this.ValidMoves.Add(new CaptureMove(this, this.Position, new Position(newX, newY), board.GetPieceAt(newX,newY)));
                        return true;
                    }
                }
                else
                {
                    this.ValidMoves.Add(new Move(this, this.Position, new Position(newX, newY)));
                    return true;
                }
            }

            return false;
        }
        public void ClearValidMoves()
        {
            this.ValidMoves.Clear();
        }
        public bool HasMove(Position destination)
        {
            foreach (Move move in ValidMoves)
            {
                if (move.Destination == destination)
                {
                    return true;
                }
            }

            return false;
        }
        public Move GetMove(Position destination)
        {
            foreach (Move move in ValidMoves)
            {
                if (move.Destination == destination)
                {
                    return move;
                }
            }

            return null;
        }
        public abstract void FindPotentialMoves(Board board);
        public abstract bool CanAttack(Board board, Position newPosition);
    }
}