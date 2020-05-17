using System;

namespace Chess.GameLogic.Pieces
{
    class Pawn : Piece
    {
        public Pawn(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            int newX = this.position.x;
            int newY;

            // White
            if (this.color == ChessColor.White)
            {
                // Forward
                newY = this.position.y + 1;

                if (AddIfEmpty(board, newX, newY))
                {
                    if (!hasMoved)
                    {
                        newY = this.position.y + 2;
                        AddIfEmpty(board, newX, newY);
                    }
                }

                // Forward and Left
                newX = this.position.x - 1;
                newY = this.position.y + 1;
                AddIfNotAlly(board, newX, newY);

                // Forward and Right
                newX = this.position.x + 1;
                newY = this.position.y + 1;
                AddIfNotAlly(board, newX, newY);
            }
            // Black
            else
            {
                // Forward
                newY = this.position.y - 1;

                if (AddIfEmpty(board, newX, newY))
                {
                    if (!hasMoved)
                    {
                        newY = this.position.y - 2;
                        AddIfEmpty(board, newX, newY);
                    }
                }

                // Forward and Left
                newX = this.position.x - 1;
                newY = this.position.y - 1;
                AddIfNotAlly(board, newX, newY);

                // Forward and Right
                newX = this.position.x + 1;
                newY = this.position.y - 1;
                AddIfNotAlly(board, newX, newY);
            }
        }
        public override bool CanAttack(Board board, Position newPosition)
        {
            // White
            if (this.color == ChessColor.White)
            {
                return (newPosition.y - this.position.y == 1 && Math.Abs(newPosition.x - this.position.x) == 1);
            }
            // Black
            else
            {
                return (this.position.y - newPosition.y == 1 && Math.Abs(newPosition.x - this.position.x) == 1);
            }
        }
    }
}
