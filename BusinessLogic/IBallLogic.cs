using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IBallLogic
    {
        List<Ball> Balls { get;}
        void CreateBalls(int c, int r);
        void StartMoving();
    }
}
