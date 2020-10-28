using System.Collections.Generic;
using System;

namespace Chess.GameLogic
{
    class Game
    {
        public Board Board { get; private set; }
        public ChessColor CurrentPlayer { get; private set; }
        public Stack<Move> PastMoves { get; private set; }
        public int TurnCounter { get; private set; }
        public event EventHandler<GameEventArgs> GameEvent;

        public Game()
        {
            Board = new Board();
            CurrentPlayer = ChessColor.White;
            PastMoves = new Stack<Move>();
            TurnCounter = 1;
            FindValidMoves(ChessColor.White);
        }
        public void IncrementTurn()
        {
            ++TurnCounter;
        }
        private void FindValidMoves(ChessColor player)
        {
            List<Move> allPotentialMoves = new List<Move>();
            foreach (Piece piece in Board.GetPieceList(player))
            {
                piece.ClearValidMoves();
                piece.FindPotentialMoves(Board);
                allPotentialMoves.AddRange(piece.ValidMoves);
            }

            List<Move> movesToRemove = new List<Move>();
            List<Move> movesToAdd = new List<Move>();
            foreach (Move move in allPotentialMoves)
            {
                SpecialMoveValidityChecks(move, movesToRemove, movesToAdd);
            }

            allPotentialMoves.AddRange(movesToAdd);
            foreach (Move move in movesToRemove)
            {
                allPotentialMoves.Remove(move);
            }
            
            foreach (Move move in allPotentialMoves)
            {
                NormalMoveValidityCheck(move);
            }
        }
        private void NormalMoveValidityCheck(Move move)
        {
            SimulateMove(move);
            if (IsKingInCheck(move.Moved.Color))
            {
                Board.GetPieceAt(move.Destination).ValidMoves.Remove(move);
            }
            UndoSimulateMove(move);
        }
        private void SpecialMoveValidityChecks(Move move, List<Move> movesToRemove, List<Move> movesToAdd)
        {
            switch (move.Moved.Type)
            {
                case PieceType.Pawn:
                    CheckPawnCaptures(move, movesToRemove, movesToAdd);
                    break;
                case PieceType.King:
                    CheckCastling(move, movesToRemove, movesToAdd);
                    break;
            }
        }
        private void CheckPawnCaptures(Move move, List<Move> movesToRemove, List<Move> movesToAdd)
        {
            // Check For Capture
            if (move.Destination.X != move.Origin.X)
            {
                // No Past Moves Means It Can't Be En Passant Or Capture
                if (PastMoves.Count == 0)
                {
                    movesToRemove.Add(move);
                    Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);
                }
                // Check For En Passant
                else if (Board.GetPieceAt(move.Destination) == null)
                {
                    Move lastMove = PastMoves.Peek();

                    // Last Piece Moved Isn't A Pawn Or It Had Been Previously Moved
                    if (lastMove.Moved.Type != PieceType.Pawn || lastMove.Moved.HasMoved)
                    {
                        movesToRemove.Add(move);
                        Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);
                    }
                    // Last Piece Moved Isn't In Correct Position For En Passant
                    else if (lastMove.Destination.X != move.Destination.X || 
                             lastMove.Destination.Y != move.Origin.Y)
                    {
                        movesToRemove.Add(move);
                        Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);
                    }
                    // Move Is En Passant
                    else
                    {
                        movesToRemove.Add(move);
                        Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);

