using Autofac.Extras.Moq;
using DemoLibrary.Logic;
using DemoLibrary.Model;
using DemoLibrary.Utilities;
using Moq;
using System;
using System.Collections.Generic;
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
		[Fact]
		public void LoadPeople_ValidCall()
		{
			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<ISqlServerDataAccess>()
					.Setup(m => m.LoadData<PersonModel>("Select * from Person"))
					.Returns(GetSamplePeople);
				
				var cls = mock.Create<PersonProcessor>();
				var actualResult = cls.LoadPeople();
				var expectedResult = GetSamplePeople();

				Assert.True(actualResult != null);
				Assert.Equal(expectedResult.Count, actualResult.Count);
				for (int i = 0; i < expectedResult.Count; i++)
				{
					Assert.Equal(expectedResult[i].FirstName, actualResult[i].FirstName);
					Assert.Equal(expectedResult[i].LastName, actualResult[i].LastName);
				}
			}
		}
		[Fact]
		public void SavePeople_ValidCall()
		{
			using(var mock = AutoMock.GetLoose())
			{
				var person = new PersonModel()
				{
					FirstName = "test",
					LastName = "test",
					HeightInInches = 77
				};
				var query = "insert into Person (FirstName, LastName, HeightInInches) " +
				"values (@FirstName, @LastName, @HeightInInches)";

				mock.Mock<ISqlServerDataAccess>()
					.Setup(m => m.SaveData(person,query));
				var cls = mock.Create<PersonProcessor>();
				cls.SavePerson(person); // Save is void
				mock.Mock<ISqlServerDataAccess>()
					.Verify(m => m.SaveData(person,query), Times.Exactly(1));
			}
		}
			private List<PersonModel> GetSamplePeople()
		{
			List<PersonModel> output = new List<PersonModel>
			{
				new PersonModel
				{
					FirstName = "John",
					LastName = "Doe"
				},
				new PersonModel
				{
					FirstName = "Tim",
					LastName = "Corey"
				}
			};
			return output;
		}
	}
}
