

using System.Data;

namespace CESI.NoyauFonctionnel.Database
{
	public class KernelImpl : IKernel, IDisposable
	{
		private IDbConnection _connection;

		public KernelImpl(IConnectionFactory factory)
		{
			//_connection = new SQLiteConnection($"Data Source={config.SqlDbPath}");
			_connection = factory.NewConnection();
			_connection.Open();
		}

		public IArticle AddArticle(string titre, string url, string resume)
		{
			return ArticleImpl.NewArticle(_connection, titre, url, resume);
		}

		public IReadOnlyList<IArticle> GetArticles()
		{
			return ArticleImpl.GetArticles(_connection);
		}

		public IArticle GetArticle(int articleID)
		{
			return ArticleImpl.GetArticle(_connection, articleID);
		}

		#region IDisposable
		public void Dispose()
		{
			this.Dispose(true);
		}

		~KernelImpl()
		{
			this.Dispose(false);
		}

		private void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				_connection.Dispose();
				GC.SuppressFinalize(this);
			}
		}

	
		#endregion
	}
}