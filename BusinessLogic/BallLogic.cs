using Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BusinessLogic
{
    internal class BallLogic : LogicApi
    {
        private readonly List<IBall> _balls = new();
        public override IEnumerable<IBall> Balls => _balls;

        private readonly DataApi _dataApi;
        private bool _isMoving = false;

        public BallLogic(DataApi dataApi)
        {
            _dataApi = dataApi;
        }

        public override void CreateBalls(int c, int r)
        {
            _balls.Clear();
            Random rng = new Random();

            for (int i = 0; i < c; i++)
            {
                int startX = rng.Next(10, _dataApi.Width - 30);
                int startY = rng.Next(10, _dataApi.Height - 30);
                IBall ball = _dataApi.CreateBall(startX, startY, r);
                _balls.Add(ball);
            }
        }

        public override void StartMoving()
        {
            if (_isMoving) return;
            _isMoving = true;

            Task.Run(async () =>
            {
                while (_isMoving)
                {
                    MoveBalls();
                    await Task.Delay(16); //1000ms / 16 ms = 60 FPS
                }
            });
        }

        public override void StopMoving() => _isMoving = false;

        private void MoveBalls()
        {
            _dataApi.MoveBalls();
        }
    }
}