using System;
namespace Core.Basics.Utils
{
    public struct XRange
    {
        public int Start { get; }
        public int Length { get; }

        public XRange(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public override bool Equals(object? value) =>
            value is XRange r &&
            r.Start.Equals(Start) &&
            r.Length.Equals(Length);

        public bool Equals(XRange other) =>
            other.Start.Equals(Start) &&
            other.Length.Equals(Length);

        public override int GetHashCode() => Start.GetHashCode() * 31 + Length.GetHashCode();

        public override string ToString() => "Range start: " + Start + " | Length: " + Length;
    }
}
