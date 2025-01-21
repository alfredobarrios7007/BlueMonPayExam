using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatUsersServices;
using CatUsersRepos;
using CatUsersModels.Entities;

namespace CatUsersApi.Controllers;
/// <summary>
/// SessionslogController class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
[ApiController]
[Route("api/sessionslog")]
public class SessionslogController : ControllerBase
{
	private readonly IServicePkInt<SessionslogEntity> _service;

	public SessionslogController(CatUsersDbContext context)
	{
		_service = new SessionslogService(context);
	}

	/// <summary>
	/// GetAll method
	/// Get all rows, asynchronous method
	/// arguments:
	/// None
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;Result&lt;IEnumerable&lt;Sessionslog&gt;&gt;&gt;</returns>
	// GET: api/Sessionslog
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<IEnumerable<SessionslogEntity>>> GetAll()
	{
		var result = _service.GetAllAsync();
		if (!result.Result.IsSuccess)
		{
			return NotFound(result.Exception);
		}
		return Ok(result.Result.Value);
	}

	/// <summary>
	/// Get method
	/// Find a row, asynchronous method
	/// arguments:
	/// int id
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;Result&lt;Sessionslog&gt;&gt;</returns>
	// GET: api/Sessionslog/{id}
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<SessionslogEntity>> Get(int id)
	{
		var result = _service.GetByIdAsync(id);
		if (result == null)
		{
			return NotFound(result.Exception);
		}

		return Ok(result.Result.Value);
	}

	
	/// <summary>
	/// Create method
	/// Create a new row, asynchronous method
	/// arguments:
	/// SessionslogEntity arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;IActionResult&gt;</returns>
	// POST: api/Sessionslog
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create([FromBody] SessionslogEntity arg)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var result = _service.AddAsync(arg);
		if (!result.Result.IsSuccess)
		{
			return Conflict(result.Exception);
		}
		return Ok(result.Result.Value);
	}

	/// <summary>
	/// Update method
	/// Update a row, asynchronous method
	/// arguments:
	/// int id
	/// SessionslogEntity arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;IActionResult;&gt;</returns>
	// PUT: api/Sessionslog/{id}
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Update(int id, [FromBody] SessionslogEntity arg)
	{
		if (id != arg.id)
		{
			return BadRequest();
		}

		var result = _service.UpdateAsync(id,arg);
		if (!result.Result.IsSuccess)
		{
			return NotFound(result.Exception);
		}

		return Ok(result.Result.Value);
	}

	/// <summary>
	/// Delete method
	/// Delete a row, asynchronous method
	/// arguments:
	/// int id
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;IActionResult&gt;</returns>
	// DELETE: api/Sessionslog/{id}
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Delete(int id)
	{
		var result = _service.DeleteAsync(id);
		if (!result.Result.IsSuccess)
		{
			return NotFound(result.Exception);
		}
		return NoContent();
	}
}
