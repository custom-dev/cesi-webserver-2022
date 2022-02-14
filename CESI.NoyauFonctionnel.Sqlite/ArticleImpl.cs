using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESI.NoyauFonctionnel.Sqlite
{
	internal class ArticleImpl : IArticle
	{
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
				IDbDataParameter pTitre = cmd.CreateParameter();
				IDbDataParameter pUrl = cmd.CreateParameter();
				IDbDataParameter pResume = cmd.CreateParameter();
				int id;

				cmd.CommandText = "INSERT INTO article(titre, url, resume, nb_like) VALUES (@titre, @url, @resume, 0);";

				pTitre.ParameterName = "@Titre";
				pTitre.Value = titre;
				pTitre.DbType = DbType.String;

				pUrl.ParameterName = "@Url";
				pUrl.Value = url;
				pUrl.DbType = DbType.String;

				pResume.ParameterName = "@Resume";
				pResume.Value = resume;
				pResume.DbType = DbType.String;

				cmd.Parameters.Add(pTitre);
				cmd.Parameters.Add(pUrl);
				cmd.Parameters.Add(pResume);

				id = cmd.ExecuteAndGetLastInsertedRowID();

				return new ArticleImpl(connection, id, titre, url, resume, 0);
			}
		}

		public static IReadOnlyList<IArticle> GetArticles(IDbConnection connection)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				List<IArticle> articles = new List<IArticle>();

				cmd.CommandText = "SELECT id, titre, url, resume, nb_like FROM article";
				cmd.CommandType = CommandType.Text;

				using (IDataReader reader = cmd.ExecuteReader())
				{
					int colID = reader.GetOrdinal("id");
					int colTitre = reader.GetOrdinal("titre");
					int colUrl = reader.GetOrdinal("url");
					int colResume = reader.GetOrdinal("resume");
					int colNbLike = reader.GetOrdinal("nb_like");

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
		}

		public static IArticle GetArticle(IDbConnection connection, int articleID)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				IArticle? article = null;
				IDbDataParameter pID = cmd.CreateParameter();

				pID.ParameterName = "@ID";
				pID.Value = articleID;
				pID.DbType = DbType.Int32;

				cmd.CommandText = "SELECT id, titre, url, resume, nb_like FROM article WHERE id=@ID;";
				cmd.CommandType = CommandType.Text;

				cmd.Parameters.Add(pID); 

				using (IDataReader reader = cmd.ExecuteReader())
				{
					int colID = reader.GetOrdinal("id");
					int colTitre = reader.GetOrdinal("titre");
					int colUrl = reader.GetOrdinal("url");
					int colResume = reader.GetOrdinal("resume");
					int colNbLike = reader.GetOrdinal("nb_like");

					if (reader.Read())
					{

						int id = reader.GetInt32(colID);
						string titre = reader.GetString(colTitre);
						string url = reader.GetString(colUrl);
						string resume = reader.GetString(colResume);
						int nbLike = reader.GetInt32(colNbLike);

						article = new ArticleImpl(connection, id, titre, url, resume, nbLike);
					}
				}

				return article;
			}
		}

		public static void UpdateArticle(IDbConnection connection, int articleID, string titre, string url, string resume)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				IDbDataParameter pID = cmd.CreateParameter();
				IDbDataParameter pTitre = cmd.CreateParameter();
				IDbDataParameter pUrl = cmd.CreateParameter();
				IDbDataParameter pResume = cmd.CreateParameter();

				cmd.CommandText = "UPDATE article SET titre = @Titre, url = @Url, resume = @Resume WHERE id=@ID";

				pID.ParameterName = "@ID";
				pID.Value = articleID;
				pID.DbType = DbType.Int32;

				pTitre.ParameterName = "@Titre";
				pTitre.Value = titre;
				pTitre.DbType = DbType.String;

				pUrl.ParameterName = "@Url";
				pUrl.Value = url;
				pUrl.DbType = DbType.String;

				pResume.ParameterName = "@Resume";
				pResume.Value = resume;
				pResume.DbType = DbType.String;

				cmd.Parameters.Add(pID);
				cmd.Parameters.Add(pTitre);
				cmd.Parameters.Add(pUrl);
				cmd.Parameters.Add(pResume);

				cmd.ExecuteNonQuery();								
			}
		}

		public static void LikeArticle(IDbConnection connection, int articleID)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				IDbDataParameter pID = cmd.CreateParameter();

				cmd.CommandText = "UPDATE article SET nb_like = nb_like + 1 WHERE id=@ID";

				pID.ParameterName = "@ID";
				pID.Value = articleID;
				pID.DbType = DbType.Int32;

				cmd.Parameters.Add(pID);

				cmd.ExecuteNonQuery();
			}
		}

		public static void DeleteArticle(IDbConnection connection, int articleID)
		{
			using (IDbCommand cmd = connection.CreateCommand())
			{
				IDbDataParameter pID = cmd.CreateParameter();

				cmd.CommandText = "DELETE FROM article WHERE id=@ID";

				pID.ParameterName = "@ID";
				pID.Value = articleID;
				pID.DbType = DbType.Int32;

				cmd.Parameters.Add(pID);

				cmd.ExecuteNonQuery();
			}
		}
	}
}
