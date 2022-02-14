using CESI.NoyauFonctionnel;

namespace CESI.WebServer.Models
{
	public class ArticleModel
	{
		public ArticleModel()
		{

		}

		public ArticleModel(IArticle article)
		{
			this.ID = article.ID;
			this.Titre = article.Titre;
			this.URL = article.URL; 
			this.Resume = article.Resume;
			this.NbLike = article.NbLike;
		}

		/// <summary>
		/// ID de l'article
		/// </summary>
		public int? ID { get; set; }

		/// <summary>
		/// Titre de l'article
		/// </summary>
		public string? Titre { get; set; }

		/// <summary>
		/// URL où l'article est disponible
		/// </summary>
		public string? URL { get; set; }

		/// <summary>
		/// Résumé de l'article
		/// </summary>
		public string? Resume { get; set; }

		/// <summary>
		/// Nombre de like de l'article
		/// </summary>
		public int? NbLike { get; set; }
	}
}
