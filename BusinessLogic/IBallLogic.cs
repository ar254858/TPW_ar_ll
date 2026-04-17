using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    internal interface IBallLogic
    {
        List<Ball> Balls { get;}
        void CreateBalls(int c, int r);
        void StartMoving();
    }
}
