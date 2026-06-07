using Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace BusinessLogic
{
    internal class BallLogic : LogicApi
    {
        private readonly List<IBall> _balls = new();
        public override IEnumerable<IBall> Balls => _balls;
        private readonly object _ballsLock = new object();
        private readonly DataApi _dataApi;
        private readonly ILogger _logger;
        private bool _isMoving = false;

        private System.Timers.Timer _timer;
        private DateTime _lastTickTime;
        private int _frameCounter;
        private double _accumulator = 0.0;
        private readonly double _fixedDeltaTime = 0.016;

        public BallLogic(DataApi dataApi, ILogger logger)
        {
            _dataApi = dataApi;
            _logger = logger;
        }

        public override void CreateBalls(int c, int r)
        {
            lock (_ballsLock)
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
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            if (!_isMoving) return;

            DateTime now = DateTime.Now;
            double frameTime = (now - _lastTickTime).TotalSeconds;
            _lastTickTime = now;
            _accumulator += frameTime;

            while (_accumulator >= _fixedDeltaTime)
            {
                MoveBalls(_fixedDeltaTime);
                _accumulator -= _fixedDeltaTime;
            }

            if (_frameCounter % 30 == 0)
            {
                var serializedData = _balls.Select(b => new
                {
                    Id = b.Id,
                    X = b.X,
                    Y = b.Y,
                    Xspeed = b.Xspeed,
                    Yspeed = b.Yspeed
                }).ToList();
                _logger.LogData(serializedData);
            }

            _frameCounter++;
        }

        public override void StartMoving()
        {
            if (_balls.Count == 0) return;
            if (_isMoving) return;
            _isMoving = true;

            var initialData = new
            {
                BallCount = _balls.Count,
                BallRadius = _balls[0].R,
                AreaWidth = _dataApi.Width,
                AreaHeight = _dataApi.Height
            };
            _logger.LogData(initialData);

            _frameCounter = 0;
            _lastTickTime = DateTime.Now;
            _timer = new System.Timers.Timer(16);
            _timer.Elapsed += OnTimerTick;
            _timer.AutoReset = true;
            _timer.Start();
        }

        public override void StopMoving()
        {
            _isMoving = false;

            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }

        private bool IsCollision(IBall b1, IBall b2)
        {
            double dx = (b1.X + b1.R) - (b2.X + b2.R);
            double dy = (b1.Y + b1.R) - (b2.Y + b2.R);
            double distanceSquared = dx * dx + dy * dy;

            double minDistance = b1.R + b2.R;
            return distanceSquared <= minDistance * minDistance;
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

            double targetSpeed = 180.0;

            double currentSpeed1 = Math.Sqrt(newX1 * newX1 + newY1 * newY1);
            b1.ChangeSpeed((newX1 / currentSpeed1 * targetSpeed), (newY1 / currentSpeed1 * targetSpeed));
            double currentSpeed2 = Math.Sqrt(newX2 * newX2 + newY2 * newY2);
            b2.ChangeSpeed((newX2 / currentSpeed2 * targetSpeed), (newY2 / currentSpeed2 * targetSpeed));

            double overlap = (b1.R + b2.R) - distance;
            if (overlap > 0)
            {
                double moveX = (overlap / 2) * nx;
                double moveY = (overlap / 2) * ny;

                double newB1X = b1.X - moveX;
                double newB1Y = b1.Y - moveY;
                double newB2X = b2.X + moveX;
                double newB2Y = b2.Y + moveY;

                newB1X = Math.Max(0, Math.Min(newB1X, _dataApi.Width - b1.D));
                newB1Y = Math.Max(0, Math.Min(newB1Y, _dataApi.Height - b1.D));
                newB2X = Math.Max(0, Math.Min(newB2X, _dataApi.Width - b2.D));
                newB2Y = Math.Max(0, Math.Min(newB2Y, _dataApi.Height - b2.D));

                b1.Move(newB1X, newB1Y);
                b2.Move(newB2X, newB2Y);
            }
        }

        public override void MoveBalls(double deltaTime)
        {
            lock (_ballsLock)
            {
                Parallel.ForEach(_balls, ball =>
                {
                    lock (ball.LockObject)
                    {
                        double newX = ball.X + ball.Xspeed * deltaTime;
                        double newY = ball.Y + ball.Yspeed * deltaTime;
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
                });
                Parallel.For(0, _balls.Count, i =>
                {
                    for (int j = i + 1; j < _balls.Count; j++)
                    {
                        IBall b1 = _balls[i];
                        IBall b2 = _balls[j];
                        if (IsCollision(b1, b2)) // sprawdzenie kolizji przed zablokowaniem, aby unikać niepotrzebnych blokad
                        {
                            IBall firstLock = b1.Id < b2.Id ? b1 : b2;
                            IBall secondLock = b1.Id < b2.Id ? b2 : b1;

                            lock (firstLock.LockObject)
                            {
                                lock (secondLock.LockObject)
                                {
                                    if (IsCollision(b1, b2)) // ponowne sprawdzenie kolizji po zablokowaniu
                                    {
                                        HandleCollision(b1, b2);
                                    }
                                }
                            }
                        }
                    }
                });
            }
        }



    }
}