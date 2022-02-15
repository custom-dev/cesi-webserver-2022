using CESI.NoyauFonctionnel.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel.Sqlite
{
	public class SQLiteConnectionFactory : IConnectionFactory
	{
		private SQLiteKernelConfig _config;

		public SQLiteConnectionFactory(SQLiteKernelConfig config)
		{
			_config = config;
		}

		public IDbConnection NewConnection()
		{
			return new SQLiteConnection($"Data Source={_config.SqlDbPath}");

		}
	}
}
