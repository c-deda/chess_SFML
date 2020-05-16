using System;

namespace Chess.GameLogic.Pieces
{
    static class PieceFactory
    {
        public static Piece CreatePiece(Position position, ChessColor color, PieceType type)
        {
            switch (type)
            {
                case PieceType.King:
                    return new King(position, color, type);
                case PieceType.Queen:
                    return new Queen(position, color, type);
                case PieceType.Rook:
                    return new Rook(position, color, type);
                case PieceType.Knight:
                    return new Knight(position, color, type);
                case PieceType.Bishop:
                    return new Bishop(position, color, type);
                case PieceType.Pawn:
                    return new Pawn(position, color, type);
                default:
                    throw new ArgumentException("Invalid Piece Argument");
            }
        }
        public static King CreateKing(Position position, ChessColor color)
        {
            return new King(position, color, PieceType.King);
        }
    }
}