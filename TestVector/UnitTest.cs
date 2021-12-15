using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VectorTask;

namespace TestVector
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestScalarProduct()
        {
            var vector1 = new Vector(new[] { 3.0, 2.0, 5.0 });
            var vector2 = new Vector(new[] { 1.0, 2.0, 4.0 });

            Assert.AreEqual(27, Vector.GetScalarProduct(vector1, vector2), 0.00001);
        }

        [TestMethod]
        public void TestAddVector()
        {
            var vector1 = new Vector(new[] { 5.0, 5.0, 5.0 });
            var vector2 = new Vector(new[] { 1.0, 1.0, 1.0 });

            vector1.Add(vector2);

            Assert.AreEqual(new Vector(new[] { 6.0, 6.0, 6.0 }), vector1);
        }

        [TestMethod]
        public void TestConstructor()
        {
            var vector1 = new Vector(4, new[] { 5.0, 5.0 });

            Assert.AreEqual(new Vector(new[] { 5.0, 5.0, 0.0, 0.0 }), vector1);
        }

        [TestMethod]
        public void TestArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Vector(Array.Empty<double>()));
        }

        [TestMethod]
        public void TestArgumentNullException()
        {
            double[] array = null;

            Assert.ThrowsException<ArgumentNullException>(() => new Vector(array));
        }
    }
}