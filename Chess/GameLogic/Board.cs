using Chess.GameLogic.Pieces;

namespace Chess.GameLogic
{
    class Board
    {
        public Piece[,] pieces { get; private set; }

        public Board()
        {
            // Initialize
            pieces = new Piece[GlobalConstants.BoardLength, GlobalConstants.BoardLength];
            
            // White Pieces
            pieces[0,0] = PieceFactory.CreatePiece(new Position(0, 0), ChessColor.White, PieceType.Rook);
            pieces[1,0] = PieceFactory.CreatePiece(new Position(1, 0), ChessColor.White, PieceType.Knight);
            pieces[2,0] = PieceFactory.CreatePiece(new Position(2, 0), ChessColor.White, PieceType.Bishop);
            pieces[3,0] = PieceFactory.CreatePiece(new Position(3, 0), ChessColor.White, PieceType.Queen);
            pieces[4,0] = PieceFactory.CreatePiece(new Position(4, 0), ChessColor.White, PieceType.King);
            pieces[5,0] = PieceFactory.CreatePiece(new Position(5, 0), ChessColor.White, PieceType.Bishop);
            pieces[6,0] = PieceFactory.CreatePiece(new Position(6, 0), ChessColor.White, PieceType.Knight);
            pieces[7,0] = PieceFactory.CreatePiece(new Position(7, 0), ChessColor.White, PieceType.Rook);
            pieces[0,1] = PieceFactory.CreatePiece(new Position(0, 1), ChessColor.White, PieceType.Pawn);
            pieces[1,1] = PieceFactory.CreatePiece(new Position(1, 1), ChessColor.White, PieceType.Pawn);
            pieces[2,1] = PieceFactory.CreatePiece(new Position(2, 1), ChessColor.White, PieceType.Pawn);
            pieces[3,1] = PieceFactory.CreatePiece(new Position(3, 1), ChessColor.White, PieceType.Pawn);
            pieces[4,1] = PieceFactory.CreatePiece(new Position(4, 1), ChessColor.White, PieceType.Pawn);
            pieces[5,1] = PieceFactory.CreatePiece(new Position(5, 1), ChessColor.White, PieceType.Pawn);
            pieces[6,1] = PieceFactory.CreatePiece(new Position(6, 1), ChessColor.White, PieceType.Pawn);
            pieces[7,1] = PieceFactory.CreatePiece(new Position(7, 1), ChessColor.White, PieceType.Pawn);

            // Black Pieces
            pieces[0,7] = PieceFactory.CreatePiece(new Position(0, 7), ChessColor.Black, PieceType.Rook);
            pieces[1,7] = PieceFactory.CreatePiece(new Position(1, 7), ChessColor.Black, PieceType.Knight);
            pieces[2,7] = PieceFactory.CreatePiece(new Position(2, 7), ChessColor.Black, PieceType.Bishop);
            pieces[3,7] = PieceFactory.CreatePiece(new Position(3, 7), ChessColor.Black, PieceType.Queen);
            pieces[4,7] = PieceFactory.CreatePiece(new Position(4, 7), ChessColor.Black, PieceType.King);
            pieces[5,7] = PieceFactory.CreatePiece(new Position(5, 7), ChessColor.Black, PieceType.Bishop);
            pieces[6,7] = PieceFactory.CreatePiece(new Position(6, 7), ChessColor.Black, PieceType.Knight);
            pieces[7,7] = PieceFactory.CreatePiece(new Position(7, 7), ChessColor.Black, PieceType.Rook);
            pieces[0,6] = PieceFactory.CreatePiece(new Position(0, 6), ChessColor.Black, PieceType.Pawn);
            pieces[1,6] = PieceFactory.CreatePiece(new Position(1, 6), ChessColor.Black, PieceType.Pawn);
            pieces[2,6] = PieceFactory.CreatePiece(new Position(2, 6), ChessColor.Black, PieceType.Pawn);
            pieces[3,6] = PieceFactory.CreatePiece(new Position(3, 6), ChessColor.Black, PieceType.Pawn);
            pieces[4,6] = PieceFactory.CreatePiece(new Position(4, 6), ChessColor.Black, PieceType.Pawn);
            pieces[5,6] = PieceFactory.CreatePiece(new Position(5, 6), ChessColor.Black, PieceType.Pawn);
            pieces[6,6] = PieceFactory.CreatePiece(new Position(6, 6), ChessColor.Black, PieceType.Pawn);
            pieces[7,6] = PieceFactory.CreatePiece(new Position(7, 6), ChessColor.Black, PieceType.Pawn);
        }
        public King GetKing(ChessColor player)
        {
            for (int y = 0; y < GlobalConstants.BoardLength; y++)
            {
                for (int x = 0; x < GlobalConstants.BoardLength; x++)
                {
                    if (pieces[x,y] != null && (pieces[x,y].type == PieceType.King && pieces[x,y].color == player))
                    {
                        return (King)pieces[x,y];
                    }
                }
            }

            return null;
        }
        public void NormalMove(Position origin, Position destination)
        {
            pieces[origin.x, origin.y].Move(destination);
            pieces[destination.x, destination.y] = pieces[origin.x, origin.y];
            pieces[origin.x, origin.y] = null;
        }
        public void EnPassantMove(Position origin, Position destination, Piece captured)
        {
            pieces[origin.x, origin.y].Move(destination);
            pieces[destination.x, destination.y] = pieces[origin.x,origin.y];
            pieces[origin.x, origin.y] = null;
            pieces[captured.position.x, captured.position.y] = null;
        }
        public void CastleMove(Position kingOrigin, Position kingDestination, Position rookOrigin, Position rookDestination)
        {
            pieces[kingOrigin.x, kingOrigin.y].Move(kingDestination);
            pieces[rookOrigin.x, rookOrigin.y].Move(rookDestination);
            pieces[kingDestination.x, kingDestination.y] = pieces[kingOrigin.x, kingOrigin.y];
            pieces[rookDestination.x, rookDestination.y] = pieces[rookOrigin.x, rookOrigin.y];
            pieces[kingOrigin.x, kingOrigin.y] = null;
            pieces[rookOrigin.x, rookOrigin.y] = null;
        }
        public void NormalUndo(Piece moved, Position destination)
        {
            pieces[moved.position.x, moved.position.y] = PieceFactory.CopyPiece(moved);
            pieces[destination.x, destination.y] = null;
        }
        public void CaptureUndo(Piece moved, Piece captured)
        {
            pieces[moved.position.x, moved.position.y] = PieceFactory.CopyPiece(moved);
            pieces[captured.position.x, captured.position.y] = PieceFactory.CopyPiece(captured);
        }
        public void EnPassantUndo(Piece moved, Piece captured, Position destination)
        {
            pieces[moved.position.x, moved.position.y] = PieceFactory.CopyPiece(moved);
            pieces[destination.x, destination.y] = null;
            pieces[captured.position.x, captured.position.y] = PieceFactory.CopyPiece(captured);
        }
        public void CastleUndo(Piece king, Piece rook, Position kingDestination, Position rookDestination)
        {
            pieces[king.position.x, king.position.y] = PieceFactory.CopyPiece(king);
            pieces[rook.position.x, rook.position.y] = PieceFactory.CopyPiece(rook);
            pieces[kingDestination.x, kingDestination.y] = null;
            pieces[rookDestination.x, rookDestination.y] = null;
        }
    }
}
