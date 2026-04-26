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
        internal int Xspeed { get; set; }
        internal int Yspeed { get; set; }

        public Ball(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
            Xspeed = 5;
            Yspeed = 5;
        }
        public void Move(int maxWidth, int maxHeight)
        {
            X += Xspeed;
            Y += Yspeed;

            if (X <= 0)
            {
                X = 0;
                Xspeed = -Xspeed;
            }
            else if (X + D >= maxWidth)
            {
                X = maxWidth - D;
                Xspeed = -Xspeed;
            }

            if (Y <= 0)
            {
                Y = 0;
                Yspeed = -Yspeed;
            }
            else if (Y + D >= maxHeight)
            {
                Y = maxHeight - D;
                Yspeed = -Yspeed;
            }

            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
