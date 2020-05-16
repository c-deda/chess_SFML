using Chess.GameLogic.Pieces;

namespace Chess.GameLogic
{
    class Board
    {
        public Piece[,] pieces { get; private set; }
        public King whiteKing { get; private set; }
        public King blackKing { get; private set; }

        public Board()
        {
            // Initialize
            pieces = new Piece[GlobalConstants.BoardLength, GlobalConstants.BoardLength];
            
            // White Pieces
            whiteKing = PieceFactory.CreateKing(new Position(4, 0), ChessColor.White);
            pieces[0,0] = PieceFactory.CreatePiece(new Position(0, 0), ChessColor.White, PieceType.Rook);
            pieces[1,0] = PieceFactory.CreatePiece(new Position(1, 0), ChessColor.White, PieceType.Knight);
            pieces[2,0] = PieceFactory.CreatePiece(new Position(2, 0), ChessColor.White, PieceType.Bishop);
            pieces[3,0] = PieceFactory.CreatePiece(new Position(3, 0), ChessColor.White, PieceType.Queen);
            pieces[4,0] = whiteKing;
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
            blackKing = PieceFactory.CreateKing(new Position(4, 7), ChessColor.Black);
            pieces[0,7] = PieceFactory.CreatePiece(new Position(0, 7), ChessColor.Black, PieceType.Rook);
            pieces[1,7] = PieceFactory.CreatePiece(new Position(1, 7), ChessColor.Black, PieceType.Knight);
            pieces[2,7] = PieceFactory.CreatePiece(new Position(2, 7), ChessColor.Black, PieceType.Bishop);
            pieces[3,7] = PieceFactory.CreatePiece(new Position(3, 7), ChessColor.Black, PieceType.Queen);
            pieces[4,7] = blackKing;
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
        public void NormalMove(Position origin, Position destination)
        {
            pieces[origin.x, origin.y].Move(destination);
            pieces[destination.x, destination.y] = pieces[origin.x, origin.y];
            pieces[origin.x, origin.y] = null;
        }
        public void EnPassantMove(Position origin, Position destination, Piece captured)
        {
            pieces[origin.x,origin.y].Move(destination);
            pieces[destination.x,destination.y] = pieces[origin.x,origin.y];
            pieces[origin.x, origin.y] = null;
            pieces[captured.position.x,captured.position.y] = null;
        }
        public void NormalUndo(Piece moved)
        {
            
        }
        public void CaptureUndo(Piece moved, Piece captured)
        {

        }
        public void EnPassantUndo(Piece moved, Piece captured, Position destination)
        {

        }
    }
}
