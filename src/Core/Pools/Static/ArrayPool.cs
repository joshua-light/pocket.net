using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common
{
    public static class ArrayPool
    {
        public static class ArrayPoolOf<T>
        {
            public struct PooledArraySegment : IEnumerable<T>, IDisposable, IEquatable<PooledArraySegment>
            {
                private struct Enumerator : IEnumerator<T>
                {
                    private readonly T[] _source;
                    private readonly int _iStart;
                    private readonly int _iEnd;

                    private int _index;

                    public Enumerator(T[] source, int iStart, int iEnd)
                    {
                        iStart.EnsureGreaterOrEqual(0);
                        iStart.EnsureLess(source.Length);
                        
                        iEnd.EnsureGreaterOrEqual(iStart);
                        iEnd.EnsureLess(source.Length);
                        
                        _source = source;
                        _iStart = iStart;
                        _iEnd = iEnd;
                        _index = _iStart - 1;
                    }

                    public T Current => _source[_index];
                    object IEnumerator.Current => Current;

                    public bool MoveNext() => ++_index < _iEnd;
                    public void Reset() => _index = _iStart - 1;

                    public void Dispose() { }
                }
                
                private readonly T[] _source;
                private readonly int _iStart;
                private readonly int _iEnd;

                public PooledArraySegment(T[] source, int iStart, int iEnd)
                {
                    _source = source;
                    _iStart = iStart;
                    _iEnd = iEnd;
                }

                public int Length => _iEnd - _iStart;
                internal int Start => _iStart;
                internal int End => _iEnd;

                public T this[int index]
                {
                    get => _source[_iStart + index];
                    set => _source[_iStart + index] = value;
                }

                public void Dispose() => Release(this);

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
                public IEnumerator<T> GetEnumerator() => new Enumerator(_source, _iStart, _iEnd);
                
                #region Equals and GetHashCode

                public static bool operator ==(PooledArraySegment a, PooledArraySegment b) => 
                    a._source == b._source && a._iStart == b._iStart && a._iEnd == b._iEnd;

                public static bool operator !=(PooledArraySegment a, PooledArraySegment b) => 
                    !(a == b);

                public bool Equals(PooledArraySegment other) => this == other;

                public override bool Equals(object obj) => 
                    !ReferenceEquals(null, obj) && obj is PooledArraySegment segment && Equals(segment);

                public override int GetHashCode()
                {
                    unchecked
                    {
                        var hashCode = _source.GetHashCode();
                        hashCode = (hashCode * 397) ^ _iStart;
                        hashCode = (hashCode * 397) ^ _iEnd;
                        return hashCode;
                    }
                }

                #endregion
            }

            internal const int Size = 1000;
        
            private static readonly T[] Buffer = new T[Size];
            private static readonly List<PooledArraySegment> UsedSegments = new List<PooledArraySegment>();

            public static PooledArraySegment Take(int size)
            {
                var startIndex = GetStartIndexForFreeSegment(size);
                var segment = new PooledArraySegment(Buffer, startIndex, startIndex + size);

                UsedSegments.Add(segment);

                return segment;
            }
            
            private static int GetStartIndexForFreeSegment(int size)
            {
                if (UsedSegments.Count == 0)
                    return 0;

                if (size <= UsedSegments[0].Start)
                    return 0;

                for (var i = 0; i < UsedSegments.Count - 1; i++)
                {
                    var spaceBetweenSegments = UsedSegments[i + 1].Start - UsedSegments[i].End;
                    if (spaceBetweenSegments < size)
                        continue;

                    return UsedSegments[i].End;
                }

                if (size <= Size - UsedSegments.Last().End)
                    return UsedSegments.Last().End;

                throw new InvalidOperationException($"Couldn't allocate memory segment of type {typeof(T).Name} with {size} size. " +
                                                    $"Only {Size - UsedSegments.Last().End} free space left.");
            }

            private static void Release(PooledArraySegment semgent)
            {
                for (var i = 0; i < semgent.Length; i++)
                    semgent[i] = default;

                UsedSegments.Remove(semgent);
            }
        }

        public static ArrayPoolOf<T>.PooledArraySegment Of<T>(int size) => ArrayPoolOf<T>.Take(size);
    }
}
