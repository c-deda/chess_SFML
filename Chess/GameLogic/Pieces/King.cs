using System;

namespace Chess.GameLogic
{
    class King : Piece
    {
        public King(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            int newX;
            int newY = this.Position.Y;

            // Castling
            if (!HasMoved)
            {
                newX = this.Position.X + 2;
                AddIfEmpty(board, newX, newY);

                newX = this.Position.X - 2;
                AddIfEmpty(board, newX, newY);
            }

            // Left
            newX = this.Position.X - 1;
            AddIfNotAlly(board, newX, newY);

            // Right
            newX = this.Position.X + 1;
            AddIfNotAlly(board, newX, newY);

            // Down
            newX = this.Position.X;
            newY = this.Position.Y - 1;
            AddIfNotAlly(board, newX, newY);

            // Up
            newY = this.Position.Y + 1;
            AddIfNotAlly(board, newX, newY);

            // Down and Left
            newX = this.Position.X - 1;
            newY = this.Position.Y - 1;
            AddIfNotAlly(board, newX, newY);

            // Down and Right
            newX = this.Position.X + 1;
            newY = this.Position.Y - 1;
            AddIfNotAlly(board, newX, newY);

            // Up and Left
            newX = this.Position.X - 1;
            newY = this.Position.Y + 1;
            AddIfNotAlly(board, newX, newY);

            // Up and Right
            newX = this.Position.X + 1;
            newY = this.Position.Y + 1;
            AddIfNotAlly(board, newX, newY);
        }
        public override bool CanAttack(Board board, Position newPosition)
        {
            return ((Math.Abs(newPosition.X - this.Position.X) == 1 && Math.Abs(newPosition.Y - this.Position.Y) == 1));
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
