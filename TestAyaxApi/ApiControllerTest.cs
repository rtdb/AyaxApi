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
	/// ��������� ������ AyaxApiController
	/// </summary>
	public class ApiControllerTest
	{
		private ApiController _controller;

		/// <summary>
		/// B�������� ������������� ����������� � EF-��������� ��� ���������� ������.
		/// </summary>
		public ApiControllerTest()
		{
			_controller = new ApiController(
				new EFRepository("Data Source = LAPTOP-R5MJF817; Database = Ayax; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"));
		}

		/// <summary>
		/// ���� 1: "��������� ������������� �� �������������� �������������"
		/// </summary>
		[Fact]		
		public async void GetDivisionTest()
		{
			Division actual = (await _controller.GetDivision(1)).Value;
			Assert.Equal("HR", actual.Name);
		}

		/// <summary>
		/// ���� 2: "��������� ������������� �� ������� ����� (like)."
		/// </summary>
		[Fact]
		public async void GetDivisionsByNameTest()
		{
			IEnumerable<Division> actual =	(await _controller.GetDivisionsByName("H", 0, 10)).Value;
			Assert.Equal("HR", actual.First().Name);
		}
	}
}