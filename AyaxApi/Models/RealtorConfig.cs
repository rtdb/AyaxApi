using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApi.Models
{
	/// <summary>
	/// Параметры маппинга в БД модели сущности "Риэлтор".
	/// </summary>
	class RealtorConfig : IEntityTypeConfiguration<Realtor>
	{
		/// <summary>
		/// Настраивает параметры маппинга в БД модели сущности "Риэлтор".
		/// </summary>
		/// <param name="builder"></param>
		public void Configure(EntityTypeBuilder<Realtor> builder)
		{
			// Таблица & схема.
			builder
				.ToTable("Realtors", "Ayax");

			// Первичный ключ, индекс, ограничение.
			builder
				.HasKey(r => r.Id).HasName("PK_Realtors_Id");
			//builder
			//	.HasAlternateKey(r => new { r.Firstname, r.Lastname }).HasName("UQ_Realtors_Firstname_Lastname");
			builder
				.HasIndex(r => r.Lastname).HasName("Idx_Realtors_Lastname");

			// Связи таблиц, внешний ключ, поведение при удалении сущности "Подразделение"
			builder
				.HasOne(r => r.Division)
				.WithMany(d => d.Realtors)
				.HasForeignKey(r => r.DivisionId)
				.HasPrincipalKey(d => d.Id)
				.OnDelete(DeleteBehavior.SetNull);

			// Поля таблицы.
			builder
				.Property(r => r.Firstname).IsRequired().HasMaxLength(200);
			builder
				.Property(r => r.Lastname).IsRequired().HasMaxLength(200);
			builder
				.Property(r => r.CreatedDateTime).HasDefaultValue(DateTime.Now);
		}
	}
}