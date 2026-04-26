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
        public void MoveBalls()
        {
            DataApi data = DataApi.CreateApi();
            data.CreateBall(10, 10, 5);
            data.CreateBall(50, 50, 5);

            var balls = data.GetBalls().ToList();
            int startX1 = balls[0].X;
            int startX2 = balls[1].X;

            data.MoveBalls();

            Assert.AreNotEqual(startX1, balls[0].X);
            Assert.AreNotEqual(startX2, balls[1].X);
        }
        [TestMethod]
        public void BoardTest()
        {
            DataApi data = DataApi.CreateApi();

            Assert.AreEqual(900, data.Width);
            Assert.AreEqual(600, data.Height);
        }
        [TestMethod]
        public void BounceTest()
        {
            int width = 100;
            int height = 100;

            //Right
            Ball ball = new Ball(92, 50, 5);
            ball.Move(width, height);
            Assert.AreEqual(width - ball.D, ball.X);
            int positionAfterBounce = ball.X;
            ball.Move(width, height);
            Assert.IsLessThan(positionAfterBounce, ball.X);

            //Bottom
            Ball ball1 = new Ball(50, 92, 5);
            ball1.Move(width, height);
            Assert.AreEqual(height - ball1.D, ball1.Y);
            int positionAfterBounce1 = ball1.Y;
            ball1.Move(width, height);
            Assert.IsLessThan(positionAfterBounce1, ball1.Y);
        }
    }
}
