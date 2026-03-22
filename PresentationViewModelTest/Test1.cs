using PresentationViewModel;

namespace PresentationViewModelTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BallPresentationVM BallVM = new();
            BallVM.Generate(5);

            Assert.HasCount(5, BallVM.Balls);
        }
    }
}
