using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel.Database
{
	public interface IConnectionFactory
	{
		/// <summary>
		/// Instancie une nouvelle connexion
		/// </summary>
		/// <returns></returns>
		IDbConnection NewConnection();
	}
}
