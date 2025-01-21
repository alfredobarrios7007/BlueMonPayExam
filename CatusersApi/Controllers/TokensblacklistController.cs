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
/// TokensblacklistController class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
[ApiController]
[Route("api/tokens-black-list")]
public class TokensblacklistController : ControllerBase
{
	private readonly IServicePkInt<TokensblacklistEntity> _service;

	public TokensblacklistController(CatUsersDbContext context)
	{
		_service = new TokensblacklistService(context);
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
	/// <returns>Task&lt;Result&lt;IEnumerable&lt;Tokensblacklist&gt;&gt;&gt;</returns>
	// GET: api/tokens-black-list
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<IEnumerable<TokensblacklistEntity>>> GetAll()
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
	/// <returns>Task&lt;Result&lt;Tokensblacklist&gt;&gt;</returns>
	// GET: api/tokens-black-list/{id}
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<TokensblacklistEntity>> Get(int id)
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
	/// TokensblacklistEntity arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;IActionResult&gt;</returns>
	// POST: api/tokens-black-list
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Create([FromBody] TokensblacklistEntity arg)
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
	/// Delete method
	/// Delete a row, asynchronous method
	/// arguments:
	/// int id
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;IActionResult&gt;</returns>
	// DELETE: api/tokens-black-list/{id}
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
