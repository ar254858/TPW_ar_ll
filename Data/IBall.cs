using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        int X { get; }
        int Y { get; }
        int R { get; }
        int D { get; }
        int Xspeed { get; }
        int Yspeed { get; }
        void Move(int newX, int newY);
        void ChangeSpeed(int newXSpeed, int newYSpeed);
    }
}
