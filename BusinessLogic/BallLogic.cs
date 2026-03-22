using Data;

namespace BusinessLogic
{
    public class BallLogic
    {
        public List<Ball> Balls { get; set; }
        public BallLogic()
        {
            Balls = new List<Ball>();
        }

        public void CreateBalls(int c)
        {
            for (int i = 0; i < c; i++)
            {
                Ball ball = new(20 * i + 50, 20 * i + 50, 5);
                Balls.Add(ball);
            }
        }
    }
}
