using BusinessLogic;
using Data;

public class MockBallLogic : IBallLogic
{
    public bool CreateBallsCalled { get; private set; }
    public bool StartMovingCalled { get; private set; }
    public List<Ball> Balls { get; set; } = new List<Ball>();

    public void CreateBalls(int c, int r)
    {
        CreateBallsCalled = true;
    }

    public void StartMoving()
    {
        StartMovingCalled = true;
    }

    public void StopMoving() {}
}