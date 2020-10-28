using System;

namespace Chess.GameLogic
{
    class Knight : Piece
    {
        public Knight(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            // Down 2 and Left 1
            int newX = this.Position.X - 1;
            int newY = this.Position.Y - 2;
            AddIfNotAlly(board, newX, newY);

            // Down 1 and Left 2
            newX = this.Position.X - 2;
            newY = this.Position.Y - 1;
            AddIfNotAlly(board, newX, newY);


            // Down 2 and Right 1
            newX = this.Position.X + 1;
            newY = this.Position.Y - 2;
            AddIfNotAlly(board, newX, newY);

            // Down 1 and Right 2
            newX = this.Position.X + 2;
            newY = this.Position.Y - 1;
            AddIfNotAlly(board, newX, newY);


            // Up 2 and Left 1
            newX = this.Position.X - 1;
            newY = this.Position.Y + 2;
            AddIfNotAlly(board, newX, newY);

            // Up 1 and Left 2
            newX = this.Position.X - 2;
            newY = this.Position.Y + 1;
            AddIfNotAlly(board, newX, newY);

            // Up 2 and Right 1
            newX = this.Position.X + 1;
            newY = this.Position.Y + 2;
            AddIfNotAlly(board, newX, newY);

            // Up 1 and Right 2
            newX = this.Position.X + 2;
            newY = this.Position.Y + 1;
            AddIfNotAlly(board, newX, newY);
        }
        public override bool CanAttack(Board board, Position newPosition)
        {
            return ((Math.Abs(newPosition.Y - this.Position.Y) == 2 && Math.Abs(newPosition.X - this.Position.X) == 1) ||
                   (Math.Abs(newPosition.Y - this.Position.Y) == 1 && Math.Abs(newPosition.X - this.Position.X) == 2));
        }
        public override string ToString()
        {
            return "N";
        }
    }
}
