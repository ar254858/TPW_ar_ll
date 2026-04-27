using System.Collections.ObjectModel;
using PresentationModel;
using System.Windows.Input;

namespace PresentationViewModel
{
    public class BallPresentationVM
    {
        private BallModel _model;
        public ObservableCollection<Object> Balls { get; } = new();
        public ICommand CreateBallsCommand { get; }
        public int SelectedBallCount { get; set; } = 5;
        public int SelectedBallRadius { get; set; } = 5;

        public BallPresentationVM(BallModel model)
        {
            _model = model;
            CreateBallsCommand = new RelayCommand(_ => Generate());
        }
        public void Generate()
        {
            int count = SelectedBallCount;
            int radius = SelectedBallRadius;
            _model.CreateBalls(count, radius);
            Balls.Clear();
            foreach (var b in _model.GetBalls())
            {
                Balls.Add(b);
            }
        }
    }
}
