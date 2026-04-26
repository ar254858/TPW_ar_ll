using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class DataApi
    {
        public abstract IBall CreateBall(int x, int y, int radius);
        public abstract IEnumerable<IBall> GetBalls();
        public abstract int Width { get; }
        public abstract int Height { get; }
        public static DataApi CreateApi() => new DataLayer(900, 600);
    }

    internal class DataLayer : DataApi
    {
        public override int Width { get; }
        public override int Height { get; }
        private readonly List<IBall> _balls = new();

        public DataLayer(int w, int h) { 
            Width = w; 
            Height = h; 
        }

        public override IBall CreateBall(int x, int y, int r)
        {
            var ball = new Ball(x, y, r);
            _balls.Add(ball);
            return ball;
        }

        public override IEnumerable<IBall> GetBalls() => _balls;

    }
}
