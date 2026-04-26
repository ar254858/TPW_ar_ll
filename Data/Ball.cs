using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class Ball : IBall
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int R { get; }
        public int D => R * 2;
        public int Xspeed { get; set; }
        public int Yspeed { get; set; }

        public Ball(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
            Xspeed = 5;
            Yspeed = 5;
        }
        public void Move(int newX, int newY)
        {
            X = newX;
            Y = newY;

            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }
        public void ChangeSpeed(int newXSpeed, int newYSpeed)
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
