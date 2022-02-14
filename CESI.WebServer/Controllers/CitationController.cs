using CESI.WebServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CESI.WebServer.Controllers
{
	public class CitationController : Controller
	{
		private static IReadOnlyList<CitationModel> _citations;

		static CitationController()
		{
			_citations = new List<CitationModel>()
			{
				new CitationModel() { Auteur = "Winston Churchill", Contenu = "Là où l'on trouve un grand pouvoir, on trouve une grande responsabilité"},
				new CitationModel() { Auteur = "Karadoc", Contenu = "Le gras, c'est la vie" },
				new CitationModel() { Auteur = "Galilée", Contenu = "Et pourtant... elle tourne..."}
			};

			foreach(CitationModel citation in _citations)
			{
				citation.ID = Guid.NewGuid().ToString();
			}
		}

		[Route("Citation/{id}")]
		public IActionResult Index(string id)
		{
			if (id == "Random")
			{
				int index = System.Random.Shared.Next(0, _citations.Count);
				CitationModel citation = _citations[index];

				return Json(new { CitationID = citation.ID });
			}
			else
			{
				CitationModel citation = _citations.First(x => x.ID == id);

				return Json(citation);
			}
		}
	}
}
