using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class Ball : IBall
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double R { get; }
        public double D => R * 2;
        public double Xspeed { get; set; }
        public double Yspeed { get; set; }
        public int Id { get; }
        private readonly object _lockObject = new object();
        public object LockObject => _lockObject;

        public Ball(double x, double y, double r, int id)
        {
            Id = id;
            X = x;
            Y = y;
            R = r;
            Xspeed = 5;
            Yspeed = 5;
        }
        public void Move(double newX, double newY)
        {
            X = newX;
            Y = newY;

            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }
        public void ChangeSpeed(double newXSpeed, double newYSpeed)
        {
            Xspeed = newXSpeed;
            Yspeed = newYSpeed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
