using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BicycleStore.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestBonusLogic()
        {
            //Arrange
            decimal value = 900;

            //Act
            var result = CanHaveBonus(value);

            //Assert
            Assert.IsTrue(result);

        }

        public bool CanHaveBonus(decimal value)
        {
            var minPrice = 1000;
            bool isBonuseable = false;

            if (value >= minPrice)
            {
                isBonuseable = true;
            }
            return isBonuseable;
        }
    }
}
