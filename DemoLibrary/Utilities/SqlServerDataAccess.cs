using Dapper;
using DemoLibrary.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Utilities
{
	public class SqlServerDataAccess : ISqlServerDataAccess
	{
		public List<T> LoadData<T>(string query)
		{

			using (var connect = new SqlConnection(LoadConnectionString()))
			{
				var output = connect.Query<T>(query);
				return output.ToList();
			}
		}

		public void SaveData<T>(T person, string query)
		{
			using (var connect = new SqlConnection(LoadConnectionString()))
			{
				connect.Execute(query, person);
			}
		}

		public void UpdateData<T>(T person, string query)
		{
			using (var connect = new SqlConnection(LoadConnectionString()))
			{
				connect.Execute(query, person);
			}
		}

		private string LoadConnectionString(string id = "Default")
		{
			return ConfigurationManager.ConnectionStrings[id].ConnectionString;
		}
	}
}
