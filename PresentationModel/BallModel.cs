using BusinessLogic;
using Data;

namespace PresentationModel
{
    public class BallModel
    {
        private BallLogic Logic = new();
        public void CreateBalls(int c)
        {
            Logic = new();
            Logic.CreateBalls(c);
        }

        public List<Ball> GetBalls()
        {
            return Logic.Balls;
        }
    }
}
