using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TodoApi.Repository
{
	/// <summary>
	/// Интерфейс репозитория.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Возвращает объект соотвествующий фильтру.
		/// </summary>
		/// <typeparam name="T">Тип объектов.</typeparam>
		/// <param name="predikate">Фильтр объектов.</param>
		/// <returns>Запрошенный объект в выделенном потоке.</returns>
		Task<T> GetObjectAsync<T>(Expression<Func<T, bool>> predikate) where T : class;

		/// <summary>
		/// Возвращает объекты модели соотвествующие фильтру.
		/// </summary>
		/// <param name="predikate">Фильтр объектов.</param>
		/// <param name="pageNum">Номер страницы данных. Если не задан - возвращаются все объекты.</param>
		/// <param name="pageSize">Количество объектов на странице данных. Если не задано - возвращаются все объекты.</param>
		/// <returns>Коллекция объектов в выделенном потоке.</returns>
		Task<List<T>> GetObjectsAsync<T>(Expression<Func<T, bool>> predikate, int? pageNum = 0, int? pageSize = 0) where T : class;
	}
}