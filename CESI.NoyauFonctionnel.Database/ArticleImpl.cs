using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel.Database
{
	internal class ArticleImpl : IArticle
	{
		private static readonly string COL_ID = "id";
		private static readonly string COL_Titre = "titre";
		private static readonly string COL_Url = "url";
		private static readonly string COL_Resume= "resume";
		private static readonly string COL_NbLike = "nb_like";

		private IDbConnection _connection;
		private int _id;
		private string _titre;
		private string _url;
		private string _resume;
		private int _nbLike;

		public ArticleImpl(IDbConnection connection, int id, string titre, string url, string resume, int nbLike)
		{
			_connection = connection;
			_id = id;
			_titre = titre; 
			_url = url;
			_resume = resume;
			_nbLike = nbLike;
		}

		public int ID => _id;
		public string Titre => _titre;

		public string URL => _url;

		public string Resume => _resume;

		public int NbLike => _nbLike;

		public void Delete()
		{
			DeleteArticle(_connection, _id);
		}

		public void Update(string titre, string url, string resume)
		{
			UpdateArticle(_connection, _id, titre, url, resume);
			_titre = titre;
			_url = url;
			_resume = resume;
		}

		public void Like()
		{
			LikeArticle(_connection, _id);
		}

		public static ArticleImpl NewArticle(IDbConnection connection, string titre, string url, string resume)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				int id;

				cmd.CommandText = $"INSERT INTO article({COL_Titre}, {COL_Url}, {COL_Resume}, {COL_Resume}) VALUES (@titre, @url, @resume, 0);";

				cmd.AddParameterWithValue("@Titre", titre)
				   .AddParameterWithValue("@url", url)
				   .AddParameterWithValue("@Resume", resume);
								
				id = cmd.ExecuteAndGetLastInsertedRowID();

				return new ArticleImpl(connection, id, titre, url, resume, 0);
			}
		}

		public static IReadOnlyList<IArticle> GetArticles(IDbConnection connection)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				IReadOnlyList<IArticle> articles;

				cmd.CommandText = $"SELECT {COL_ID}, {COL_Titre}, {COL_Url}, {COL_Resume}, {COL_NbLike} FROM article";
				cmd.CommandType = CommandType.Text;
				articles = ReadArticles(connection, cmd);

				return articles;
			}
		}

		private static IReadOnlyList<IArticle> ReadArticles(IDbConnection connection, IDbCommand cmd)
		{
			List<IArticle> articles = new List<IArticle>();

			using (IDataReader reader = cmd.ExecuteReader())
			{
				int colID = reader.GetOrdinal(COL_ID);
				int colTitre = reader.GetOrdinal(COL_Titre);
				int colUrl = reader.GetOrdinal(COL_Url);
				int colResume = reader.GetOrdinal(COL_Resume);
				int colNbLike = reader.GetOrdinal(COL_NbLike);

				while (reader.Read())
				{
					IArticle article;

					int id = reader.GetInt32(colID);
					string titre = reader.GetString(colTitre);
					string url = reader.GetString(colUrl);
					string resume = reader.GetString(colResume);
					int nbLike = reader.GetInt32(colNbLike);

					article = new ArticleImpl(connection, id, titre, url, resume, nbLike);
					articles.Add(article);
				}
			}

			return articles;
		}

		public static IArticle? GetArticle(IDbConnection connection, int articleID)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				IArticle? article = null;

				cmd.CommandText = $"SELECT {COL_ID}, {COL_Titre}, {COL_Url}, {COL_Resume}, {COL_NbLike} FROM article WHERE id=@ID;";
				cmd.CommandType = CommandType.Text;

				cmd.AddParameterWithValue("@ID", articleID);

				article = ReadArticles(connection, cmd).FirstOrDefault();				

				return article;
			}
		}

		public static void UpdateArticle(IDbConnection connection, int articleID, string titre, string url, string resume)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = $"UPDATE article SET {COL_Titre} = @Titre, {COL_Url} = @Url, {COL_Resume} = @Resume WHERE {COL_ID}=@ID";

				cmd.AddParameterWithValue("@ID", articleID)
				   .AddParameterWithValue("@Titre", titre)
				   .AddParameterWithValue("@Url", url)
				   .AddParameterWithValue("Resume", resume);

				cmd.ExecuteNonQuery();								
			}
		}

		public static void LikeArticle(IDbConnection connection, int articleID)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = $"UPDATE article SET {COL_NbLike} = {COL_NbLike} + 1 WHERE {COL_ID}=@ID";
				cmd.AddParameterWithValue("@ID", articleID);				
				cmd.ExecuteNonQuery();
			}
		}

		public static void DeleteArticle(IDbConnection connection, int articleID)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = $"DELETE FROM article WHERE {COL_ID}=@ID";
				cmd.AddParameterWithValue("@ID", articleID);

				cmd.ExecuteNonQuery();
			}
		}
	}
}
