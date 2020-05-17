using System;

namespace Chess.GameLogic.Pieces
{
    class Queen : Piece
    {
        public Queen(Position position, ChessColor color, PieceType type) : base(position, color, type) { }

        public override void FindPotentialMoves(Board board)
        {
            // - - - Bishop Moveset - - - //

            int newX = this.position.x;
            int newY = this.position.y;

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

            newX = this.position.x;
            newY = this.position.y;

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

            newX = this.position.x;
            newY = this.position.y;

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

            newX = this.position.x;
            newY = this.position.y;

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

            newX = this.position.x;
            newY = this.position.y;

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

            newY = this.position.y;

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

            newX = this.position.x;
            newY = this.position.y;

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

            newX = this.position.x;

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
            int x = this.position.x;
            int y = this.position.y;

            // - - - Bishop Moveset - - - //

            if (Math.Abs(newPosition.y - this.position.y) == Math.Abs(newPosition.x - this.position.x))
            {
                // Up and Right
                if (newPosition.y > this.position.y && newPosition.x > this.position.x)
                {
                    while (true)
                    {
                        ++x;
                        ++y;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
                // Up and Left
                else if (newPosition.y > this.position.y && newPosition.x < this.position.x)
                {
                    x = this.position.x;
                    y = this.position.y;

                    while (true)
                    {
                        --x;
                        ++y;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
                // Down and Left
                else if (newPosition.y < this.position.y && newPosition.x < this.position.x)
                {
                    x = this.position.x;
                    y = this.position.y;

                    while (true)
                    {
                        --x;
                        --y;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
                // Down and Right
                else if (newPosition.y < this.position.y && newPosition.x > this.position.x)
                {
                    x = this.position.x;
                    y = this.position.y;

                    while (true)
                    {
                        ++x;
                        --y;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
            }
            // - - - Rook Moveset - - - //
            else if (newPosition.x == this.position.x || newPosition.y == this.position.y)
            {
                x = this.position.x;
                y = this.position.y;
                
                // Up
                if (newPosition.y > this.position.y)
                {
                    y = this.position.y;

                    while (true)
                    {
                        ++y;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
                // Down
                else if (newPosition.y < this.position.y)
                {
                    y = this.position.y;

                    while (true)
                    {
                        --y;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
                // Left
                else if (newPosition.x < this.position.x)
                {
                    y = this.position.y;

                    while (true)
                    {
                        --x;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
                // Right
                else
                {
                    x = this.position.x;

                    while (true)
                    {
                        ++x;

                        if (x == newPosition.x && y == newPosition.y)
                        {
                            return true;
                        }
                        else if (board.pieces[x,y] != null)
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }
    }
}
