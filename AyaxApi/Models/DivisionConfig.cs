using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AyaxApi.Models
{
	/// <summary>
	/// Параметры маппинга в БД модели сущности "Подразделение".
	/// </summary>
	public class DivisionConfig : IEntityTypeConfiguration<Division>
	{
		/// <summary>
		/// Настраивает параметры маппинга в БД модели сущности "Подразделение".
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<Division> builder)
		{
			// Таблица & схема.
			builder
				.ToTable("Divisions", "Ayax");

			// Индексы и ограничения.
			builder
				.HasKey(d => d.Id).HasName("PK_Divisions_Id");
			//builder
			//	.HasAlternateKey(d => d.Name).HasName("UQ_Divisions_Name");
			builder
				.HasIndex(d => d.Name).HasName("Idx__Divisions_Name");

			// Поля таблицы.
			builder
				.Property(d => d.Name).IsRequired().HasMaxLength(200);
			builder
				.Property(d => d.CreatedDateTime).IsRequired();
		}
	}
}