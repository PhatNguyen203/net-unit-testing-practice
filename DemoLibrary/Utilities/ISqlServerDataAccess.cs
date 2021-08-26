using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Utilities
{
	public interface ISqlServerDataAccess
	{
		void SaveData<T>(T person, string query);
		List<T>LoadData<T>(string query);
		void UpdateData<T>(T person, string query);
	}
}
