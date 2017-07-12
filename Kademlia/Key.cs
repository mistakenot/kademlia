using System;
using System.Linq;
using System.Numerics;

namespace Kademlia
{
    public class Key : IEquatable<Key>
    {
        private readonly byte[] _bytes;

        public Key()
            : this(new byte[20])
        {

        }

        public Key(byte[] bytes)
        {
            if (bytes.Length != 20)
            {
                throw new ArgumentException($"{nameof(bytes)} must have length of 20.");
            }

            _bytes = bytes;
        }

        public bool Equals(Key other)
        {
            return this._bytes.Length == other._bytes.Length 
                && this._bytes.SequenceEqual(other._bytes);
        }

        public BigInteger DistanceTo(Key other)
        {
            var value = this._bytes.Zip(other._bytes, (a, b) => (byte)(a ^ b));
            return new BigInteger(value.ToArray());
        }

        public static readonly Key MaxValue = new Key(Enumerable.Repeat((byte)0x88, 20).ToArray());

        public static readonly Key MinValue = new Key(Enumerable.Repeat((byte)0x00, 20).ToArray());
    }
}
