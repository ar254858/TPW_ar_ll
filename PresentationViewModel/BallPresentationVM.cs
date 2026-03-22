using System.Collections.ObjectModel;
using PresentationModel;
using Data;

namespace PresentationViewModel
{
    public class BallPresentationVM
    {
        public BallModel model { get; } = new();
        public ObservableCollection<Ball> Balls { get; } = new();

        public void Generate(int count)
        {
            model.CreateBalls(count);
            Balls.Clear();
            foreach (var b in model.GetBalls())
            {
                Balls.Add(b);
            }
        }
    }
}
