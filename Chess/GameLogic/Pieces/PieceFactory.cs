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
        public static Piece CopyPiece(Piece toCopy)
        {
            Piece copy;

            switch (toCopy.type)
            {
                case PieceType.King:
                    copy = new King(toCopy.position, toCopy.color, toCopy.type);
                    break;
                case PieceType.Queen:
                    copy = new Queen(toCopy.position, toCopy.color, toCopy.type);
                    break;
                case PieceType.Rook:
                    copy = new Rook(toCopy.position, toCopy.color, toCopy.type);
                    break;
                case PieceType.Knight:
                    copy = new Knight(toCopy.position, toCopy.color, toCopy.type);
                    break;
                case PieceType.Bishop:
                    copy = new Bishop(toCopy.position, toCopy.color, toCopy.type);
                    break;
                case PieceType.Pawn:
                    copy = new Pawn(toCopy.position, toCopy.color, toCopy.type);
                    break;
                default:
                    throw new ArgumentException("Invalid Piece Argument");
            }

            copy.hasMoved = toCopy.hasMoved;
            copy.validMoves = toCopy.validMoves;

            return copy;
        }
    }
}