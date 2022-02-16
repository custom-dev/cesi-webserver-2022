using CESI.NoyauFonctionnel;
using System.ComponentModel.DataAnnotations;

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
		[Display(Name = "ID")]
		public int? ID { get; set; }

		/// <summary>
		/// Titre de l'article
		/// </summary>
		[Display(Name = "Titre")]
		[Required(ErrorMessage = "Le titre de l'article est requis")]		
		public string? Titre { get; set; }

		/// <summary>
		/// URL où l'article est disponible
		/// </summary>
		[Required(ErrorMessage = "URL obligatoire")]		
		[Url(ErrorMessage = "Fake, pas une URL")]
		[Display(Name = "URL")]
		public string? URL { get; set; }

		/// <summary>
		/// Résumé de l'article
		/// </summary>
		[Required(ErrorMessage = "Résumé obligatoire")]		
		[MinLength(10, ErrorMessage = "Un résumé doit faire au minimum 10 caractères")]
		[MaxLength(100, ErrorMessage = "On a dit un résumé, pas un roman !")]
		[Display(Name = "Résumé")]
		public string? Resume { get; set; }

		/// <summary>
		/// Nombre de like de l'article
		/// </summary>
		[Display(Name = "Nombre de likes")]
		public int? NbLike { get; set; }
	}
}
