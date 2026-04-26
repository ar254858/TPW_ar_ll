using BusinessLogic;
using Data;
using PresentationModel;
using PresentationViewModel;

namespace PresentationViewModelTest
{
    [TestClass]
    public class BallPresentationVMTests
    {
        [TestMethod]
        public void CreateBallsTest()
        {
            DataApi data = DataApi.CreateApi();
            LogicApi logic = LogicApi.CreateApi(data);
            BallModel model = new BallModel(logic);
            BallPresentationVM vm = new BallPresentationVM(model);

            int expectedCount = 3;
            vm.SelectedBallCount = expectedCount;
            vm.CreateBallsCommand.Execute(null);
            Assert.HasCount(expectedCount, vm.Balls);
            Assert.IsNotNull(vm.Balls.FirstOrDefault());
        }
        [TestMethod]
        public void RelayComExecuteTest()
        {
            bool wasExecuted = false;
            var command = new RelayCommand(_ => wasExecuted = true);
            command.Execute(null);
            Assert.IsTrue(wasExecuted);
        }
        [TestMethod]
        public void ReturnPredicateTest()
        {
            var command = new RelayCommand(_ => { }, _ => false);
            bool canExecute = command.CanExecute(null);
            Assert.IsFalse(canExecute);
        }
    }
}
