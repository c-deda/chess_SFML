using Chess.GameLogic.Pieces;
using System.Collections.Generic;
using System;

namespace Chess.GameLogic
{
    class Game
    {
        public Board board { get; private set; }
        public ChessColor currentTurn { get; private set; }
        public LinkedList<Move> pastMoves { get; private set; }
        public bool gameOver { get; private set; }
        public int turnCounter { get; private set; }

        public event EventHandler<EventArgs> BoardStateChanged;

        public Game()
        {
            board = new Board();
            currentTurn = ChessColor.White;
            pastMoves = new LinkedList<Move>();
            gameOver = false;
            turnCounter = 1;
            FindValidMoves();
        }
        public void FindValidMoves()
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (board.pieces[x,y] != null)
                    {
                        board.pieces[x,y].FindPotentialMoves(board);

                        switch (board.pieces[x,y].type)
                        {
                            case PieceType.Pawn:
                                CheckPawnCaptures(board.pieces[x,y]);
                                break;
                            case PieceType.King:
                            case PieceType.Rook:
                                // Check Castling
                                break;
                        }
                        // Check If King is Checked
                    }
                }
            }
        }
        private void CheckPawnCaptures(Piece pawn)
        {
            List<Move> movesToRemove = new List<Move>();
            EnPassantMove enPassant = null;

            foreach (Move move in pawn.validMoves)
            {
                if (move.destination.x != pawn.position.x)
                {
                    // No Past Moves Means It Can't Be En Passant Or Capture
                    if (pastMoves.Count == 0)
                    {
                        movesToRemove.Add(move);
                    }
                    // Check For En Passant
                    else if (board.pieces[move.destination.x,move.destination.y] == null)
                    {
                        Move lastMove = pastMoves.Last.Value;

                        // Last Piece Moved Isn't A Pawn Or It Had Been Previously Moved
                        if (lastMove.moved.type != PieceType.Pawn || lastMove.moved.hasMoved)
                        {
                            movesToRemove.Add(move);
                        }
                        // Last Piece Moved Isn't In Correct Position For En Passant
                        else if (lastMove.destination.x != move.destination.x || 
                                 lastMove.destination.y != move.origin.y)
                        {
                            movesToRemove.Add(move);
                        }
                        // Move Is En Passant
                        else
                        {
                            movesToRemove.Add(move);
                            enPassant = new EnPassantMove(move.moved, move.origin, move.destination, board.pieces[move.destination.x,move.origin.y]);
                        }
                    }
                    // Can't Attack Own Piece
                    else if (board.pieces[move.destination.x,move.destination.y].color == pawn.color)
                    {
                        movesToRemove.Add(move);
                    }
                }
            }
            // Remove Illegal Moves
            foreach (Move move in movesToRemove)
            {
                pawn.validMoves.Remove(move);
            }
            
            // Add En Passant If Exists
            if (enPassant != null)
            {
                pawn.validMoves.Add(enPassant);
            }
        }
        public void CheckCastling(Piece piece)
        {
            // White
            if (piece.color == ChessColor.White)
            {

            }
        }
        public void ExecuteMove(Move move)
        {
            move.Execute(board);
            pastMoves.AddLast(move);
            // Check Pawn Promotion
            OnBoardStateChanged();
            ChangeTurns();
        }
        private void ChangeTurns()
        {
            turnCounter++;

            if (currentTurn == ChessColor.White)
            {
                currentTurn = ChessColor.Black;
            }
            else
            {
                currentTurn = ChessColor.White;
            }

            CheckKingChecked(currentTurn);
            CheckGameOver();
        }
        private void CheckKingChecked(ChessColor player)
        {
            King playerKing;

            if (player == ChessColor.White)
            {
                playerKing = board.whiteKing;
            }
            else
            {
                playerKing = board.blackKing;
            }

            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (board.pieces[x,y] != null)
                    {
                        if (board.pieces[x,y].color != player && board.pieces[x,y].CanAttack(board, playerKing.position))
                        {
                            playerKing.isChecked = true;
                            break;
                        }
                    }
                }
            }
        }
        private void CheckGameOver()
        {
            bool whiteMoves = false;
            bool blackMoves = false;

            FindValidMoves();

            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (whiteMoves && blackMoves)
                    {
                        break;
                    }
                    if (board.pieces[x,y] != null)
                    {
                        if (board.pieces[x,y].validMoves.Count > 0)
                        {
                            if (board.pieces[x,y].color == ChessColor.White)
                            {
                                whiteMoves = true;
                            }
                            else
                            {
                                blackMoves = true;
                            }
                        }
                    }
                }
            }

            if (!whiteMoves)
            {
                if (board.whiteKing.isChecked)
                {
                    System.Console.WriteLine("Black Wins");
                }
                else
                {
                    System.Console.WriteLine("Stalemate");
                }

                gameOver = true;
            }
            else if (!blackMoves)
            {
                if (board.blackKing.isChecked)
                {
                    System.Console.WriteLine("White Wins");
                }
                else
                {
                    System.Console.WriteLine("Stalemate");
                }

                gameOver = true;
            }
        }
        protected virtual void OnBoardStateChanged()
        {
            if (BoardStateChanged != null)
            {
                BoardStateChanged(this, new EventArgs());
            }
        }
    }
}