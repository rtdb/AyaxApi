
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AyaxApi
{
	public class Utils
	{
		/// <summary>
		/// Читает строку подключения к базе данных из конфигурационного файла.
		/// </summary>
		/// <param name="fileName">Имя файла в текущем каталоге</param>
		/// <returns>Строка подключения к базе данных.</returns>
		public static string LoadFromSettings(string fileName)
		{
			var confBuilder = new ConfigurationBuilder();
			confBuilder.SetBasePath(Directory.GetCurrentDirectory());
			confBuilder.AddJsonFile(fileName);
			var config = confBuilder.Build();
			return config.GetConnectionString("DefaultConnection");
		}
	}
}