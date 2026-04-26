using BusinessLogic;
using PresentationModel;
using PresentationViewModel;

namespace PresentationView
{
    public class ViewModelLocator
    {
        public BallPresentationVM MainViewModel { get; }

        public ViewModelLocator()
        {
            IBallLogic ballLogic = new BallLogic();
            BallModel ballModel = new BallModel(ballLogic);

            MainViewModel = new BallPresentationVM(ballModel);
        }
    }
}
