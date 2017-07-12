using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Numerics;

namespace Kademlia.Test
{
    [TestClass]
    public class KeyTests
    {
        /* Key Properties
         * - 160 bit
         * - Comparable
         * - Xor distance
         */


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Keys_are_160_bits_fails_when_wrong()
        {
            var bytes = Enumerable.Repeat(byte.MaxValue, 21);
            var val = new Key(bytes);
        }

        [TestMethod]
        public void Keys_are_160_bits_succeeds_when_correct()
        {
            new Key(new BigInteger(int.MaxValue));
        }

        [TestMethod]
        public void Keys_are_comparable()
        {
            var keyA = new Key(new BigInteger(0));
            var keyB = new Key(new BigInteger(0));
            Assert.IsTrue(keyA.Equals(keyB));
        }

        [TestMethod]
        public void Keys_have_xnor_distance_when_zero()
        {
            var keyA = new Key(new BigInteger(0));
            var keyB = new Key(new BigInteger(0));
            var actual = keyA.DistanceTo(keyB);
            var expected = new BigInteger(new byte[160]);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Keys_have_xor_distance_when_not_zero()
        {
            var x = new BigInteger(12341234);
            var y = new BigInteger(23452345);

            var keyA = new Key(x);
            var keyB = new Key(y);

            Assert.AreEqual(x ^ y, keyA.DistanceTo(keyB));
            Assert.AreEqual(x ^ y, keyB.DistanceTo(keyA));
        }
    }
}
