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
		/// Тест 1: "Получение подразделения по идентификатору подразделения"
		/// </summary>
		[Fact]		
		public async void GetDivisionTest()
		{
			Division actual = (await _controller.GetDivision(1)).Value;
			Assert.Equal("HR", actual.Name);
		}

		/// <summary>
		/// Тест 2: "Получение подразделений по шаблону имени (like)."
		/// </summary>
		[Fact]
		public async void GetDivisionsByNameTest()
		{
			IEnumerable<Division> actual =	(await _controller.GetDivisionsByName("H", 0, 10)).Value;
			Assert.Equal("HR", actual.First().Name);
		}
	}
}