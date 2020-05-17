using System;

namespace Chess.GameLogic.Pieces
{
    class Knight : Piece
    {
        public Knight(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            // Down 2 and Left 1
            int newX = this.position.x - 1;
            int newY = this.position.y - 2;
            AddIfNotAlly(board, newX, newY);

            // Down 1 and Left 2
            newX = this.position.x - 2;
            newY = this.position.y - 1;
            AddIfNotAlly(board, newX, newY);


            // Down 2 and Right 1
            newX = this.position.x + 1;
            newY = this.position.y - 2;
            AddIfNotAlly(board, newX, newY);

            // Down 1 and Right 2
            newX = this.position.x + 2;
            newY = this.position.y - 1;
            AddIfNotAlly(board, newX, newY);


            // Up 2 and Left 1
            newX = this.position.x - 1;
            newY = this.position.y + 2;
            AddIfNotAlly(board, newX, newY);

            // Up 1 and Left 2
            newX = this.position.x - 2;
            newY = this.position.y + 1;
            AddIfNotAlly(board, newX, newY);

            // Up 2 and Right 1
            newX = this.position.x + 1;
            newY = this.position.y + 2;
            AddIfNotAlly(board, newX, newY);

            // Up 1 and Right 2
            newX = this.position.x + 2;
            newY = this.position.y + 1;
            AddIfNotAlly(board, newX, newY);
        }
        public override bool CanAttack(Board board, Position newPosition)
        {
            return ((Math.Abs(newPosition.y - this.position.y) == 2 && Math.Abs(newPosition.x - this.position.x) == 1) ||
                   (Math.Abs(newPosition.y - this.position.y) == 1 && Math.Abs(newPosition.x - this.position.x) == 2));
        }
    }
}
