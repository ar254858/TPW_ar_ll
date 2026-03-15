using BusinessLogic;

namespace BusinessLogicTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 class_test = new Class1();
            int a = 10;
            int b = 20;
            int expected = 30;

            int result = class_test.add(a, b);
            Assert.AreEqual(expected, result);
        }
    }
}
