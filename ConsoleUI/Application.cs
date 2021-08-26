using DemoLibrary.Logic;
using DemoLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	public class Application : IApplication
	{
		private readonly IPersonProcessor db;

		public Application(IPersonProcessor db)
		{
			this.db = db;
		}
		public void Run()
		{
			RunTask();
		}
		private void RunTask()
		{
			var choice = "";
			Console.WriteLine();
			do
			{
				choice = GetActionChoice();
				switch (choice)
				{
					case "1":
						DisplayPeople(db.LoadPeople());
						break;
					case "2":
						AddPerson();
						break;
					case "3":
						Console.WriteLine("Thanks for using this application");
						break;
					default:
						Console.WriteLine("That was an invalid choice. Hit enter and try again.");
						break;
				}
			}
			while (choice != "3");
		}
		private void DisplayPeople(List<PersonModel> people)
		{
			foreach (var p in people)
			{
				Console.WriteLine(p.FullName);
			}
		}
		private string GetActionChoice()
		{
			string output = "";

			Console.Clear();
			Console.WriteLine("Menu Options".ToUpper());
			Console.WriteLine("1 - Load People");
			Console.WriteLine("2 - Create and Save Person");
			Console.WriteLine("3 - Exit");
			Console.Write("What would you like to choose: ");
			output = Console.ReadLine();

			return output;
		}

		private void AddPerson()
		{
			Console.Write("What is the person's first name: ");
			string firstName = Console.ReadLine();
			Console.Write("What is the person's last name: ");
			string lastName = Console.ReadLine();
			Console.Write("What is the person's height: ");
			string height = Console.ReadLine();

			var person = db.CreatePerson(firstName, lastName, height);
			db.SavePerson(person);
		}
	}
}