                        Move enpassant = new EnPassantMove(move.Moved, move.Origin, move.Destination, 
                                                           Board.GetPieceAt(new Position(move.Destination.X,move.Origin.Y)));
                        movesToAdd.Add(enpassant);
                        Board.GetPieceAt(enpassant.Origin).ValidMoves.Add(enpassant);
                    }
                }
                // Can't Attack Own Piece
                else if (Board.GetPieceAt(move.Destination).Color == move.Moved.Color)
                {
                    movesToRemove.Add(move);
                    Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);
                }
            }
        }
        private void CheckCastling(Move move, List<Move> movesToRemove, List<Move> movesToAdd)
        {
            // If King Tries To Move 2 Spaces Left Or Right, Check For Castling
            if (Math.Abs(move.Destination.X - move.Origin.X) == 2 &&
               (move.Destination.Y == move.Origin.Y))
            {
                if (!move.Moved.HasMoved && !IsKingInCheck(move.Moved.Color))
                {
                    Piece rookSquare;
                    Piece middleSquare;
                    Position middleSquarePosition;
                    Piece queenSideSquare = null;
                    bool isKingSide = move.Destination.X > move.Origin.X;

                    // King Side Castle
                    if (isKingSide)
                    {
                        rookSquare = Board.GetPieceAt(7, move.Moved.Position.Y);
                        middleSquarePosition = new Position(5, move.Moved.Position.Y);
                        middleSquare = Board.GetPieceAt(middleSquarePosition);
                    }
                    // Queen Side Castle
                    else
                    {
                        rookSquare = Board.GetPieceAt(0, move.Moved.Position.Y);
                        middleSquarePosition = new Position(3, move.Moved.Position.Y);
                        middleSquare = Board.GetPieceAt(middleSquarePosition);
                    }

                    // Make Sure Rook Hasn't Moved And Middle Square Is Empty And Isn't Attacked
                    if ((rookSquare != null && !rookSquare.HasMoved) && (middleSquare == null && 
                        (!IsSquareAttacked(move.Moved.Color, middleSquarePosition))))
                    {
                        // If It Happens To Be Queen Side Make Sure Queen Side Square Is Empty
                        if (!isKingSide)
                        {
                            if (queenSideSquare != null)
                            {
                                movesToRemove.Add(move);
                                Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);
                                return;
                            }
                        }
                        // Move Is Castle
                        movesToRemove.Add(move);
                        Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);

                        Move castle = new CastleMove(move.Moved, move.Origin, move.Destination, 
                                                     rookSquare, rookSquare.Position, middleSquarePosition);
                        movesToAdd.Add(castle);
                        Board.GetPieceAt(castle.Origin).ValidMoves.Add(castle);
                        
                    }
                    // Move Is Illegal
                    else
                    {
                        movesToRemove.Add(move);
                        Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);
                    }
                }
                // Move Is Illegal
                else
                {
                    movesToRemove.Add(move);
                    Board.GetPieceAt(move.Origin).ValidMoves.Remove(move);
                }
            }
        }
        public void ExecuteMove(Move move)
        {
            move.Execute(Board);
            PastMoves.Push(move);
            CheckPawnPromotion();
            ChangeTurns();
        }
        private void SimulateMove(Move move)
        {
            move.Execute(Board);
        }
        private void UndoSimulateMove(Move move)
        {
            move.Undo(Board);
        }
        private void ChangeTurns()
        {
            switch (CurrentPlayer)
            {
                case ChessColor.White:
                    CurrentPlayer = ChessColor.Black;
                    break;
                case ChessColor.Black:
                    CurrentPlayer = ChessColor.White;
                    break;
            }

            CheckGameOver();
        }
        private bool IsKingInCheck(ChessColor player)
        {
            King playerKing = Board.GetKing(player);

            return IsSquareAttacked(player, playerKing.Position);
        }
        private bool IsSquareAttacked(ChessColor attackedPlayer, Position square)
        {
            List<Piece> attackingPieces;
            if (attackedPlayer == ChessColor.White)
            {
                attackingPieces = Board.BlackPieces;
            }
            else
            {
                attackingPieces = Board.WhitePieces;
            }
            foreach (Piece piece in attackingPieces)
            {
                if (piece.CanAttack(Board, square))
                {
                    return true;
                }
            }

            return false;
        }
        // TODO: Create UI Popup To Allow Player To Select Promotion Type
        private void CheckPawnPromotion()
        {
            int y;
            if (CurrentPlayer == ChessColor.White)
            {
                // Promotion Row For White
                y = 7;        
            }
            else
            {
                // Promotion Row For Black
                y = 0;
            }

            for (int x = 0; x < GlobalConstants.BoardLength; ++x)
            {
                if (Board.GetPieceAt(x,y) != null && Board.GetPieceAt(x,y).Type == PieceType.Pawn)
                {
                    Board.GetPieceList(CurrentPlayer).Remove(Board.GetPieceAt(x,y));
                    Board.GetPieceList(CurrentPlayer).Add(PieceFactory.CreatePiece(new Position(x,y), CurrentPlayer, PieceType.Queen));
                }
            }
        }
        private void CheckGameOver()
        {
            FindValidMoves(CurrentPlayer);

            // No Moves Means The Game Is Over
            if (!PlayerHasMoves(CurrentPlayer))
            {
                // Checkmate
                if (IsKingInCheck(CurrentPlayer))
                {
                    switch (CurrentPlayer)
                    {
                        case ChessColor.White:
                            OnGameEvent(GameState.WhiteMated);
                            break;
                        case ChessColor.Black:
                            OnGameEvent(GameState.BlackMated);
                            break;
                    }
                }
                // Otherwise It's A Stalemate
                else
                {
                    switch (CurrentPlayer)
                    {
                        case ChessColor.White:
                            OnGameEvent(GameState.WhiteStalemate);
                            break;
                        case ChessColor.Black:
                            OnGameEvent(GameState.BlackStalemate);
                            break;
                    }
                    
                }
            }
            // Piece Is In Check
            else if (IsKingInCheck(CurrentPlayer))
            {
                switch (CurrentPlayer)
                {
                    case ChessColor.White:
                        OnGameEvent(GameState.WhiteInCheck);
                        break;
                    case ChessColor.Black:
                        OnGameEvent(GameState.BlackInCheck);
                        break;
                }
            }
            // Normal Turn Change
            else
            {
                switch (CurrentPlayer)
                {
                    case ChessColor.White:
                        OnGameEvent(GameState.WhiteTurn);
                        break;
                    case ChessColor.Black:
                        OnGameEvent(GameState.BlackTurn);
                        break;
                }
            }
        }
        public bool PlayerHasMoves(ChessColor player)
        {
            foreach (Piece piece in Board.GetPieceList(player))
            {
                if (piece.ValidMoves.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
        protected virtual void OnGameEvent(GameState state)
        {
            if (GameEvent != null)
            {
                GameEvent(this, new GameEventArgs() { State = state } );
            }
        }
    }

    public class GameEventArgs : EventArgs
    {
        public GameState State { get; set; }
    }

    public enum GameState
    {
        WhiteTurn,
        BlackTurn,
        WhiteInCheck,
        BlackInCheck,
        WhiteMated,
        BlackMated,
        WhiteStalemate,
        BlackStalemate
    }
}