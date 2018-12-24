using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repository
{
	public class MSSQLRepository : IRepository, IDisposable
	{
		private readonly EFContext _context;		

		private DbSet<T> GetCollectionHelper<T>() where T : class
		{
			var type = typeof(T);
			if (Equals(type, typeof(Division)))
			{
				return _context.Divisions as DbSet<T>;
			}
			else if (Equals(type, typeof(Realtor)))
			{
				return _context.Realtors as DbSet<T>;
			}

			throw new Exception();
		}

		public MSSQLRepository(string connString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<EFContext>();
			DbContextOptions<EFContext> options = optionsBuilder
				.UseSqlServer(connString)
				.Options;

			_context = new EFContext(options);
		}

		public void Dispose()
		{
			if (!Equals(_context, null))
			{
				_context.Dispose();
			}
		}

		/// <summary>
		/// Возвращает объект из репозитория соотвествующий фильтру.
		/// </summary>
		/// <typeparam name="T">Тип объектов.</typeparam>
		/// <param name="predikate">Фильтр объектов.</param>
		/// <returns>Запрошенный объект в выделенном потоке.</returns>
		public Task<T> GetObjectAsync<T>(Expression<Func<T,bool>> predikate) where T : class
		{
			return GetCollectionHelper<T>()
						.FirstOrDefaultAsync(predikate as Expression<Func<T, bool>>) as Task<T>;
		}

		/// <summary>
		/// Возвращает объекты из репозитория соотвествующие фильтру.
		/// </summary>
		/// <typeparam name="T">Тип объектов.</typeparam>
		/// <param name="predikate">Фильтр объектов.</param>
		/// <returns>Запрошенные объекты в выделенном потоке.</returns>
		public Task<List<T>> GetObjectsAsync<T>(Expression<Func<T, bool>> predikate) where T : class
		{
			return GetCollectionHelper<T>()
						.Where(predikate)
						.ToListAsync();
		}

		//public Task<List<Realtor>> GetRealtorsAsync(string pattern)
		//{
		//	return GetCollectionHelper<Realtor>()
		//				.Where(d => EF.Functions.Like(d.Lastname, pattern))
		//				.ToListAsync();
		//}
	}
}