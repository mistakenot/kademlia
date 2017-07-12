using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Kademlia
{
    public class Key : IEquatable<Key>
    {
        private readonly BigInteger _value;

        public Key(IEnumerable<byte> bytes)
            : this(new BigInteger(bytes.Concat(new[] { byte.MinValue }).ToArray()))
        {

        }

        public Key(BigInteger value)
        {
            if (value > MaxIntValue)
            {
                throw new ArgumentException("Value is too large.");
            }

            _value = value;
        }

        public bool Equals(Key other)
        {
            return _value.Equals(other._value);
        }

        public BigInteger DistanceTo(Key other)
        {
            return this._value ^ other._value;
        }

        public static readonly Key MaxValue = new Key(MaxIntValue);
        static readonly BigInteger MaxIntValue = new BigInteger(
            Enumerable
                .Repeat(byte.MaxValue, 20)
                .Concat(new[] { byte.MinValue })
                .ToArray());

        public static readonly Key MinValue = new Key(new BigInteger(0));
        static readonly BigInteger MinIntValue = new BigInteger(0);
    }
}
