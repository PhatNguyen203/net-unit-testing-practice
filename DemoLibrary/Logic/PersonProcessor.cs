using DemoLibrary.Model;
using DemoLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Logic
{
	public class PersonProcessor : IPersonProcessor
	{
		private readonly ISqlServerDataAccess db;

		public PersonProcessor(ISqlServerDataAccess db)
		{
			this.db = db;
		}

		public (bool isValid, double heightInInches) ConvertHeightTextToInches(string height)
		{
			double result = 0;
			int feets = 0;
			double inches = 0;
			bool isValid = true;
			int feetsIndicator = height.IndexOf('\'');
			int inchesIndicator = height.IndexOf('"');
			if (feetsIndicator < 0 || inchesIndicator < 0 || inchesIndicator < feetsIndicator)
				return (false, 0);
			// Split on both the feet and inches indicators
			string[] heightParts = height.Split(new char[] { '\'', '"' });
			if (!int.TryParse(heightParts[0], out feets) || !double.TryParse(heightParts[1], out inches))
				return (false, 0);
			result = (feets * 12) + inches;
			return (isValid, result);

		}

		public PersonModel CreatePerson(string firstName, string lastName, string height)
		{
			var output = new PersonModel();
			if (ValidateName(firstName))
				output.FirstName = firstName;
			else
			{
				throw new ArgumentException("The value was not valid", "firstName");
			}

			if (ValidateName(lastName))
			{
				output.LastName = lastName;
			}
			else
			{
				throw new ArgumentException("The value was not valid", "lastName");
			}
			if (ConvertHeightTextToInches(height).isValid)
				output.HeightInInches = ConvertHeightTextToInches(height).heightInInches;
			return output;
		}

		public List<PersonModel> LoadPeople()
		{
			var query = "Select * from Person";
			return db.LoadData<PersonModel>(query);
		}

		public void SavePerson(PersonModel person)
		{
			string query = "insert into Person (FirstName, LastName, HeightInInches) " +
				"values (@FirstName, @LastName, @HeightInInches)";
			db.SaveData<PersonModel>(person, query);
		}

		public void UpdatePerson(PersonModel person)
		{
			string query = "update Person set FirstName = @FirstName, LastName = @LastName" +
				", HeightInInches = @HeightInInches where Id = @Id";
			db.UpdateData<PersonModel>(person, query);
		}
		private bool ValidateName(string name)
		{
			bool output = true;
			char[] invalidCharacters = "`~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();

			if (name.Length < 2)
			{
				output = false;
			}

			if (name.IndexOfAny(invalidCharacters) >= 0)
			{
				output = false;
			}

			return output;
		}
	}
}
