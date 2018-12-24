using System;

namespace TodoApi.Models
{
	/// <summary>
	/// Модель сущности "Риэлтор".
	/// </summary>
	public class Realtor
	{
		/// <summary>
		/// Идентификатор сущности в БД (primary key).
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Имя.
		/// </summary>
		public string Firstname { get; set; }

		/// <summary>
		/// Фамилия.
		/// </summary>
		public string Lastname { get; set; }

		/// <summary>
		/// Идентификатор подразделения. (foreign key)
		/// </summary>
		public long? DivisionId { get; set; }

		/// <summary>
		/// Подразделение. (навигационное свойство)
		/// </summary>
		public virtual Division Division { get; set; }

		/// <summary>
		/// Дата создания.
		/// </summary>
		public DateTime CreatedDateTime { get; set; }
	}
}

//Модель сущности "Риэлтор":

//Id - ключ(long)
//Firstname - имя(string, MaxLength(200))
//Lastname - фамилия(string, MaxLength(200))
//Division - подразделение(связь с сущностью "Подразделение")
//CreatedDateTime - дата создания(datetime, not null)
//Список сущности "Риэлтор" должен иметь возможность фильтрации по Id(eq), LastName(like), Division(eq), а так же возможность пагинации