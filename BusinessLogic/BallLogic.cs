using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BallLogic : IBallLogic
    {
        public List<Ball> Balls { get; set; }
        public Board GameBoard { get; set; }
        
        private bool _isMoving = false;

        public BallLogic()
        {
            Balls = new List<Ball>();
            GameBoard = new Board(900, 600);
        }

        public void CreateBalls(int c, int r)
        {
            Balls.Clear();
            Random rng = new Random();

            for (int i = 0; i < c; i++)
            {
                int startX = rng.Next(10, GameBoard.Width - 30);
                int startY = rng.Next(10, GameBoard.Height - 30);
                
                Ball ball = new(startX, startY, r); 
                Balls.Add(ball);
            }
        }

        public void StartMoving()
        {
            if (_isMoving) return;
            _isMoving = true;
            
            Task.Run(async () =>
            {
                while (_isMoving)
                {
                    MoveBalls();
                    await Task.Delay(16); // ~60 FPS
                }
            });
        }

        public void StopMoving()
        {
            _isMoving = false;
        }

        public void MoveBalls()
        {
            foreach (var ball in Balls)
            {
                ball.X += ball.Xspeed;
                ball.Y += ball.Yspeed;

                if (ball.X <= 0)
                {
                    ball.X = 0;
                    ball.Xspeed = -ball.Xspeed;
                }
                else if (ball.X + ball.D >= GameBoard.Width)
                {
                    ball.X = GameBoard.Width - ball.D;
                    ball.Xspeed = -ball.Xspeed;
                }

                if (ball.Y <= 0)
                {
                    ball.Y = 0;
                    ball.Yspeed = -ball.Yspeed;
                }
                else if (ball.Y + ball.D >= GameBoard.Height)
                {
                    ball.Y = GameBoard.Height - ball.D;
                    ball.Yspeed = -ball.Yspeed;
                }
            }
        }
    }
}
