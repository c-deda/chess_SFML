using System;

namespace Chess.GameLogic
{
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public static bool operator ==(Position a, Position b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }
        public static bool operator !=(Position a, Position b)
        {
            return (a.X != b.X) || (a.Y != b.Y);
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Position p = (Position)obj;
                return (this.X == p.X) && (this.Y == p.Y);
            }
        }
        public override int GetHashCode()
        {
            return ShiftAndWrap(X.GetHashCode(), 2) ^ Y.GetHashCode();
        }
        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;

            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            uint wrapped = number >> (32 - positions);

            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }
    }
}