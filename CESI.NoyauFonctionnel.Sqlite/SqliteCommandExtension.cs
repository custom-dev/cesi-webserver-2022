using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel.Sqlite
{
	public static class SQLiteCommandExtension
	{
		public static int ExecuteAndGetLastInsertedRowID(this IDbCommand cmd)
		{
			cmd.CommandText = $"{cmd.CommandText}; SELECT last_insert_rowid();";
			return Convert.ToInt32(cmd.ExecuteScalar());
		}
	}
}
