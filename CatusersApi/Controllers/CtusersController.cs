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
/// CtusersController class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
[ApiController]
[Route("api/users")]
public class CtusersController : ControllerBase
{
	private readonly IServiceUser<CatUsersEntity> _service;

	public CtusersController(CatUsersDbContext context)
	{
		_service = new CatUsersService(context);
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
	/// <returns>Task&lt;Result&lt;IEnumerable&lt;Ctusers&gt;&gt;&gt;</returns>
	// GET: api/Ctusers
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<IEnumerable<CatUsersEntity>>> GetAll()
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
	/// <returns>Task&lt;Result&lt;Ctusers&gt;&gt;</returns>
	// GET: api/Ctusers/{id}
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<CatUsersEntity>> Get(int id)
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
	/// CatUsersEntity arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;IActionResult&gt;</returns>
	// POST: api/Ctusers
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create([FromBody] CatUsersEntity arg)
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
	/// CatUsersEntity arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;IActionResult;&gt;</returns>
	// PUT: api/Ctusers/{id}
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Update(int id, [FromBody] CatUsersEntity arg)
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
	// DELETE: api/Ctusers/{id}
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
