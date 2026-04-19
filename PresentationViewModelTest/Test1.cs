using Data;
using PresentationModel;
using PresentationViewModel;

namespace PresentationViewModelTest
{
    [TestClass]
    public class BallPresentationVMTests
    {
        [TestMethod]
        public void CorrectModelMethods()
        {
            var mockLogic = new MockBallLogic();
            mockLogic.Balls.Add(new Ball(1, 2, 5));
            mockLogic.Balls.Add(new Ball(2, 3, 5));
            mockLogic.Balls.Add(new Ball(3, 4, 5));
            var model = new BallModel(mockLogic);
            var vm = new BallPresentationVM(model);

            vm.SelectedBallCount = 3;
            vm.CreateBallsCommand.Execute(null);

            Assert.HasCount(3, vm.Balls);
            Assert.IsTrue(mockLogic.CreateBallsCalled);
        }
        [TestMethod]
        public void RelayCommandExecuteAction()
        {
            bool wasExecuted = false;
            var command = new RelayCommand(_ => wasExecuted = true);

            command.Execute(null);
            Assert.IsTrue(wasExecuted);
        }

        [TestMethod]
        public void RelayCommand_CanExecute_ShouldReturnPredicateResult()
        {
            var command = new RelayCommand(_ => { }, _ => false);

            bool canExecute = command.CanExecute(null);
            Assert.IsFalse(canExecute);
        }
    }
}
