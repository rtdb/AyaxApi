using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
	/// <summary>
	/// Модель сущности "Подразделение".
	/// </summary>
	public class Division
	{
		/// <summary>
		/// Идентификатор сущности в БД (primary key).
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Наименование подразделения.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Дата регистрации.
		/// </summary>
		public DateTime CreatedDateTime { get; set; }

		/// <summary>
		/// Риэлторы. (навигационное свойство)
		/// </summary>
		public virtual List<Realtor> Realtors { get; set; }
	}
}

//Модель сущности "Подразделение":

//Id - ключ(long)
//Name - имя(string, MaxLength(200))
//CreatedDateTime - дата регистрации(datetime, not null)
//Список сущности "Подразделение" должен иметь возможность фильтрации по Id(eq), Name(like), а так же возможность пагинации