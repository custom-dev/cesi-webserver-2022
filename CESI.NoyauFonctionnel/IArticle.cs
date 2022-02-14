using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel
{
	public interface IArticle
	{
		/// <summary>
		/// ID de l'article
		/// </summary>
		int ID { get; }

		/// <summary>
		/// Titre de l'article
		/// </summary>
		string Titre { get; }

		/// <summary>
		/// URL de l'article
		/// </summary>
		string URL { get; }

		/// <summary>
		/// Résumé de l'article
		/// </summary>
		string Resume { get; }

		/// <summary>
		/// Nb de like
		/// </summary>
		int NbLike { get; }

		/// <summary>
		/// Supprime un article
		/// </summary>
		void Delete();

		/// <summary>
		/// Met à jour un article
		/// </summary>
		/// <param name="titre"></param>
		/// <param name="url"></param>
		/// <param name="resume"></param>
		void Update(string titre, string url, string resume);

		/// <summary>
		/// Incrémente le nombre de like
		/// </summary>
		void Like();
	}
}
