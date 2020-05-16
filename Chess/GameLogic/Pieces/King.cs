using System;

namespace Chess.GameLogic.Pieces
{
    class King : Piece
    {
        public bool isChecked { get; set; }
        public King(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            ClearValidMoves();
            
            int newX;
            int newY = this.position.y;

            // Castling
            if (!hasMoved)
            {
                newX = this.position.x + 2;
                AddIfEmpty(board, newX, newY);

                newX = this.position.x - 2;
                AddIfEmpty(board, newX, newY);
            }

            // Left
            newX = this.position.x - 1;
            AddIfNotAlly(board, newX, newY);

            // Right
            newX = this.position.x + 1;
            AddIfNotAlly(board, newX, newY);

            // Down
            newX = this.position.x;
            newY = this.position.y - 1;
            AddIfNotAlly(board, newX, newY);

            // Up
            newY = this.position.y + 1;
            AddIfNotAlly(board, newX, newY);

            // Down and Left
            newX = this.position.x - 1;
            newY = this.position.y - 1;
            AddIfNotAlly(board, newX, newY);

            // Down and Right
            newX = this.position.x + 1;
            newY = this.position.y - 1;
            AddIfNotAlly(board, newX, newY);

            // Up and Left
            newX = this.position.x - 1;
            newY = this.position.y + 1;
            AddIfNotAlly(board, newX, newY);

            // Up and Right
            newX = this.position.x + 1;
            newY = this.position.y + 1;
            AddIfNotAlly(board, newX, newY);
        }
        public override bool CanAttack(Board board, Position newPosition)
        {
            return ((Math.Abs(newPosition.x - this.position.x) == 1 && Math.Abs(newPosition.y - this.position.y) == 1));
        }
    }
}
