using System;

namespace Chess.GameLogic
{
    class Move
    {
        public Piece Moved { get; private set; }
        public Position Origin { get; private set; }
        public Position Destination { get; private set; }
        public int Value { get; set; }

        public Move(Piece moved, Position origin, Position destination)
        {
            this.Moved = PieceFactory.CopyPiece(moved);
            this.Origin = origin;
            this.Destination = destination;
        }
        public virtual void Execute(Board board)
        {
            board.NormalMove(Origin, Destination);
        }
        public virtual void Undo(Board board)
        {
            board.NormalUndo(Moved, Origin, Destination);
        }
        public override string ToString()
        {
            return Moved.ToString() + GetTileString(Destination);
        }
        public string GetTileString(Position position)
        {
            return GetColString(position.X) + GetRowString(position.Y);
        }
        public string GetColString(int x)
        {
            switch (x)
            {
                case 0:
                    return "a";
                case 1:
                    return "b";
                case 2:
                    return "c";
                case 3:
                    return "d";
                case 4:
                    return "e";
                case 5:
                    return "f";
                case 6:
                    return "g";
                case 7:
                    return "h";
                default:
                    throw new ArgumentException("Impossible Column!");
            }
        }
        private string GetRowString(int y)
        {
            switch (y)
            {
                case 0:
                    return "1";
                case 1:
                    return "2";
                case 2:
                    return "3";
                case 3:
                    return "4";
                case 4:
                    return "5";
                case 5:
                    return "6";
                case 6:
                    return "7";
                case 7:
                    return "8";
                default:
                    throw new ArgumentException("Impossible Row!");
            }
        }
    }
}