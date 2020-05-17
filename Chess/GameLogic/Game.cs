using Chess.GameLogic.Pieces;
using System.Collections.Generic;
using System;

namespace Chess.GameLogic
{
    class Game
    {
        public Board board { get; private set; }
        public ChessColor currentTurn { get; private set; }
        public Stack<Move> pastMoves { get; private set; }
        private Dictionary<ChessColor, bool> kingChecked;
        public bool gameOver { get; private set; }
        public int turnCounter { get; private set; }

        public event EventHandler<EventArgs> BoardStateChanged;

        public Game()
        {
            board = new Board();
            currentTurn = ChessColor.White;
            pastMoves = new Stack<Move>();
            kingChecked = new Dictionary<ChessColor, bool>() {
                [ChessColor.White] = false,
                [ChessColor.Black] = false
            };
            gameOver = false;
            turnCounter = 1;
            FindValidMoves(ChessColor.White);
        }
        public void FindValidMoves(ChessColor player)
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (board.pieces[x,y] != null && board.pieces[x,y].color == player)
                    {
                        List<Move> movesToRemove = new List<Move>();

                        board.pieces[x,y].ClearValidMoves();
                        board.pieces[x,y].FindPotentialMoves(board);
                                
                        // Moves Are Invalid If King Is In Check
                        foreach (Move move in board.pieces[x,y].validMoves)
                        {
                            SimulateMove(move);
                            if (IsKingChecked(player))
                            {
                                movesToRemove.Add(move);
                            }
                            UndoSimulateMove(move);
                        }
                        // Remove Invalid Moves
                        foreach (Move move in movesToRemove)
                        {
                            board.pieces[x,y].validMoves.Remove(move);
                        }
                        // Special Validity Checks
                        switch (board.pieces[x,y].type)
                        {
                            case PieceType.Pawn:
                                CheckPawnCaptures(board.pieces[x,y]);
                                break;
                            case PieceType.King:
                                CheckCastling(board.pieces[x,y]);
                                break;
                        }
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
                // Check For Capture
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
                        Move lastMove = pastMoves.Peek();

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
        public void CheckCastling(Piece king)
        {
            List<Move> movesToRemove = new List<Move>();
            List<CastleMove> castleMoves = new List<CastleMove>();

            foreach (Move move in king.validMoves)
            {
                // If King Tries To Move 2 Spaces Left Or Right, Check For Castling
                if (Math.Abs(move.destination.x - move.origin.x) == 2 &&
                   (move.destination.y == move.origin.y))
                {
                    if (!king.hasMoved && !kingChecked[king.color])
                    {
                        Position rook;
                        Position middleSquare;
                        Position queenSideSquare = new Position(-1, -1);

                        // King Side Castle
                        if (move.destination.x > move.origin.x)
                        {
                            rook = new Position(7, king.position.y);
                            middleSquare = new Position(5, king.position.y);
                        }
                        // Queen Side Castle
                        else
                        {
                            rook = new Position(0, king.position.y);
                            middleSquare = new Position(3, king.position.y);
                            queenSideSquare = new Position(1, king.position.y);
                        }

                        // Make Sure Rook Hasn't Moved And Middle Square Is Empty And Isn't Attacked
                        if ((!board.pieces[rook.x, rook.y].hasMoved) && (board.pieces[middleSquare.x, middleSquare.y] == null) &&
                           (!IsSquareAttacked(king.color, middleSquare)))
                        {
                            // If It Happens To Be Queen Side
                            if (queenSideSquare.x == 1)
                            {
                                // Make Sure Queen Side Square Is Empty
                                if (board.pieces[queenSideSquare.x, queenSideSquare.y] != null)
                                {
                                    movesToRemove.Add(move);
                                    continue;
                                }
                            }
                            // Move Is Castle
                            castleMoves.Add(new CastleMove(king, move.origin, move.destination, board.pieces[rook.x, rook.y], rook, middleSquare));
                            movesToRemove.Add(move);
                        }
                        else
                        {
                            movesToRemove.Add(move);
                        }
                    }
                    else
                    {
                        movesToRemove.Add(move);
                    }
                }
            }
            foreach (Move move in movesToRemove)
            {
                king.validMoves.Remove(move);
            }
            foreach (CastleMove move in castleMoves)
            {
                king.validMoves.Add(move);
            }
        }
        public void ExecuteMove(Move move)
        {
            move.Execute(board);
            pastMoves.Push(move);
            CheckPawnPromotion();
            ChangeTurns();
        }
        public void SimulateMove(Move move)
        {
            move.Execute(board);
        }
        public void UndoSimulateMove(Move move)
        {
            move.Undo(board);
        }
        private void ChangeTurns()
        {
            if (currentTurn == ChessColor.White)
            {
                currentTurn = ChessColor.Black;
            }
            else
            {
                currentTurn = ChessColor.White;
                turnCounter++;
            }

            if (IsKingChecked(currentTurn))
            {
                kingChecked[currentTurn] = true;
            }
            else
            {
                kingChecked[currentTurn] = false;
            }

            CheckGameOver();
        }
        private bool IsKingChecked(ChessColor player)
        {
            King playerKing = board.GetKing(player);

            return IsSquareAttacked(player, playerKing.position);
        }
        private bool IsSquareAttacked(ChessColor player, Position square)
        {
            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (board.pieces[x,y] != null)
                    {
                        if (board.pieces[x,y].color != player && board.pieces[x,y].CanAttack(board, square))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        // TODO: Create UI Popup To Allow Player To Select Promotion Type
        private void CheckPawnPromotion()
        {
            for (int x = 0; x < GlobalConstants.BoardLength; ++x)
            {
                // Black Promotion
                if (board.pieces[x,0] != null && board.pieces[x,0].type == PieceType.Pawn)
                {
                    board.pieces[x,0] = PieceFactory.CreatePiece(new Position(x,0), ChessColor.Black, PieceType.Queen);
                }
                // White Promotion
                else if (board.pieces[x,7] != null && board.pieces[x,7].type == PieceType.Pawn)
                {
                    board.pieces[x,7] = PieceFactory.CreatePiece(new Position(x,7), ChessColor.White, PieceType.Queen);
                }
            }
        }
        private void CheckGameOver()
        {
            bool whiteMoves = false;
            bool blackMoves = false;

            FindValidMoves(ChessColor.White);
            FindValidMoves(ChessColor.Black);

            for (int y = 0; y < GlobalConstants.BoardLength; ++y)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; ++x)
                {
                    if (whiteMoves && blackMoves)
                    {
                        break;
                    }
                    else if (board.pieces[x,y] != null)
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
                if (kingChecked[ChessColor.White])
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
                if (kingChecked[ChessColor.Black])
                {
                    System.Console.WriteLine("White Wins");
                }
                else
                {
                    System.Console.WriteLine("Stalemate");
                }

                gameOver = true;
            }

            OnBoardStateChanged();
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