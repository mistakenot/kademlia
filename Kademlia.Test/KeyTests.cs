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
            new Key(new byte[19]);
        }

        [TestMethod]
        public void Keys_are_160_bits_succeeds_when_correct()
        {
            new Key(new byte[20]);
        }

        [TestMethod]
        public void Keys_are_comparable()
        {
            var keyA = new Key();
            var keyB = new Key();
            Assert.IsTrue(keyA.Equals(keyB));
        }

        [TestMethod]
        public void Keys_have_xnor_distance_when_zero()
        {
            var keyA = new Key();
            var keyB = new Key();
            var actual = keyA.DistanceTo(keyB);
            var expected = new BigInteger(new byte[160]);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Keys_have_xor_distance_when_not_zero()
        {
            var zero = Enumerable.Repeat((byte)0x00, 19);

            var valA = new[] { (byte)0x00 }
                .Concat(zero)
                .ToArray();
                
            var keyA = new Key(valA);

            var valB = new[] { (byte)0x01 }
                .Concat(zero)
                .ToArray();

            var keyB = new Key(valB);

            Assert.AreEqual(new BigInteger(1), keyA.DistanceTo(keyB));
            Assert.AreEqual(new BigInteger(1), keyB.DistanceTo(keyA));
        }
    }
}
