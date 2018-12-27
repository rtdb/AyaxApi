using System;
using Xunit;

using AyaxApi.Repository;
using AyaxApi.Controllers;
using AyaxApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TestAyaxApi
{
	/// <summary>
	/// Тестирует методы AyaxApiController
	/// </summary>
	public class ApiControllerTest
	{
		private ApiController _controller;

		/// <summary>
		/// Bыполняет инициализацию репозитория и EF-контекста для выполнения тестов.
		/// </summary>
		public ApiControllerTest()
		{
			_controller = new ApiController(
				new EFRepository("Data Source = LAPTOP-R5MJF817; Database = Ayax; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"));
		}

		/// <summary>
		/// Тест GetDivision(): "Получение подразделения по идентификатору подразделения"
		/// </summary>
		[Fact]		
		public async void GetDivisionTest()
		{
			Division actual = (await _controller.GetDivision(1)).Value;
			Assert.NotNull(actual);
			Assert.Equal("HR", actual.Name);
		}

		/// <summary>
		/// Тест GetDivisionsByName(): "Получение подразделений по шаблону имени (like)."
		/// </summary>
		[Fact]
		public async void GetDivisionsByNameTest()
		{
			IEnumerable<Division> actual =	(await _controller.GetDivisionsByName("H", 0, 10)).Value;
			Assert.NotEmpty(actual);
			Assert.Equal("HR", actual.First().Name);
		}

		/// <summary>
		/// Тест GetDivisionsByName(): "Получение подразделений по шаблону имени (like)."
		/// </summary>
		//[Fact]
		//public async void GetDivisionsByNameCheckParamsTest()
		//{
		//	Assert.Throws(typeof(Exception), (var res = await _controller.GetDivisionsByName("H", 0, 0)).Value));

		//}

		/// <summary>
		/// Тест SaveDivision(): "Сохранение экземпляра сущности "Подразделение" в репозитории."
		/// </summary>
		[Fact]
		public async void SaveDivisionTest()
		{
			var date = DateTime.Now;
			var expected = new Division { Name = "", CreatedDateTime = date };

			await _controller.SaveDivision(expected);
			Division actual = (await _controller.GetDivision(expected.Id)).Value;

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		/// <summary>
		/// Тест DeleteDivision(): "Удаление объекта модели "Подразделение" из репозитория."
		/// </summary>
		[Fact]
		public async void DeleteDivisionTest()
		{
			Division expected = (await _controller.DeleteDivision(1)).Value;
			Division actual = (await _controller.GetDivision(expected.Id)).Value;

			Assert.NotNull(expected);
			Assert.Null(actual);
		}

		/// <summary>
		/// Тест GetRealtor(): "Получение экземпляра сущности "Риэлтор" по идентификатору
		/// </summary>
		[Fact]
		public async void GetRealtorTest()
		{
			Realtor actual = (await _controller.GetRealtor(2)).Value;

			Assert.NotNull(actual);
			Assert.Equal("Petr", actual.Firstname);
		}

		/// <summary>
		/// Тест GetRealtorsByDivisionTest(): "Получение риэлторов по идентификатору подразеления."
		/// </summary>
		[Fact]
		public async void GetRealtorsByDivisionTest()
		{
			IEnumerable<Realtor> actual = (await _controller.GetRealtorsByDivision(2, 0, 10)).Value;

			Assert.NotEmpty(actual);

			foreach(var r in actual)
			{
				Assert.Equal(2, r.DivisionId);
			}
			
		}

		/// <summary>
		/// Тест GetRealtorsByName(): "Получение риэлторов по шаблону имени (like)."
		/// </summary>
		[Fact]
		public async void GetRealtorsByNameTest()
		{
			IEnumerable<Realtor> actual = (await _controller.GetRealtorsByName("a", 0, 10)).Value;

			Assert.NotEmpty(actual);

			foreach (var r in actual)
			{
				Assert.Matches("a", r.Lastname);
			}
		}

		/// <summary>
		/// Тест SaveRealtor(): "Сохранение экземпляра сущности "Риэлтор" в репозитории."
		/// </summary>
		[Fact]
		public async void SaveRealtorTest()
		{
			var date = DateTime.Now;
			var expected = new Realtor { Firstname = "Dmitry", Lastname = "Testov", CreatedDateTime = date, DivisionId = 2 };

			await _controller.SaveRealtor(expected);
			Realtor actual = (await _controller.GetRealtor(expected.Id)).Value;

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		/// <summary>
		/// Тест DeleteRealtor(): "Удаление объекта модели "Риэлтор" из репозитория."
		/// </summary>
		[Fact]
		public async void DeleteRealtorTest()
		{
			Realtor expected = (await _controller.DeleteRealtor(1)).Value;
			Realtor actual = (await _controller.GetRealtor(expected.Id)).Value;

			Assert.NotNull(expected);
			Assert.Null(actual);
		}
	}
}