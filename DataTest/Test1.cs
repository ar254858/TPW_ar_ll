using Data;

namespace DataTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void CreateAndStoreBalls()
        {
            DataApi data = DataApi.CreateApi();
            data.CreateBall(10, 10, 5);
            data.CreateBall(20, 20, 5);
            var balls = data.GetBalls();

            Assert.AreEqual(2, balls.Count());
        }
        [TestMethod]
        public void BoardTest()
        {
            DataApi data = DataApi.CreateApi();

            Assert.AreEqual(900, data.Width);
            Assert.AreEqual(600, data.Height);
        }
        [TestMethod]
        public void LoggerTest()
        {
            string testDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestLogs");
            ILogger logger = new ASCIILogger(testDir);
            var testData = new { Value = 42 };

            logger.LogData(testData);
            logger.Stop();

            var directoryInfo = new DirectoryInfo(testDir);
            var latestFile = directoryInfo.GetFiles("ball_log_*.txt").OrderByDescending(f => f.CreationTime).First();
            string content = File.ReadAllText(latestFile.FullName);

            Assert.Contains("\"Value\":42", content);
            Directory.Delete(testDir, true);
        }

    }
}
