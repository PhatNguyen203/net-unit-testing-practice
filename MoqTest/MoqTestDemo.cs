using DemoLibrary.Logic;
using System;
using Xunit;

namespace MoqTest
{
	public class MoqTestDemo
	{
		[Theory]
		[InlineData("6'8\"", true, 80)]
		[InlineData("6\"8'", false, 0)]
		[InlineData("six'eight\"", false, 0)]
		public void ConvertHeightTextToInches_VariousOptions(
			string heightText,
			bool expectedIsValid,
			double expectedHeightInInches)
		{
			var person = new PersonProcessor(null);
			var actual = person.ConvertHeightTextToInches(heightText);

			Assert.Equal(expectedIsValid, actual.isValid);
			Assert.Equal(expectedHeightInInches, actual.heightInInches);
		}
	}
}
