using Data;

namespace DataTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void CreateAndStoreBalls()
        {
            DataApi data = DataApi.CreateApi();
            data.CreateBall(10, 10, 5);
            data.CreateBall(20, 20, 5);
            var balls = data.GetBalls();

            Assert.AreEqual(2, balls.Count());
        }
        [TestMethod]
        public void BoardTest()
        {
            DataApi data = DataApi.CreateApi();

            Assert.AreEqual(900, data.Width);
            Assert.AreEqual(600, data.Height);
        }
    }
}
