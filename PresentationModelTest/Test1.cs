using BusinessLogic;
using Data;
using PresentationModel;

namespace PresentationModelTest
{
    public class MockLogger : ILogger
    {
        public void LogData(object data) {}
        public void Stop() {}
    }
    [TestClass]
    public class BallModelTests
    {
        [TestMethod]
        public void LogicAndDataShouldWorkTest()
        {
            DataApi data = DataApi.CreateApi();
            MockLogger logger = new MockLogger();
            LogicApi logic = LogicApi.CreateApi(data, logger);
            BallModel model = new BallModel(logic);

            int count = 5;
            int radius = 10;

            model.CreateBalls(count, radius);
            Assert.AreEqual(count, model.GetBalls().Count());
            Assert.AreEqual(radius, ((IBall)model.GetBalls().First()).R);
        }
    }
}
