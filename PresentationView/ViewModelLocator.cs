using BusinessLogic;
using Data;
using PresentationModel;
using PresentationViewModel;

namespace PresentationView
{
    public class ViewModelLocator
    {
        public BallPresentationVM MainViewModel { get; }

        public ViewModelLocator()
        {
            DataApi dataApi = DataApi.CreateApi();
            LogicApi logicApi = LogicApi.CreateApi(dataApi);

            BallModel ballModel = new BallModel(logicApi);
            MainViewModel = new BallPresentationVM(ballModel);
        }
    }
}
