using BusinessLogic;
using Data;

namespace BusinessLogicTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void CreateBallsTest()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            int count = 5;

            logic.CreateBalls(count, 5);
            Assert.AreEqual(count, logic.Balls.Count());
        }
        [TestMethod]
        public void MoveBalls()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            logic.CreateBalls(2, 5);

            var balls = data.GetBalls().ToList();
            int startX1 = balls[0].X;
            int startX2 = balls[1].X;

            logic.MoveBalls();

            Assert.AreNotEqual(startX1, balls[0].X);
            Assert.AreNotEqual(startX2, balls[1].X);
        }
        [TestMethod]
        public async Task MovingTest()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            logic.CreateBalls(1, 5);

            var ball = logic.Balls.First();
            int initialX = ball.X;
            int initialY = ball.Y;

            logic.StartMoving();
            await Task.Delay(160);
            logic.StopMoving();

            Assert.AreNotEqual(initialX, ball.X);
            Assert.AreNotEqual(initialY, ball.Y);
        }
        [TestMethod]
        public async Task StopMovingTest()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            logic.CreateBalls(1, 5);

            logic.StartMoving();
            await Task.Delay(50);
            logic.StopMoving();

            int positionAfterStop = logic.Balls.First().X;

            await Task.Delay(50);
            Assert.AreEqual(positionAfterStop, logic.Balls.First().X);
        }
        [TestMethod]
        public void BounceTest()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            logic.CreateBalls(1, 5);
            var balls = data.GetBalls().ToList();
            IBall ball = balls.First();
            
            ball.Move(data.Width - ball.R, ball.Y); 
            int startX = ball.X;
            logic.MoveBalls();
            Assert.IsLessThan(startX, ball.X);
            
            ball.Move(ball.X, data.Height - ball.R);
            int startY = ball.Y;
            logic.MoveBalls();
            Assert.IsLessThan(startY, ball.Y);
        }
    }
}
