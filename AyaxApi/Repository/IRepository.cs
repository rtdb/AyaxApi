using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repository
{
	/// <summary>
	/// Интерфейс репозитория.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Возвращает объект из репозитория соотвествующий фильтру.
		/// </summary>
		/// <typeparam name="T">Тип объектов.</typeparam>
		/// <param name="predikate">Фильтр объектов.</param>
		/// <returns>Запрошенный объект в выделенном потоке.</returns>
		Task<T> GetObjectAsync<T>(Expression<Func<T, bool>> predikate) where T : class;

		/// <summary>
		/// Возвращает объекты из репозитория соотвествующие фильтру.
		/// </summary>
		/// <typeparam name="T">Тип объектов.</typeparam>
		/// <param name="predikate">Фильтр объектов.</param>
		/// <returns>Запрошенные объекты в выделенном потоке.</returns>
		Task<List<T>> GetObjectsAsync<T>(Expression<Func<T, bool>> predikate) where T : class;
	}
}