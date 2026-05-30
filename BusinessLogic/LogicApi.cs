using Data;

namespace BusinessLogic
{
    public abstract class LogicApi
    {
        public abstract IEnumerable<IBall> Balls { get; }
        public abstract void CreateBalls(int count, int radius);
        public abstract void StartMoving();
        public abstract void MoveBalls(double deltaTime);
        public abstract void StopMoving();

        public static LogicApi CreateApi(DataApi dataApi = null, ILogger logger = null)
        {
            return new BallLogic(dataApi ?? DataApi.CreateApi(), logger);
        }
    }
}
