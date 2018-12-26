using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaxApi.Models
{
	public abstract class ModelBase
	{
		/// <summary>
		/// Уникальный идентификатор объекта модели (первичный ключ в БД).
		/// </summary>
		public long Id { get; set; }
	}
}