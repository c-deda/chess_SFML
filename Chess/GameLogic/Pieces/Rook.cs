namespace Chess.GameLogic
{
    class Rook : Piece
    {
        public Rook(Position position, ChessColor color, PieceType type) : base(position, color, type) {}

        public override void FindPotentialMoves(Board board)
        {
            int newX = this.Position.X;
            int newY = this.Position.Y;

            // Down
            while (newY > 0)
            {
                --newY;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }

            newY = this.Position.Y;

            // Up
            while (newY < GlobalConstants.BoardLength - 1)
            {
                ++newY;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }

            newX = this.Position.X;
            newY = this.Position.Y;

            // Left
            while (newX > 0)
            {
                --newX;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }

            newX = this.Position.X;

            // Right
            while (newX < GlobalConstants.BoardLength - 1)
            {
                ++newX;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }
        }
        public override bool CanAttack(Board board, Position newPosition)
        {
            if (newPosition.X == this.Position.X || newPosition.Y == this.Position.Y)
            {
                int x = this.Position.X;
                int y = this.Position.Y;

                // Up
                if (newPosition.Y > this.Position.Y)
                {
                    y = this.Position.Y;

                    while (true)
                    {
                        y++;

                        if (x == newPosition.X && y == newPosition.Y)
                        {
                            return true;
                        }
                        else if (board.GetPieceAt(x,y) != null)
                        {
                            return false;
                        }
                    }
                }
                // Down
                else if (newPosition.Y < this.Position.Y)
                {
                    y = this.Position.Y;

                    while (true)
                    {
                        y--;

                        if (x == newPosition.X && y == newPosition.Y)
                        {
                            return true;
                        }
                        else if (board.GetPieceAt(x,y) != null)
                        {
                            return false;
                        }
                    }
                }
                // Left
                else if (newPosition.X < this.Position.X)
                {
                    y = this.Position.Y;

                    while (true)
                    {
                        x--;

                        if (x == newPosition.X && y == newPosition.Y)
                        {
                            return true;
                        }
                        else if (board.GetPieceAt(x,y) != null)
                        {
                            return false;
                        }
                    }
                }
                // Right
                else
                {
                    x = this.Position.X;

                    while (true)
                    {
                        x++;

                        if (x == newPosition.X && y == newPosition.Y)
                        {
                            return true;
                        }
                        else if (board.GetPieceAt(x,y) != null)
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
