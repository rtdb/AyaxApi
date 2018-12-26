using System;
using Microsoft.EntityFrameworkCore;

using AyaxApi.Models;

namespace AyaxApi.Repository
{
	/// <summary>
	/// Контекст моделей на Microsoft Entity Framework Core.
	/// </summary>
	public class EFContext : DbContext
	{
		/// <summary>
		/// Выполняет подготовку контекста моделей к перовому использованию.
		/// </summary>
		/// <param name="modelBuilder">Ссылка на Fluent API.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Настройка параметров маппинга моделей в БД
			modelBuilder.ApplyConfiguration(new RealtorConfig());
			modelBuilder.ApplyConfiguration(new DivisionConfig());

			//Заполнение моделей начальными данными
			modelBuilder.Entity<Division>().HasData(new Division[] {
				new Division { Id=1, Name="HR", CreatedDateTime = DateTime.Now},
				new Division { Id=2, Name="Sale", CreatedDateTime = DateTime.Now}
			});

			modelBuilder.Entity<Realtor>().HasData(new Realtor[] {
				new Realtor { Id = 1, Firstname = "Ivan", Lastname = "Ivanov", DivisionId=1 },
				new Realtor { Id = 2, Firstname = "Petr", Lastname = "Petrov" , DivisionId=1},
				new Realtor { Id = 3, Firstname = "Foma", Lastname = "Fomin" , DivisionId=1},
				new Realtor { Id = 4, Firstname = "Pavel", Lastname = "Pavlov" , DivisionId=2},
				new Realtor { Id = 5, Firstname = "Elena", Lastname = "Lenina" , DivisionId=2},
				new Realtor { Id = 6, Firstname = "Svetlana", Lastname = "Svetina" , DivisionId=2}
			});

			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Риэлторы.
		/// </summary>
		public DbSet<Realtor> Realtors { get; set; }

		/// <summary>
		/// Подразделения.
		/// </summary>
		public DbSet<Division> Divisions { get; set; }

		/// <summary>
		/// Инициализирует контекст данных.
		/// </summary>
		public EFContext(DbContextOptions<EFContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}