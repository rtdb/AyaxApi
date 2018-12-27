using System;
using System.Collections.Generic;

namespace AyaxApi.Models
{
	/// <summary>
	/// Модель сущности "Подразделение".
	/// </summary>
	public class Division : ModelBase
	{
		/// <summary>
		/// Наименование подразделения.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Дата регистрации.
		/// </summary>
		public DateTime CreatedDateTime { get; set; }

		/// <summary>
		/// Риэлторы.
		/// </summary>
		public virtual List<Realtor> Realtors { get; set; }
	}
}