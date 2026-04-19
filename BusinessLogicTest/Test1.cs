using BusinessLogic;
using Data;

namespace BusinessLogicTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void CorrectCreateBalls()
        {
            BallLogic logic = new();
            int count = 10;
            int radius = 5;

            logic.CreateBalls(count, radius);
            Assert.HasCount(count, logic.Balls);

            foreach (var ball in logic.Balls)
            {
                Assert.IsLessThanOrEqualTo(logic.GameBoard.Width, ball.X + ball.D);
                Assert.IsLessThanOrEqualTo(logic.GameBoard.Height, ball.Y + ball.D);
            }
        }
        [TestMethod]
        public void MoveBallsTest()
        {
            BallLogic logic = new();
            logic.CreateBalls(1, 5);
            var ball = logic.Balls[0];
            int initialX = ball.X;
            int initialY = ball.Y;

            logic.MoveBalls();

            Assert.AreEqual(initialX + ball.Xspeed, ball.X);
            Assert.AreEqual(initialY + ball.Yspeed, ball.Y);
        }
        [TestMethod]
        public void MoveBallsBounceTest()
        {
            BallLogic logic = new();
            logic.CreateBalls(1, 5);
            var ball = logic.Balls[0];

            ball.X = logic.GameBoard.Width - ball.D;
            ball.Xspeed = 5;

            logic.MoveBalls();

            Assert.AreEqual(-5, ball.Xspeed);
            Assert.IsLessThanOrEqualTo(logic.GameBoard.Width, ball.X + ball.D);
        }
        [TestMethod]
        public async Task MoveBallsAsyncTest()
        {
            BallLogic logic = new();
            logic.CreateBalls(1, 5);
            int startX = logic.Balls[0].X;

            logic.StartMoving();
            await Task.Delay(50);
            logic.StopMoving();

            Assert.AreNotEqual(startX, logic.Balls[0].X);
        }
    }
}
