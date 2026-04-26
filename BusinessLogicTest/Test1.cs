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
    }
}
