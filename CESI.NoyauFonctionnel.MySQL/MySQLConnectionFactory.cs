using CESI.NoyauFonctionnel.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel.MySQL
{
	public class MySQLConnectionFactory : IConnectionFactory
	{
		private MySQLKernelConfig _config;

		public MySQLConnectionFactory(MySQLKernelConfig config)
		{
			_config = config;
		}

		public IDbConnection NewConnection()
		{
			MySqlConnection connection = new MySqlConnection($"Server={_config.Host};Database={_config.Database};Uid={_config.Login};Pwd={_config.Password};");
			return connection;
		}
	}
}
