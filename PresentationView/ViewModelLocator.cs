using BusinessLogic;
using Data;
using PresentationModel;
using PresentationViewModel;
using System.IO;

namespace PresentationView
{
    public class ViewModelLocator
    {
        public BallPresentationVM MainViewModel { get; }

        public ViewModelLocator()
        {
            ILogger diagnosticLogger = new ASCIILogger();
            System.Windows.Application.Current.Exit += (sender, args) =>
            {
                diagnosticLogger.Stop();
            };

            DataApi dataApi = DataApi.CreateApi();
            LogicApi logicApi = LogicApi.CreateApi(dataApi, diagnosticLogger);

            BallModel ballModel = new BallModel(logicApi);
            MainViewModel = new BallPresentationVM(ballModel);
        }
    }
}
