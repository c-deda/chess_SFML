using System;

namespace Chess.GameLogic
{
    class Bishop : Piece
    {
        public Bishop(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            int newX = this.Position.X;
            int newY = this.Position.Y;

            // Down And Left
            while (newX > 0 && newY > 0)
            {
                --newX;
                --newY;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }

            newX = this.Position.X;
            newY = this.Position.Y;

            // Down And Right
            while (newX < GlobalConstants.BoardLength - 1 && newY > 0)
            {
                ++newX;
                --newY;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }

            newX = this.Position.X;
            newY = this.Position.Y;

            // Up And Left
            while (newX > 0 && newY < GlobalConstants.BoardLength - 1)
            {
                --newX;
                ++newY;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }

            newX = this.Position.X;
            newY = this.Position.Y;

            // Up And Right
            while (newX < GlobalConstants.BoardLength - 1 && newY < GlobalConstants.BoardLength - 1)
            {
                ++newX;
                ++newY;

                if (!AddIfEmpty(board, newX, newY))
                {
                    AddIfNotAlly(board, newX, newY);
                    break;
                }
            }
        }

        public override bool CanAttack(Board board, Position newPosition)
        {
            if (Math.Abs(newPosition.Y - this.Position.Y) == Math.Abs(newPosition.X - this.Position.X))
            {
                int x = this.Position.X;
                int y = this.Position.Y;

                // Up and Right
                if (newPosition.Y > this.Position.Y && newPosition.X > this.Position.X)
                {
                    while (true)
                    {
                        ++x;
                        ++y;

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
                // Up and Left
                else if (newPosition.Y > this.Position.Y && newPosition.X < this.Position.X)
                {
                    x = this.Position.X;
                    y = this.Position.Y;

                    while (true)
                    {
                        --x;
                        ++y;

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
                // Down and Left
                else if (newPosition.Y < this.Position.Y && newPosition.X < this.Position.X)
                {
                    x = this.Position.X;
                    y = this.Position.Y;

                    while (true)
                    {
                        --x;
                        --y;

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
                // Down and Right
                else if (newPosition.Y < this.Position.Y && newPosition.X > this.Position.X)
                {
                    x = this.Position.X;
                    y = this.Position.Y;

                    while (true)
                    {
                        ++x;
                        --y;

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
            return "B";
        }
    }
}
