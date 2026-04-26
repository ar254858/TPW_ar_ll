using BusinessLogic;
using Data;
using PresentationModel;

namespace PresentationModelTest
{
    [TestClass]
    public class BallModelTests
    {
        [TestMethod]
        public void LogicAndDataShouldWorkTest()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            BallModel model = new BallModel(logic);

            int count = 5;
            int radius = 10;

            model.CreateBalls(count, radius);
            Assert.AreEqual(count, model.GetBalls().Count());
            Assert.AreEqual(radius, model.GetBalls().First().R);
        }
    }
}
