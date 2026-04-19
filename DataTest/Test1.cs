using Data;

namespace DataTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void GetterTest()
        {
            Ball ball = new(3,4,5);
            Assert.AreEqual(3, ball.X);
            Assert.AreEqual(4, ball.Y);
            Assert.AreEqual(5, ball.R);
            Assert.AreEqual(10, ball.D);
            Assert.AreEqual(5, ball.Xspeed);
            Assert.AreEqual(5, ball.Yspeed);
        }
        [TestMethod]
        public void NotificationTest()
        {
            Ball ball = new(3, 4, 5);
            bool receivedNotification = false;
            int notificationCount = 0;

            ball.PropertyChanged += (sender, e) =>
            {
                receivedNotification = true;
                notificationCount++;
            };

            ball.X = 10;
            ball.X = 10;

            Assert.IsTrue(receivedNotification);
            Assert.AreEqual(1, notificationCount);
        }
        [TestMethod]
        public void BoardTest()
        {
            Board board = new(100, 200);
            Assert.AreEqual(100, board.Width);
            Assert.AreEqual(200, board.Height);
        }
    }
}
