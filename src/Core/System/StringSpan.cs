using System;

namespace Pocket.Common
{
    public struct StringSpan : IEquatable<StringSpan>
    {
        public StringSpan(string source) : this(source, 0, source.Length) { }
        public StringSpan(string source, int offset, int length)
        {
            Source = source;
            Offset = offset;
            Length = length;
        }
        
        public string Source { get; }
        
        public int Offset { get; }
        public int Length { get; }

        public bool IsEmpty => Length == 0;
        
        public char this[int i] =>
            Source[Offset + i];
        public string Value =>
            Source.Substring(Offset, Length);
        
        public override string ToString() => Value;

        public StringSpan AsEmpty() =>
            new StringSpan(Source, Offset, 0);
        public StringSpan With(StringSpan other) =>
            new StringSpan(Source, Offset, other.Length);
        
        public StringSpan Extend(int length) =>
            new StringSpan(Source, Offset, Length + length);
        public StringSpan Take(int count) =>
            new StringSpan(Source, Offset, count.ButNotGreater(than: Length));
        
        public StringSpan Skip(StringSpan other) => Skip(other.Length);
        public StringSpan Skip(int count)
        {
            count = count.ButNotGreater(than: Length);
            
            return new StringSpan(Source, Offset + count, Length - count);
        }
        
        public (StringSpan Left, StringSpan Right) Split(int at) =>
            (Left:  new StringSpan(Source, Offset, at),
             Right: new StringSpan(Source, Offset + at, Length - at));
        
        public static implicit operator StringSpan(string self) => new StringSpan(self);

        public static bool operator ==(StringSpan a, StringSpan b) => a.Equals(b);
        public static bool operator !=(StringSpan a, StringSpan b) => !a.Equals(b);

        public bool Equals(StringSpan other)
        {
            if (Length != other.Length)
                return false;
            
            for (var i = 0; i < Length; i++)
                if (this[i] != other[i])
                    return false;

            return true;
        }

        public override bool Equals(object obj) =>
            obj is StringSpan other && Equals(other);

        public override int GetHashCode() =>
            GetHashCode(Source, Offset, Length);

        public static int GetHashCode(string str) =>
            GetHashCode(str, 0, str.Length);
        
        private static int GetHashCode(string source, int offset, int length)
        {
            switch (length)
            {
                case 0: return 0;
                case 1: return source[offset];
                case 2: return source[offset] * 9733 ^ source[offset + 1];
                case 3: return (source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2];
                case 4: return ((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3];
                case 5: return (((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4];
                case 6: return ((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5];
                case 7: return (((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6];
                case 8: return ((((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6]) * 9733 ^ source[offset + 7];
                case 9: return (((((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6]) * 9733 ^ source[offset + 7]) * 9733 ^ source[offset + 8];
                case 10: return ((((((((source[offset] * 9733 ^ source[offset + 1]) * 9733 ^ source[offset + 2]) * 9733 ^ source[offset + 3]) * 9733 ^ source[offset + 4]) * 9733 ^ source[offset + 5]) * 9733 ^ source[offset + 6]) * 9733 ^ source[offset + 7]) * 9733 ^ source[offset + 8]) * 9733 ^ source[offset + 9];
            }
            
            var hash = (int) source[offset];
            var i = offset + 1;
            length--;

            while (length > 2)
            {
                hash = (hash * 9733) ^ source[i++];
                hash = (hash * 9733) ^ source[i++];
                length -= 2;
            }

            if (length != 0)
                hash = (hash * 9733) ^ source[i];

            return hash;
        }
    }
}