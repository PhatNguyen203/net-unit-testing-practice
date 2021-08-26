using DemoLibrary.Model;
using DemoLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Logic
{
	public interface IPersonProcessor
	{
		List<PersonModel> LoadPeople();
		void SavePerson(PersonModel person);
		void UpdatePerson(PersonModel person);
		(bool isValid, double heightInInches) ConvertHeightTextToInches(string heightText);
		PersonModel CreatePerson(string firstName, string lastName, string heightText);
	}
}
