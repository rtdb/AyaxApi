using System;
using Xunit;

using AyaxApi.Repository;
using AyaxApi.Controllers;
using AyaxApi.Models;

namespace TestAyaxApi
{
	public class UnitTest1
	{
		[Fact]
		public async void Test1()
		{
			IRepository repository = new EFRepository("Data Source = LAPTOP-R5MJF817; Database = Ayax; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
			TodoController controller = new TodoController(repository);

			Division actual = (await controller.GetDivision(2)).Value;
			Assert.Equal("Sale", actual.Name);
		}
	}
}