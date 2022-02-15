using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel.Database
{
	public static class IDbCommandExtension
	{
		public static IDbCommand AddParameterWithValue(this IDbCommand cmd, string parameterName, object value)
		{
			IDbDataParameter dbParamter = cmd.CreateParameter();
			dbParamter.ParameterName = parameterName;
			dbParamter.Value = value;
			cmd.Parameters.Add(dbParamter);

			return cmd;
		}
	}
}
