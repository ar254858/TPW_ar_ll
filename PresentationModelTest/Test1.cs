using BusinessLogic;
using Data;
using PresentationModel;

namespace PresentationModelTest
{
    [TestClass]
    public class BallModelTests
    {
        [TestMethod]
        public void Test()
        {
            var mockLogic = new MockBallLogic();
            var model = new BallModel(mockLogic);

            model.CreateBalls(5, 10);

            Assert.IsTrue(mockLogic.CreateBallsCalled);
            Assert.IsTrue(mockLogic.StartMovingCalled);
        }
    }
}
