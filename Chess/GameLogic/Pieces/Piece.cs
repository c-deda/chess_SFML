using System.Collections.Generic;

namespace Chess.GameLogic.Pieces
{
    abstract class Piece
    {
        public Position position { get; set; }
        public ChessColor color { get; private set; }
        public PieceType type { get; private set; }
        public bool hasMoved { get; set; }
        public List<Move> validMoves;

        public Piece(Position position, ChessColor color, PieceType type)
        {
            this.position = position;
            this.color = color;
            this.type = type;
            validMoves = new List<Move>();
        }
        public void Move(Position destination)
        {
            if (!hasMoved)
            {
                hasMoved = true;
            }
            
            this.position = destination;
        }
        public bool AddIfEmpty(Board board, int newX, int newY)
        {
            if (newX >= 0 && newX < GlobalConstants.BoardLength && newY >= 0 && newY < GlobalConstants.BoardLength)
            {
                if (board.pieces[newX,newY] == null)
                {
                    this.validMoves.Add(new Move(this, this.position, new Position(newX, newY)));
                    return true;
                }
            }
            
            return false;
        }
        public bool AddIfNotAlly(Board board, int newX, int newY)
        {
            if (newX >= 0 && newX < GlobalConstants.BoardLength && newY >= 0 && newY < GlobalConstants.BoardLength)
            {
                if (board.pieces[newX,newY] != null)
                {
                    if (board.pieces[newX,newY].color != this.color)
                    {
                        this.validMoves.Add(new CaptureMove(this, this.position, new Position(newX, newY), board.pieces[newX, newY]));
                        return true;
                    }
                }
                else
                {
                    this.validMoves.Add(new Move(this, this.position, new Position(newX, newY)));
                    return true;
                }
            }

            return false;
        }
        public void ClearValidMoves()
        {
            this.validMoves.Clear();
        }
        public bool HasMove(Position destination)
        {
            foreach (Move move in validMoves)
            {
                if (move.destination == destination)
                {
                    return true;
                }
            }

            return false;
        }
        public Move GetMove(Position destination)
        {
            foreach (Move move in validMoves)
            {
                if (move.destination == destination)
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