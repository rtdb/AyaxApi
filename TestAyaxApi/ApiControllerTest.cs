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
		/// ���� GetDivision(): "��������� ������������� �� �������������� �������������"
		/// </summary>
		[Fact]		
		public async void GetDivisionTest()
		{
			Division actual = (await _controller.GetDivision(1)).Value;
			Assert.NotNull(actual);
			Assert.Equal("HR", actual.Name);
		}

		/// <summary>
		/// ���� GetDivisionsByName(): "��������� ������������� �� ������� ����� (like)."
		/// </summary>
		[Fact]
		public async void GetDivisionsByNameTest()
		{
			IEnumerable<Division> actual =	(await _controller.GetDivisionsByName("H", 0, 10)).Value;
			Assert.NotEmpty(actual);
			Assert.Equal("HR", actual.First().Name);
		}

		/// <summary>
		/// ���� GetDivisionsByName(): "��������� ������������� �� ������� ����� (like)."
		/// </summary>
		//[Fact]
		//public async void GetDivisionsByNameCheckParamsTest()
		//{
		//	Assert.Throws(typeof(Exception), (var res = await _controller.GetDivisionsByName("H", 0, 0)).Value));

		//}

		/// <summary>
		/// ���� SaveDivision(): "���������� ���������� �������� "�������������" � �����������."
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
		/// ���� DeleteDivision(): "�������� ������� ������ "�������������" �� �����������."
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
		/// ���� GetRealtor(): "��������� ���������� �������� "�������" �� ��������������
		/// </summary>
		[Fact]
		public async void GetRealtorTest()
		{
			Realtor actual = (await _controller.GetRealtor(2)).Value;

			Assert.NotNull(actual);
			Assert.Equal("Petr", actual.Firstname);
		}

		/// <summary>
		/// ���� GetRealtorsByDivisionTest(): "��������� ��������� �� �������������� ������������."
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
		/// ���� GetRealtorsByName(): "��������� ��������� �� ������� ����� (like)."
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
		/// ���� SaveRealtor(): "���������� ���������� �������� "�������" � �����������."
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
		/// ���� DeleteRealtor(): "�������� ������� ������ "�������" �� �����������."
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