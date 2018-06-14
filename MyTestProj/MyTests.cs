using System;
using Xunit;
using Moq;
using TryXUnit;

namespace MyTestProj
{
    public class MyTests
    {
        private Mock<MyInterface> mockedClass;
        ToBeTested toBeTested;


        public MyTests(){
            mockedClass = new Mock<MyInterface>();
            toBeTested = new ToBeTested(mockedClass.Object);
        }

        [Fact]
        //This test makes sure that the method fromt he mocked class is being called.
        public void ToBeTestedMethod_Gets_Name_From_Injected_Class()
        {
            toBeTested.ToBeTestedMethod(1);

            mockedClass.Verify(m => m.ReturnsName(1));

            //more robust way to verify that the method is being called by exact parameter...
            mockedClass.Verify(m => m.ReturnsName(It.Is<int>(d => d == 1)), Times.Once());

         }

        [Fact]
        //This test makes sure that the method fromt he mocked class is being called.
        public void ToBeTestedMethod_Returns_Name_Getting_From_Mocked_Class()
        {
            var expectedName = "Kiran Koirala";
            var id = 100;
            mockedClass.Setup(i => i.ReturnsName(id)).Returns(expectedName);


            var actualResult = toBeTested.ToBeTestedMethod(id);

            Assert.Equal(expectedName, actualResult);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        //This test makes sure that the method throws an expection for invalid input.
        public void ToBeTestedMethod_throws_If_Id_Is_LessThan_1(int id)
        {
            var expectedName = "Kiran Koirala";
            mockedClass.Setup(i => i.ReturnsName(id)).Returns(expectedName);

            Assert.Throws<ArgumentException>(() => toBeTested.ToBeTestedMethod(id));
        }

        [Fact]
        //This test makes sure that the method throws an expection for invalid input.
        public void ToBeTestedMethod_throws_If_Id_Is_LessThan_1_With_Message()
        {
            var expectedName = "Kiran Koirala";
            var id = 0;
            mockedClass.Setup(i => i.ReturnsName(id)).Returns(expectedName);


            var exception = Assert.Throws<ArgumentException>(() => toBeTested.ToBeTestedMethod(id));

            Assert.Equal("Invalid Id", exception.Message);
        }
    }
}
