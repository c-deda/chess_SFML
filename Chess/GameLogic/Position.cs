using System;

namespace Chess.GameLogic
{
    public struct Position
    {
        public int x { get; set; }
        public int y { get; set; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static bool operator ==(Position a, Position b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }
        public static bool operator !=(Position a, Position b)
        {
            return (a.x != b.x) || (a.y != b.y);
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
                return (this.x == p.x) && (this.y == p.y);
            }
        }
        public override int GetHashCode()
        {
            return ShiftAndWrap(x.GetHashCode(), 2) ^ y.GetHashCode();
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