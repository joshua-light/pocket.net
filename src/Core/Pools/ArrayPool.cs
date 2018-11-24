using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pocket.Common
{
    public class ArrayPool<T>
    {
        /// <summary>
        ///     Represents a bounded segment of some <see cref="ArrayPool{T}"/> buffer, 
        ///     which behaves like <see cref="Array"/> or <see cref="IEnumerable"/>.
        /// </summary>
        public struct Segment : IEnumerable<T>, IDisposable, IEquatable<Segment>
        {
            public struct Enumerator : IEnumerator<T>
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

            private readonly ArrayPool<T> _pool;
            private readonly T[] _source;

            internal Segment(ArrayPool<T> pool, int iStart, int iEnd)
            {
                Start = iStart;
                End = iEnd;
                
                _pool = pool;
                _source = pool._buffer;
            }

            internal int Start { get; }
            internal int End { get; }

            /// <summary>
            ///     Length of segment.
            /// </summary>
            public int Length => End - Start;

            /// <summary>
            ///     Gets or sets elements of segment.
            /// </summary>
            /// <param name="index">Index of element.</param>
            public T this[int index]
            {
                // We ensure that `index` is not less than zero,
                // because it can be used to access elements of neighbour segments.
                get { index.EnsureGreaterOrEqual(0); return _source[Start + index]; }
                set { index.EnsureGreaterOrEqual(0); _source[Start + index] = value; }
            }
            
            /// <summary>
            ///     Releases segment and frees all its memory.
            /// </summary>
            public void Dispose() => _pool.Release(this);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
            
            public Enumerator GetEnumerator() => new Enumerator(_source, Start, End);

            #region Equals and GetHashCode

            public static bool operator ==(Segment a, Segment b) =>
                a._source == b._source && a.Start == b.Start && a.End == b.End;

            public static bool operator !=(Segment a, Segment b) =>
                !(a == b);

            public bool Equals(Segment other) => this == other;

            public override bool Equals(object obj) =>
                !ReferenceEquals(null, obj) && obj is Segment segment && Equals(segment);

            public override int GetHashCode() =>
                Hash.Of(_source).With(Start).With(End);

            #endregion
        }

        private readonly T[] _buffer;
        private readonly List<Segment> _usedSegments;
        
        private readonly int _size;

        /// <summary>
        ///     Initializes instance of <see cref="ArrayPool{T}"/> with internal buffer of specified size.
        /// </summary>
        /// <param name="size">Size of array that is used as buffer.</param>
        public ArrayPool(int size)
        {
            _buffer = new T[size];
            _usedSegments = new List<Segment>();
            
            _size = size;
        }

        /// <summary>
        ///     Allocates <see cref="Segment"/> of specified size and returns it.
        /// </summary>
        /// <param name="size">Size of <see cref="Segment"/>.</param>
        /// <returns>Instance of <see cref="Segment"/>.</returns>
        /// <exception cref="InvalidOperationException">Not enough memory to allocate segment of specified size.</exception>
        public Segment Take(int size)
        {
            var index = StartIndexOfFreeSegment(size);
            var segment = new Segment(this, index, index + size);

            _usedSegments.Add(segment);

            return segment;
        }

        private int StartIndexOfFreeSegment(int size)
        {
            // First we check whether free segment can be obtained at the start of buffer.
            if (_usedSegments.Count == 0)
                return 0;
            if (size <= _usedSegments[0].Start)
                return 0;

            // Then we check whether free segment can be obtained in between of currently used ones.
            for (var i = 0; i < _usedSegments.Count - 1; i++)
            {
                var spaceBetweenSegments = _usedSegments[i + 1].Start - _usedSegments[i].End;
                if (spaceBetweenSegments < size)
                    continue;

                return _usedSegments[i].End;
            }

            // The last one case is when free segment can be obtained at the end of buffer.
            if (size <= _size - _usedSegments.Last().End)
                return _usedSegments.Last().End;

            throw new InvalidOperationException(
                $"Couldn't allocate memory segment of type {typeof(T).Name} with {size} size. " +
                $"Only {_size - _usedSegments.Last().End} free space left.");
        }

        private void Release(Segment semgent)
        {
            for (var i = 0; i < semgent.Length; i++)
                semgent[i] = default;

            _usedSegments.Remove(semgent);
        }
    }
}
