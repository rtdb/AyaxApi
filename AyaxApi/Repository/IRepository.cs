using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AyaxApi.Models;

namespace AyaxApi.Repository
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
		Task<List<T>> GetObjectsAsync<T>(Expression<Func<T, bool>> predikate, int? pageNum, int? pageSize) where T : class;

		/// <summary>
		/// Новый объект сохраняет в репозитории, существующий - обновляет.
		/// </summary>
		/// <typeparam name="T">Тип объекта.</typeparam>
		/// <param name="newObject">Новый или существующий объект модели.</param>
		/// <returns>Поток сохранения оъекта.</returns>
		Task<int> SaveObjectAsync<T>(T newObject) where T : ModelBase;
	}
}