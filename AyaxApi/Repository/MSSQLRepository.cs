using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AyaxApi.Models;

namespace AyaxApi.Repository
{
	public class MSSQLRepository : IRepository, IDisposable
	{
		private readonly EFContext _context;		

		// Возвращает коллекцию из контекста соотвествующую типу модели.
		private DbSet<T> SeletCollection<T>() where T : class
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
			return SeletCollection<T>()
						.FirstOrDefaultAsync(predikate as Expression<Func<T, bool>>) as Task<T>;
		}

		/// <summary>
		/// Возвращает объекты модели соотвествующие фильтру.
		/// </summary>
		/// <param name="predikate">Фильтр объектов.</param>
		/// <param name="pageNum">Номер страницы данных начиная с "0". Если не задан - возвращаются все объекты.</param>
		/// <param name="pageSize">Количество объектов на странице данных. Если не задано - возвращаются все объекты.</param>
		/// <returns>Коллекция объектов в выделенном потоке.</returns>
		public Task<List<T>> GetObjectsAsync<T>(Expression<Func<T, bool>> predikate, int? pageNum, int? pageSize) where T : class
		{
			IQueryable<T> objects = SeletCollection<T>().Where(predikate);

			if (pageNum == null || pageSize == null)
			{
				return objects.ToListAsync();
			}

			if(pageNum < 0 || pageSize < 1)
			{
				throw new Exception("Invalid paging params.");
			}

			return objects
				.Skip(pageNum.Value * pageSize.Value)
				.Take(pageSize.Value)
				.ToListAsync();
		}

		/// <summary>
		/// Новый объект сохраняет в репозитории, существующий - обновляет.
		/// </summary>
		/// <typeparam name="T">Тип объекта.</typeparam>
		/// <param name="newObject">Новый или существующий объект модели.</param>
		/// <returns>Поток сохранения оъекта.</returns>
		public Task<int> SaveObjectAsync<T>(T newObject) where T : ModelBase
		{
			DbSet<T> coll = SeletCollection<T>();

			if (newObject.Id == 0)
			{
				coll.Add(newObject);
			}
			else
			{
				if (coll.Contains(newObject))
				{
					_context.Entry(newObject).State = EntityState.Modified;
				}
				else
				{
					throw new Exception(string.Format("Object with Id={0} not exist.", newObject.Id));
				}
			}

			return _context.SaveChangesAsync();			 
		}
	}
}