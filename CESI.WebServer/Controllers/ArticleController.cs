using CESI.NoyauFonctionnel;
using CESI.WebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CESI.WebServer.Controllers
{
	public class ArticleController : Controller
	{
		private IKernel _kernel;

		public ArticleController(IKernel kernel)
		{
			_kernel = kernel;
		}
		
		public IActionResult List()
		{
			IReadOnlyList<IArticle> articles = _kernel.GetArticles();
			ArticleModel[] articleModels = articles.Select(x => new ArticleModel(x)).ToArray();
			CitationModel citation = GetCitationAsync().Result;

			this.ViewBag.Citation = citation;
			return View(articleModels);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(ArticleModel article)
		{
			IArticle newArticle = _kernel.AddArticle(article.Titre, article.URL, article.Resume);

			if (newArticle != null)
			{
				return RedirectToAction("List");
			}
			else
			{
				return View(article);
			}
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			IArticle article = _kernel.GetArticle(id);
			ArticleModel model = new ArticleModel(article);
			return View(model);
		}

		[HttpGet]
		public IActionResult Vote(int id)
		{
			IArticle article = _kernel.GetArticle(id);
			article.Like();
			return RedirectToAction("List");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind()] ArticleModel model)
		{
			IArticle article = _kernel.GetArticle(id);
			article.Update(model.Titre, model.URL, model.Resume);
			return RedirectToAction("List");
		}

		[HttpGet]
		public IActionResult Details(int id)
		{
			IArticle article = _kernel.GetArticle(id);
			ArticleModel model = new ArticleModel(article);
			return View(model);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			IArticle article = _kernel.GetArticle(id);
			article.Delete();
			return RedirectToAction("List");
		}

		private async Task<CitationModel> GetCitationAsync()
		{
			using(HttpClient client = new HttpClient())
			{
				string r = await client.GetStringAsync("https://localhost:7024/Citation/Random");

				JObject jCitation = JObject.Parse(r);
				string citationID = jCitation.Value<string>("citationID");

				string json = await client.GetStringAsync($"https://localhost:7024/Citation/{citationID}");
				CitationModel citation = JsonConvert.DeserializeObject<CitationModel>(json);

				return citation;
			}
		}

	}
}
