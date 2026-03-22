using Data;

namespace DataTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Ball ball = new(3,4,5);
            Assert.AreEqual(3, ball.X);
            Assert.AreEqual(4, ball.Y);
            Assert.AreEqual(5, ball.R);
        }
    }
}
