using BusinessLogic;
using Data;

namespace BusinessLogicTest
{
    public class MockLogger : ILogger
    {
        public int CallCount { get; private set; } = 0;
        public void LogData(object data) 
        {
            CallCount++;
        }
        public void Stop() {}
    }
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
            double startX1 = balls[0].X;
            double startX2 = balls[1].X;
            logic.MoveBalls(0.016);

            Assert.AreNotEqual(startX1, balls[0].X);
            Assert.AreNotEqual(startX2, balls[1].X);
        }
        [TestMethod]
        public async Task MovingTest()
        {
            DataApi data = DataApi.CreateApi();
            MockLogger logger = new MockLogger();
            LogicApi logic = LogicApi.CreateApi(data, logger);
            logic.CreateBalls(1, 5);

            var ball = logic.Balls.First();
            double initialX = ball.X;
            double initialY = ball.Y;

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
            MockLogger logger = new MockLogger();
            LogicApi logic = LogicApi.CreateApi(data, logger);
            logic.CreateBalls(1, 5);

            logic.StartMoving();
            await Task.Delay(50);
            logic.StopMoving();

            double positionAfterStop = logic.Balls.First().X;

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

            ball.ChangeSpeed(100, 0);
            ball.Move(data.Width - ball.D + 1, ball.Y);
            double startX = ball.X;

            logic.MoveBalls(0.016);
            Assert.IsLessThan(startX, ball.X);

            ball.ChangeSpeed(0, 100);
            ball.Move(ball.X, data.Height - ball.D + 1);
            double startY = ball.Y;

            logic.MoveBalls(0.016);
            Assert.IsLessThan(startY, ball.Y);
        }
        [TestMethod]
        public void BarrierTest()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            logic.CreateBalls(100, 10);
            for (int i = 0; i < 50; i++)
            {
                logic.MoveBalls(0.016);
            }

            foreach (var ball in logic.Balls)
            {
                Assert.IsTrue(ball.X >= 0 && ball.X + ball.D <= data.Width);
                Assert.IsTrue(ball.Y >= 0 && ball.Y + ball.D <= data.Height);
            }
        }
        [TestMethod]
        public async Task TestIsLoggerCalled()
        {
            DataApi data = DataApi.CreateApi();
            var mockLogger = new MockLogger();

            LogicApi logic = LogicApi.CreateApi(data, mockLogger);
            logic.CreateBalls(2, 10);

            logic.StartMoving();
            await Task.Delay(200);

            logic.StopMoving();
            Assert.IsGreaterThan(0, mockLogger.CallCount);
        }
    }
}
