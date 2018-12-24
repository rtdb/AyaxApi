using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TodoApi.Models;
using TodoApi.Repository;

namespace TodoApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class TodoController : ControllerBase
	{
		private readonly IRepository _repository;


		public TodoController(IRepository repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Обрабатывает запрос GET. Пр.: "/division=1"
		/// Возвращает экземпляр сущности "Подразделение".
		/// </summary>
		/// <param name="id">Идентификатор сущности "Подразделение".</param>
		/// <returns>Экземпляр сущности "Подразделение". </returns>
		[HttpGet("division={id}")]
		public async Task<ActionResult<Division>> GetDivision(long id)
		{
			Division division = null;
			try
			{
				division = await _repository.GetObjectAsync<Division>(d => d.Id == id);
			}
			catch (Exception)
			{
			}

			if (Equals(division, null))
			{
				return NotFound();
			}

			return division;
		}

		[HttpGet("divisionsbyname={pattern}")]
		public async Task<ActionResult<IEnumerable<Division>>> GetDivisionsByName(string pattern)
		{
			List<Division> divisions = null;
			try
			{
				divisions = await _repository.GetObjectsAsync<Division>(d => EF.Functions.Like(d.Name, pattern));
			}
			catch (Exception)
			{
			}

			if (Equals(divisions, null))
			{
				return NotFound();
			}

			return divisions;
		}

		/// <summary>
		/// Обрабатывает запрос GET. Пр.: "/realtor:1"
		/// Возвращает экземпляр сущности "Риэлтор".
		/// </summary>
		/// <param name="id">Идентификатор сущности "Риэлтор".</param>
		/// <returns>Экземпляр сущности "Риэлтор". </returns>
		[HttpGet("realtor={id}")]
		public async Task<ActionResult<Realtor>> GetRealtor(long id)
		{
			Realtor realtor = null;
			try
			{
				realtor = await _repository.GetObjectAsync<Realtor>(r => r.Id == id);
			}
			catch (Exception)
			{
			}

			if (Equals(realtor, null))
			{
				return NotFound();
			}

			return realtor;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="divId"></param>
		/// <returns></returns>
		[HttpGet("realtorsdiv={divId}")]
		public async Task<ActionResult<IEnumerable<Realtor>>> GetRealtorsByDivision(long divId)
		{
			List<Realtor> realtors = null;
			try
			{
				realtors = await _repository.GetObjectsAsync<Realtor>(r => r.DivisionId == divId);
			}
			catch (Exception)
			{
			}


			if (Equals(realtors, null))
			{
				return NotFound();
			}

			return realtors;
		}

		[HttpGet("realtorsbyname={pattern}")]
		public async Task<ActionResult<IEnumerable<Realtor>>> GetRealtorsByName(string pattern)
		{
			List<Realtor> realtors = null;
			try
			{				
				realtors = await _repository.GetObjectsAsync<Realtor>(d => EF.Functions.Like(d.Lastname, pattern));
			}
			catch (Exception)
			{
			}


			if (Equals(realtors, null))
			{
				return NotFound();
			}

			return realtors;
		}
	}
}