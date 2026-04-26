using BusinessLogic;
using Data;

namespace PresentationModel
{
    public class BallModel(LogicApi logic)
    {
        public void CreateBalls(int c, int r)
        {
            logic.CreateBalls(c, r);
            logic.StartMoving();
        }

        public IEnumerable<IBall> GetBalls()
        {
            return logic.Balls;
        }
    }
}
