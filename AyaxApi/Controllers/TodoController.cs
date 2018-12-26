using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AyaxApi.Models;
using AyaxApi.Repository;

namespace AyaxApi.Controllers
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
		/// Возвращает экземпляр сущности "Подразделение".
		/// </summary>
		/// <param name="id">Идентификатор сущности "Подразделение".</param>
		/// <returns>Экземпляр сущности "Подразделение". </returns>
		[HttpGet("division/id={id}")]
		public async Task<ActionResult<Division>> GetDivision(long id)
		{
			Division division = null;
			try
			{
				division = await _repository.GetObjectAsync<Division>(d => d.Id == id);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);				
			}

			if (Equals(division, null))
			{
				return NotFound();
			}

			return division;
		}

		/// <summary>
		/// Возвращает подразделения по шаблону имени (like).
		/// </summary>
		/// <param name="patternName">Шаблон имени подразделения.</param>
		/// <param name="pageNum">Номер страницы данных начиная с "0". Если не задан - возвращаются все объекты.</param>
		/// <param name="pageSize">Количество объектов на странице данных. Если не задано - возвращаются все объекты.</param>
		/// <returns>Коллекция объектов соотвествующая шаблону.</returns>
		//[HttpGet("divisions={patternName};{pageNum};{pageSize}")]		
		[HttpGet("divisions/name={patternName};page={pageNum};size={pageSize}")]
		public async Task<ActionResult<IEnumerable<Division>>> GetDivisionsByName(string patternName, int? pageNum, int? pageSize)
		{
			List<Division> divisions = null;
			try
			{
				divisions = await _repository.GetObjectsAsync<Division>(d => 
													EF.Functions.Like(d.Name, string.Format("%{0}%", patternName)), pageNum, pageSize);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
				//return StatusCode(500, ex);
			}

			if (divisions.Count == 0)
			{
				return NotFound();
			}

			return divisions;
		}

		/// <summary>
		/// Сохраняет экземпляр сущности "Подразделение" в репозитории. 
		/// Новые данные будут добавлены, существующие обновлены.
		/// </summary>
		/// <param name="div">Новый или существующий объект модели.</param>
		/// <returns>Поток сохранения оъекта.</returns>
		[HttpPost("save_division")]
		public async Task<ActionResult<Division>> SaveDivision (Division div)
		{
			try
			{
				await _repository.SaveObjectAsync<Division>(div);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex);
			}

			return CreatedAtAction("GetDivision", new { div.Id}, div);
		}

		/// <summary>
		/// Удаляет объект модели "Подразделение" из репозитория.
		/// </summary>
		/// <param name="id">Идентификатор удаляемого экземпляра сущности "Подразделение".</param>
		/// <returns>Удалённая из репозитория экземпляр.</returns>
		[HttpDelete("del_division/id={id}")]
		public async Task<ActionResult<Division>> DeleteDivision(long id)
		{
			Division delObject = null;

			try
			{
				delObject = await _repository.DeleteObjectAsync<Division>(id);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}

			if (delObject == null)
			{
				return NotFound();
			}

			return delObject;
		}

		/// <summary>
		/// Возвращает экземпляр сущности "Риэлтор".
		/// </summary>
		/// <param name="id">Идентификатор сущности "Риэлтор".</param>
		/// <returns>Экземпляр сущности "Риэлтор". </returns>
		[HttpGet("realtor/id={id}")]
		public async Task<ActionResult<Realtor>> GetRealtor(long id)
		{
			Realtor realtor = null;
			try
			{
				realtor = await _repository.GetObjectAsync<Realtor>(r => r.Id == id);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
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
		[HttpGet("realtors_div/divid={divId}")]
		public async Task<ActionResult<IEnumerable<Realtor>>> GetRealtorsByDivision(long divId, int? pageNum, int? pageSize)
		{
			List<Realtor> realtors = null;
			try
			{
				realtors = await _repository.GetObjectsAsync<Realtor>(r => r.DivisionId == divId, pageNum, pageSize);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

			if (realtors.Count == 0)
			{
				return NotFound();
			}

			return realtors;
		}

		[HttpGet("realtors_name/name={patternName};page={pageNum};size={pageSize}")]
		public async Task<ActionResult<IEnumerable<Realtor>>> GetRealtorsByName(string patternName, int? pageNum, int? pageSize)
		{
			List<Realtor> realtors = null;
			try
			{				
				realtors = await _repository.GetObjectsAsync<Realtor>(r =>
												EF.Functions.Like(r.Lastname, string.Format("%{0}%", patternName)), pageNum, pageSize);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}


			if (realtors.Count == 0)
			{
				return NotFound();
			}

			return realtors;
		}

		/// <summary>
		/// Сохраняет экземпляр сущности "Риэлтор" в репозитории. 
		/// Новые данные будут добавлены, существующие обновлены.
		/// </summary>
		/// <param name="realtor">Новый или существующий объект модели.</param>
		/// <returns>Поток сохранения оъекта.</returns>
		[HttpPost("save_realtor")]
		public async Task<ActionResult<Realtor>> SaveRealtor(Realtor realtor)
		{
			try
			{
				await _repository.SaveObjectAsync<Realtor>(realtor);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}

			return CreatedAtAction("GetRealtor", new { realtor.Id }, realtor);
		}

		/// <summary>
		/// Удаляет объект модели "Риэлтор" из репозитория.
		/// </summary>
		/// <param name="id">Идентификатор удаляемого экземпляра сущности "Риэлтор".</param>
		/// <returns>Удалённая из репозитория экземпляр.</returns>
		[HttpDelete("del_realtor/id={id}")]
		public async Task<ActionResult<Realtor>> DeleteRealtor(long id)
		{
			Realtor delObject = null;

			try
			{
				delObject = await _repository.DeleteObjectAsync<Realtor>(id);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex);
			}

			if (delObject == null)
			{
				return NotFound();
			}

			return delObject;
		}
	}
}