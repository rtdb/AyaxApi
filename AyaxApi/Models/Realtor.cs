using System;

namespace AyaxApi.Models
{
	/// <summary>
	/// Модель сущности "Риэлтор".
	/// </summary>
	public class Realtor : ModelBase
	{
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
		/// Подразделение.
		/// </summary>
		public virtual Division Division { get; set; }

		/// <summary>
		/// Дата создания.
		/// </summary>
		public DateTime CreatedDateTime { get; set; }
	}
}