using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        public int X
        {
            get;
            set
            {
                if (field != value)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Y
        {
            get;
            set
            {
                if (field != value)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        public int R { get; set; }
        public int Xspeed { get; set; }
        public int Yspeed { get; set; }
        public int D => R * 2;

        public Ball(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
            Xspeed = 5;
            Yspeed = 5;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
