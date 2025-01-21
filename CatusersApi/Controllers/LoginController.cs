using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatUsersServices;
using CatUsersRepos;
using CatUsersModels;
using CatUsersModels.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CatUsersApi.Controllers;
/// <summary>
/// CtUsersController class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 12:00:24 AM
/// </summary>
[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly IServiceUser<CatUsersEntity> _service;

    public LoginController(CatUsersDbContext context) => _service = new CatUsersService(context);    

    /// <summary>
    /// Login, asynchronous method
    /// arguments:
    /// Login arg
    /// 
    /// Author: Alfredo Barrios
    /// Date: 2025-Jan-21 12:00:24 AM
    /// </summary>
    /// <returns>Task&lt;IActionResult&gt;</returns>
    // POST: api/CtUsers
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] Login arg)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.LoginAsync(arg);
        if (!result.IsSuccess)
        {
            return NotFound(new
            {
                type= "Invalid Credentials",
                title= "Login",
                status= 401,
                detail= result.Errors
            });
        }
        return Ok(new
        {
            type = "Success",
            title = "Login",
            status = 200,
            detail = "Access Granted",
            accesToken = result.Value.AccessToken,
        });
    }

}
