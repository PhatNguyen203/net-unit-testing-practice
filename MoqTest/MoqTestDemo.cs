using DemoLibrary.Logic;
using DemoLibrary.Model;
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
		[Theory]
		[InlineData("Tim", "Corey", "6'8\"", 80)]
		[InlineData("Charitry", "Corey", "5'4\"", 64)]
		public void CreatePerson_Successful(string firstName, string lastName, string heightText, double expectedHeight)
		{
			PersonProcessor person = new PersonProcessor(null);
			PersonModel expectedResut = new PersonModel()
			{
				FirstName = firstName,
				LastName = lastName,
				HeightInInches = expectedHeight
			};
			var actualResult = person.CreatePerson(firstName, lastName, heightText);

			Assert.Equal(expectedResut.FirstName, actualResult.FirstName);
			Assert.Equal(expectedResut.LastName, actualResult.LastName);
			Assert.Equal(expectedResut.HeightInInches, actualResult.HeightInInches);

		}
	}
}
