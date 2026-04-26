using Data;

namespace BusinessLogic
{
    public abstract class LogicApi
    {
        public abstract IEnumerable<IBall> Balls { get; }
        public abstract void CreateBalls(int count, int radius);
        public abstract void StartMoving();
        public abstract void StopMoving();

        public static LogicApi CreateApi(DataApi dataApi = null)
        {
            return new BallLogic(dataApi ?? DataApi.CreateApi());
        }
    }
}
