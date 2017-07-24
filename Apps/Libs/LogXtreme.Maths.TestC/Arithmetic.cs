using LogXtreme.Maths;
using Xunit;
using Swensen.Unquote;

namespace LogXtreme.Maths.TestC {
    
    public class ArithmeticTest {

        [Fact]
        public void AddTwoToOneGivesThree() {

            // arrange
            var one = 1;
            var two = 2;
            var expected = 3;

            // act
            var result = Arithmetic.add(one, two);

            // assert
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(2, -1)]
        public void AddIntegersTheory(int val1, int val2) {
            Assert.Equal(val1 + val2, Arithmetic.add(val1, val2));
        }

        //[Theory]
        //[InlineData(1.0, -1.0)]
        //[InlineData(2.0, -1.0)]
        //public void AddFloatsTheory(float val1, float val2) {
        //    Assert.Equal(val1 + val2, Arithmetic.add(val1, val2));
        //}
    }
}
