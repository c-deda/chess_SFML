using System;

namespace Chess.GameLogic
{
    class Queen : Piece
    {
        public Queen(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            // - - - Bishop Moveset - - - //

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

            // - - - Rook Moveset - - - //

            newX = this.Position.X;
            newY = this.Position.Y;

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
            int x = this.Position.X;
            int y = this.Position.Y;

            // - - - Bishop Moveset - - - //

            if (Math.Abs(newPosition.Y - this.Position.Y) == Math.Abs(newPosition.X - this.Position.X))
            {
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
            // - - - Rook Moveset - - - //
            else if (newPosition.X == this.Position.X || newPosition.Y == this.Position.Y)
            {
                x = this.Position.X;
                y = this.Position.Y;
                
                // Up
                if (newPosition.Y > this.Position.Y)
                {
                    y = this.Position.Y;

                    while (true)
                    {
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
                // Down
                else if (newPosition.Y < this.Position.Y)
                {
                    y = this.Position.Y;

                    while (true)
                    {
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
                // Left
                else if (newPosition.X < this.Position.X)
                {
                    y = this.Position.Y;

                    while (true)
                    {
                        --x;

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
                        ++x;

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
            return "Q";
        }
    }
}
