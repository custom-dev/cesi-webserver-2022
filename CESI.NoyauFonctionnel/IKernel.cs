using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel
{
	public interface IKernel
	{
		/// <summary>
		/// Récupère la liste des articles
		/// </summary>
		/// <returns></returns>
		IReadOnlyList<IArticle> GetArticles();

		/// <summary>
		/// Ajoute un nouvel article
		/// </summary>
		/// <param name="titre"></param>
		/// <param name="url"></param>
		/// <param name="resume"></param>
		/// <returns></returns>
		IArticle AddArticle(string titre, string url, string resume);

		/// <summary>
		/// Récupère un article par son ID
		/// </summary>
		/// <param name="articleID"></param>
		/// <returns></returns>
		IArticle GetArticle(int articleID);
	}
}
