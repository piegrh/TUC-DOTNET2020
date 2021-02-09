using System;
using Xunit;

namespace uppgift1.UnitTests
{
    public class Uppgift1UnitTests
    {
        [Fact]
        public void Calculate_BMI_AreEqual()
        {
            float result = Program.BMI(200, 100);
            Assert.Equal(25f, result);
        }

        [Fact]
        public void Calculate_CelsiusToFahrenheit_AreEqual()
        {
            float result = Program.CelsiusToFahrenheit(100);
            Assert.Equal(212f, result);
        }

        [Fact]
        public void Calculate_FahrenheitToCelsius_AreEqual()
        {
            float result = Program.FahrenheitToCelsius(212);
            Assert.Equal(100, result);
        }

        [Fact]
        public void Marcus_ParseStringToInt_Is42()
        {
            float result = IOMaster.ParseToFloat("MARCUS");
            Assert.Equal(42, result);
        }

        [Fact]
        public void Marcus_IsVariable_IsTrue()
        {
            Assert.True(IOMaster.IsVariable("MARCUS"));
        }

        [Fact]
        public void IsVariable_IsFalse()
        {
            Assert.False(IOMaster.IsVariable(null));
            Assert.False(IOMaster.IsVariable(""));
            Assert.False(IOMaster.IsVariable("XMARCUSX"));
        }

        [Fact]
        public void Calculate_Addition_AreEqual()
        {
            float result = Program.Compute(100f, 50f, '+');
            float expected = 150f;
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Calculate_Subtraction_AreEqual()
        {
            float result = Program.Compute(100, 50, '-');
            float expected = 50;
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Calculate_Division_AreEqual()
        {
            float result = Program.Compute(100, 50, '/');
            float expected = 2;
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Calculate_Newton_AreEqual()
        {
            Assert.Equal(100, Program.Newton(5, 20));
            Assert.Equal(64, Program.Newton(8, 8));
            Assert.Equal(0, Program.Newton(8, 0));
        }

        [Fact]
        public void Calculate_Multiplication_AreEqual()
        {
            float result = Program.Compute(100, 50, '*');
            float expected = 5000;
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("46364674f")]
        [InlineData("-336x")]
        [InlineData("745z")]
        [InlineData("-1.0f")]
        public void IsNumberAndLastIsALetter_IsTrue(string input)
        {
            Assert.True(IOMaster.IsNumberAndLastIsALetter(input));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("-")]
        [InlineData("-1...0f")]
        [InlineData("1..0f")]
        [InlineData("abc")]
        [InlineData("21342354235aa")]
        [InlineData("-a")]
        [InlineData("-12c34c")]
        public void IsNumberAndLastIsALetter_IsFalse(string input)
        {
            Assert.False(IOMaster.IsNumberAndLastIsALetter(input));
        }

        [Theory]
        [InlineData("-000001")]
        [InlineData("000001")]
        [InlineData("0")]
        [InlineData("0.1")]
        [InlineData("1")]
        [InlineData("-1")]
        [InlineData("1.0")]
        [InlineData("-1.0")]
        [InlineData("1.")]
        [InlineData(".0")]
        [InlineData("-.1")]
        public void IsNumeric_IsTrue(string input)
        {
            Assert.True(IOMaster.IsNumeric(input));
        }

        [Theory]
        [InlineData("NaN")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("340282400000000000000000000000000000000")]
        [InlineData("abc")]
        [InlineData("123abc")]
        [InlineData("m.n")]
        [InlineData(".")]
        public void IsNumeric_IsFalse(string input)
        {
            Assert.False(IOMaster.IsNumeric(input));
        }

        [Fact]
        public void ParseToFloat_IsNaN_False()
        {
            // Max value
            string value = "340282300000000000000000000000000000000";
            Assert.Equal(float.MaxValue.ToString("#"), value);
            Assert.False(float.IsNaN(IOMaster.ParseToFloat(value)));
        
            // min value
            value = "-340282300000000000000000000000000000000";
            Assert.Equal(float.MinValue.ToString("#"), value);
            Assert.False(float.IsNaN(IOMaster.ParseToFloat(value)));
         
            // max value + 0.1
            value = $"340282300000000000000000000000000000000.1";
            Assert.False(float.IsNaN(IOMaster.ParseToFloat(value)));

            value = $"340282300000000000000000000000000000000.{new string('1', 99999)}";
            Assert.False(float.IsNaN(IOMaster.ParseToFloat(value)));

            value = new string('2', 39);
            Assert.False(float.IsNaN(IOMaster.ParseToFloat(value)));

            value = new string('0', 39);
            Assert.False(float.IsNaN(IOMaster.ParseToFloat(value)));
        }

        [Fact]
        public void ParseToFloat_IsNaN_True()
        {
            string value = "340282400000000000000000000000000000000";
            Assert.True(float.IsNaN(IOMaster.ParseToFloat(value)));

            value = "-340282400000000000000000000000000000000";
            Assert.True(float.IsNaN(IOMaster.ParseToFloat(value)));

            value = new string('4', 39);
            Assert.True(float.IsNaN(IOMaster.ParseToFloat(value)));

            value = new string('1', 40);
            Assert.True(float.IsNaN(IOMaster.ParseToFloat(value)));
        }
    }
}
