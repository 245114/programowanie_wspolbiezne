using Logic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class LogicTest
    {
        private LogicAbstractApi LApi;
        private Mock<TimerApi> timer;

        [TestInitialize]
        public void Initialize()
        {
            timer = new Mock<TimerApi>();
            LApi = LogicAbstractApi.CreateApi(800, 600, timer.Object);
        }

        [TestMethod]
        public void CreateBallsList_IncreasesBallCountCorrectly()
        {
            LApi.CreateBallsList(5);
            Assert.AreEqual(5, LApi.GetCount, "Creating 5 balls should set count to 5.");

            LApi.CreateBallsList(-3);
            Assert.AreEqual(2, LApi.GetCount, "Creating -3 balls should reduce count to 2.");

            LApi.CreateBallsList(-3);
            Assert.AreEqual(0, LApi.GetCount, "Reducing count below 0 should set count to 0.");
        }

        [TestMethod]
        public void GetRadius_ShouldBeWithinExpectedRange()
        {
            LApi.CreateBallsList(1);
            int radius = LApi.GetSize(0);
            Assert.IsTrue(radius >= 20 && radius <= 40, "Radius should be within the 20-40 range.");
        }

        [TestMethod]
        public void GetCoordinates_ShouldBeWithinValidRange()
        {
            LApi.CreateBallsList(1);
            int radius = LApi.GetSize(0);
            int x = (int)LApi.GetX(0);
            int y = (int)LApi.GetY(0);

            Assert.IsTrue(x >= radius && x <= LApi.Width - radius, "X coordinate should be within valid range.");
            Assert.IsTrue(y >= radius && y <= LApi.Height - radius, "Y coordinate should be within valid range.");
        }
    }
}
