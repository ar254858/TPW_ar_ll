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
    }
}
