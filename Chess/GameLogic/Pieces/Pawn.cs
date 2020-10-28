using System;

namespace Chess.GameLogic
{
    class Pawn : Piece
    {
        public Pawn(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            int newX = this.Position.X;
            int newY;

            // White
            if (this.Color == ChessColor.White)
            {
                // Forward
                newY = this.Position.Y + 1;

                if (AddIfEmpty(board, newX, newY))
                {
                    if (!HasMoved)
                    {
                        newY = this.Position.Y + 2;
                        AddIfEmpty(board, newX, newY);
                    }
                }

                // Forward and Left
                newX = this.Position.X - 1;
                newY = this.Position.Y + 1;
                AddIfNotAlly(board, newX, newY);

                // Forward and Right
                newX = this.Position.X + 1;
                newY = this.Position.Y + 1;
                AddIfNotAlly(board, newX, newY);
            }
            // Black
            else
            {
                // Forward
                newY = this.Position.Y - 1;

                if (AddIfEmpty(board, newX, newY))
                {
                    if (!HasMoved)
                    {
                        newY = this.Position.Y - 2;
                        AddIfEmpty(board, newX, newY);
                    }
                }

                // Forward and Left
                newX = this.Position.X - 1;
                newY = this.Position.Y - 1;
                AddIfNotAlly(board, newX, newY);

                // Forward and Right
                newX = this.Position.X + 1;
                newY = this.Position.Y - 1;
                AddIfNotAlly(board, newX, newY);
            }
        }
        public override bool CanAttack(Board board, Position newPosition)
        {
            // White
            if (this.Color == ChessColor.White)
            {
                return (newPosition.Y - this.Position.Y == 1 && Math.Abs(newPosition.X - this.Position.X) == 1);
            }
            // Black
            else
            {
                return (this.Position.Y - newPosition.Y == 1 && Math.Abs(newPosition.X - this.Position.X) == 1);
            }
        }
        public override string ToString()
        {
            return "";
        }
    }
}
