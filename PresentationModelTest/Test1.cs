using PresentationModel;

namespace PresentationModelTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BallModel model = new BallModel();
            model.CreateBalls(10);
            model.CreateBalls(2);
            Assert.HasCount(2, model.GetBalls());
        }
    }
}
