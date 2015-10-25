using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StronglyConnectedComponents.Client;

namespace StronglyConnectedComponents.Tests
{
    [TestClass()]
    public class LineFeederTests
    {
        [TestMethod()]
        public void LineFeederTest()
        {
            var a = new LineFeeder(@"DataToTestLineFeeder.txt");
            var results = a.ToList();
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("linea1", results.First());
            Assert.AreEqual("linea2", results.Skip(1).First());
            Assert.AreEqual("linea3", results.Skip(2).First());
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            Assert.Fail();
        }
    }
}