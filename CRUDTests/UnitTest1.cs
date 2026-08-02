using CRUDTests.Services;

namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange -declariation of variables 
            MyMath mm = new MyMath();
            int input1 = 10, input2 = 20;
            //expected value
            int expected = 30;

            //act - which methods would you like to test (calling method)
            int actual = mm.Add(input1, input2);

            //assert -compare actually value with expected value 
            Assert.Equal(expected, actual); //if equal, they return true, and test passes

        }
    }
}
