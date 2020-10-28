using System;

namespace Chess.GameLogic
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
        public static Piece CopyPiece(Piece toCopy)
        {
            Piece copy;

            switch (toCopy.Type)
            {
                case PieceType.King:
                    copy = new King(toCopy.Position, toCopy.Color, toCopy.Type);
                    break;
                case PieceType.Queen:
                    copy = new Queen(toCopy.Position, toCopy.Color, toCopy.Type);
                    break;
                case PieceType.Rook:
                    copy = new Rook(toCopy.Position, toCopy.Color, toCopy.Type);
                    break;
                case PieceType.Knight:
                    copy = new Knight(toCopy.Position, toCopy.Color, toCopy.Type);
                    break;
                case PieceType.Bishop:
                    copy = new Bishop(toCopy.Position, toCopy.Color, toCopy.Type);
                    break;
                case PieceType.Pawn:
                    copy = new Pawn(toCopy.Position, toCopy.Color, toCopy.Type);
                    break;
                default:
                    throw new ArgumentException("Invalid Piece Argument");
            }

            copy.HasMoved = toCopy.HasMoved;
            copy.ValidMoves = toCopy.ValidMoves;

            return copy;
        }
    }
}