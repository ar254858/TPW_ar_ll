using BusinessLogic;
using Data;

namespace BusinessLogicTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BallLogic ballLogic = new();
            ballLogic.CreateBalls(1);
            Assert.HasCount(1, ballLogic.Balls);
            Assert.AreEqual(typeof(Ball), ballLogic.Balls.ElementAt(0).GetType());
            Assert.Contains(ballLogic.Balls.ElementAt(0), ballLogic.Balls);
        }
    }
}
