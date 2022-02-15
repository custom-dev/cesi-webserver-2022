using CESI.NoyauFonctionnel;
using CESI.NoyauFonctionnel.Database;
using CESI.NoyauFonctionnel.Sqlite;
using Microsoft.Extensions.Options;

public class WebServer
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.Configure<SQLiteKernelConfig>(builder.Configuration.GetSection("KernelSQLite"));

		builder.Services.AddControllersWithViews();
		builder.Services.AddSingleton<SQLiteKernelConfig>(x => x.GetService<IOptions<SQLiteKernelConfig>>().Value);
		builder.Services.AddSingleton<IConnectionFactory, SQLiteConnectionFactory>();
		builder.Services.AddSingleton<IKernel, KernelImpl>();
		builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

		builder.Configuration.AddJsonFile("appsettings.json");

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		
		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.MapControllers();
		app.Run();
	}
}