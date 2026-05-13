using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        double X { get; }
        double Y { get; }
        double R { get; }
        double D { get; }
        double Xspeed { get; }
        double Yspeed { get; }
        int Id { get; }
        object LockObject { get; }
        void Move(double newX, double newY);
        void ChangeSpeed(double newXSpeed, double newYSpeed);
    }
}
