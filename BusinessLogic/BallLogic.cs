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

        private bool IsCollision(IBall b1, IBall b2)
        {
            double dx = (b1.X + b1.R) - (b2.X + b2.R);
            double dy = (b1.Y + b1.R) - (b2.Y + b2.R);
            double distanceSquared = dx * dx + dy * dy;

            double minDistance = b1.R + b2.R;
            return distanceSquared <= minDistance * minDistance; //nie obciazamy cpu pierwiastkiem
        }

        private void HandleCollision(IBall b1, IBall b2)
        {
            double dx = (b2.X + b2.R) - (b1.X + b1.R);
            double dy = (b2.Y + b2.R) - (b1.Y + b1.R);
            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance == 0) return;

            double nx = dx / distance;
            double ny = dy / distance;

            double rvx = b1.Xspeed - b2.Xspeed;
            double rvy = b1.Yspeed - b2.Yspeed;
            double velAlongNormal = rvx * nx + rvy * ny;

            if (velAlongNormal < 0) return;

            double impulse = velAlongNormal;

            double newX1 = b1.Xspeed - impulse * nx;
            double newY1 = b1.Yspeed - impulse * ny;
            double newX2 = b2.Xspeed + impulse * nx;
            double newY2 = b2.Yspeed + impulse * ny;

            double targetSpeed = 5.0;

            double currentSpeed1 = Math.Sqrt(newX1 * newX1 + newY1 * newY1);
            b1.ChangeSpeed((newX1 / currentSpeed1 * targetSpeed), (newY1 / currentSpeed1 * targetSpeed));
            double currentSpeed2 = Math.Sqrt(newX2 * newX2 + newY2 * newY2);
            b2.ChangeSpeed((newX2 / currentSpeed2 * targetSpeed), (newY2 / currentSpeed2 * targetSpeed));

            double overlap = (b1.R + b2.R) - distance;
            if (overlap > 0)
            {
                double moveX = (overlap / 2) * nx;
                double moveY = (overlap / 2) * ny;
                b1.Move((b1.X - moveX), (b1.Y - moveY));
                b2.Move((b2.X + moveX), (b2.Y + moveY));
            }
        }

        public override void MoveBalls()
        {
            foreach (IBall ball in _balls)
            {
                lock (ball.LockObject)
                {
                    double newX = ball.X + ball.Xspeed;
                    double newY = ball.Y + ball.Yspeed;
                    double newXspeed = ball.Xspeed;
                    double newYspeed = ball.Yspeed;
                    double D = ball.D;
                    if (newX <= 0)
                    {
                        newX = 0;
                        newXspeed = -newXspeed;
                    }
                    else if (newX + D >= _dataApi.Width)
                    {
                        newX = _dataApi.Width - D;
                        newXspeed = -newXspeed;
                    }

                    if (newY <= 0)
                    {
                        newY = 0;
                        newYspeed = -newYspeed;
                    }
                    else if (newY + D >= _dataApi.Height)
                    {
                        newY = _dataApi.Height - D;
                        newYspeed = -newYspeed;
                    }

                    ball.Move(newX, newY);
                    ball.ChangeSpeed(newXspeed, newYspeed);
                }
            }
            for (int i = 0; i < _balls.Count; i++)
            {
                for (int j = i + 1; j < _balls.Count; j++)
                {
                    IBall b1 = _balls[i];
                    IBall b2 = _balls[j];
                    IBall firstLock = b1.Id < b2.Id ? b1 : b2;
                    IBall secondLock = b1.Id < b2.Id ? b2 : b1;

                    lock (firstLock.LockObject)
                    {
                        lock (secondLock.LockObject)
                        {
                            if (IsCollision(b1, b2))
                            {
                                HandleCollision(b1, b2);
                            }
                        }
                    }
                }
            }
        }


    }
}