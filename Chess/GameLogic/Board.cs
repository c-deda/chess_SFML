using System.Collections.Generic;

namespace Chess.GameLogic
{
    class Board
    {
        public List<Piece> WhitePieces { get; private set; }
        public List<Piece> BlackPieces { get; private set; }

        public Board()
        {
            // Initialize
            WhitePieces = new List<Piece>();
            BlackPieces = new List<Piece>();
            
            // White Pieces
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(0, 0), ChessColor.White, PieceType.Rook));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(1, 0), ChessColor.White, PieceType.Knight));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(2, 0), ChessColor.White, PieceType.Bishop));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(3, 0), ChessColor.White, PieceType.Queen));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(4, 0), ChessColor.White, PieceType.King));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(5, 0), ChessColor.White, PieceType.Bishop));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(6, 0), ChessColor.White, PieceType.Knight));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(7, 0), ChessColor.White, PieceType.Rook));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(0, 1), ChessColor.White, PieceType.Pawn));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(1, 1), ChessColor.White, PieceType.Pawn));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(2, 1), ChessColor.White, PieceType.Pawn));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(3, 1), ChessColor.White, PieceType.Pawn));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(4, 1), ChessColor.White, PieceType.Pawn));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(5, 1), ChessColor.White, PieceType.Pawn));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(6, 1), ChessColor.White, PieceType.Pawn));
            WhitePieces.Add(PieceFactory.CreatePiece(new Position(7, 1), ChessColor.White, PieceType.Pawn));

            // Black Pieces
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(0, 7), ChessColor.Black, PieceType.Rook));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(1, 7), ChessColor.Black, PieceType.Knight));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(2, 7), ChessColor.Black, PieceType.Bishop));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(3, 7), ChessColor.Black, PieceType.Queen));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(4, 7), ChessColor.Black, PieceType.King));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(5, 7), ChessColor.Black, PieceType.Bishop));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(6, 7), ChessColor.Black, PieceType.Knight));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(7, 7), ChessColor.Black, PieceType.Rook));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(0, 6), ChessColor.Black, PieceType.Pawn));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(1, 6), ChessColor.Black, PieceType.Pawn));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(2, 6), ChessColor.Black, PieceType.Pawn));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(3, 6), ChessColor.Black, PieceType.Pawn));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(4, 6), ChessColor.Black, PieceType.Pawn));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(5, 6), ChessColor.Black, PieceType.Pawn));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(6, 6), ChessColor.Black, PieceType.Pawn));
            BlackPieces.Add(PieceFactory.CreatePiece(new Position(7, 6), ChessColor.Black, PieceType.Pawn));
        }
        public King GetKing(ChessColor player)
        {
            if (player == ChessColor.White)
            {
                foreach (Piece piece in WhitePieces)
                {
                    if (piece.Type == PieceType.King)
                    {
                        return (King)piece;
                    }
                }
            }
            else
            {
                foreach (Piece piece in BlackPieces)
                {
                    if (piece.Type == PieceType.King)
                    {
                        return (King)piece;
                    }
                }
            }

            return null;
        }
        public Piece GetPieceAt(Position position)
        {
            foreach (Piece piece in WhitePieces)
            {
                if (piece.Position == position)
                {
                    return piece;
                }
            }
            foreach (Piece piece in BlackPieces)
            {
                if (piece.Position == position)
                {
                    return piece;
                }
            }

            return null;
        }
        public Piece GetPieceAt(int x, int y)
        {
            return GetPieceAt(new Position(x, y));
        }
        public void RemovePieceAt(Position position)
        {
            foreach (Piece piece in WhitePieces)
            {
                if (piece.Position == position)
                {
                    WhitePieces.Remove(piece);
                    return;
                }
            }
            foreach (Piece piece in BlackPieces)
            {
                if (piece.Position == position)
                {
                    BlackPieces.Remove(piece);
                    return;
                }
            }
        }
        public List<Piece> GetPieceList(ChessColor player)
        {
            if (player == ChessColor.White)
            {
                return WhitePieces;
            }
            else
            {
                return BlackPieces;
            }
        }
        public void AddPieceToBoard(Piece piece)
        {
            if (piece.Color == ChessColor.White)
            {
                WhitePieces.Add(piece);
            }
            else
            {
                BlackPieces.Add(piece);
            }
        }
        public void NormalMove(Position origin, Position destination)
        {
            GetPieceAt(origin).UpdatePosition(destination);
        }
        public void CaptureMove(Position origin, Position destination)
        {
            RemovePieceAt(destination);
            GetPieceAt(origin).UpdatePosition(destination);
        }
        public void EnPassantMove(Position origin, Position destination, Piece captured)
        {
            GetPieceAt(origin).UpdatePosition(destination);
            RemovePieceAt(captured.Position);
        }
        public void CastleMove(Position kingOrigin, Position kingDestination, Position rookOrigin, Position rookDestination)
        {
            GetPieceAt(kingOrigin).UpdatePosition(kingDestination);
            GetPieceAt(rookOrigin).UpdatePosition(rookDestination);
        }
        public void NormalUndo(Piece moved, Position origin, Position destination)
        {
            RemovePieceAt(destination);
            AddPieceToBoard(PieceFactory.CopyPiece(moved));
        }
        public void CaptureUndo(Piece moved, Piece captured, Position destination)
        {
            RemovePieceAt(destination);
            AddPieceToBoard(PieceFactory.CopyPiece(moved));
            AddPieceToBoard(PieceFactory.CopyPiece(captured));
        }
        public void CastleUndo(Piece king, Piece rook, Position kingDestination, Position rookDestination)
        {
            RemovePieceAt(kingDestination);
            RemovePieceAt(rookDestination);
            AddPieceToBoard(PieceFactory.CopyPiece(king));
            AddPieceToBoard(PieceFactory.CopyPiece(rook));
        }
    }
}
