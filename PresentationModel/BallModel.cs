using BusinessLogic;
using Data;

namespace PresentationModel
{
    public class BallModel(BallLogic logic)
    {
        public void CreateBalls(int c, int r)
        {
            logic.CreateBalls(c, r);
            logic.StartMoving();
        }

        public List<Ball> GetBalls()
        {
            return logic.Balls;
        }
    }
}
