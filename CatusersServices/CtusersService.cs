using FluentResults;
using Microsoft.EntityFrameworkCore;
using CatUsersModels.Entities;
using CatUsersRepos;
using CatUsersModels;
using CatUsersServices.Helper;

namespace CatUsersServices;
/// <summary>
/// CatUsersService class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 12:00:24 AM
/// </summary>
public class CatUsersService : IServiceUser<CatUsersEntity>
{
	CatUsersDbContext _repo;
	public CatUsersService(){ }
	public CatUsersService(CatUsersDbContext context)
	{
		_repo = context;
	}
	public Result<IEnumerable<CatUsersEntity>> GetAll()
	{
		IEnumerable<CatUsersEntity> rows = _repo.Ctusers.ToList();
		return Result.Ok(rows);
	}
	public Result<CatUsersEntity> GetById(int id)
	{
		var row = _repo.Ctusers.Find(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		return Result.Ok(row);
	}
	public Result<CatUsersEntity> Add(CatUsersEntity row)
	{
		_repo.Ctusers.Add(row);
		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public Result<CatUsersEntity> Update(int id, CatUsersEntity row)
	{
		if (id != row.id)
			return Result.Fail(new Exception("The primary key and the row don't match").Message);
		var _row = _repo.Ctusers.Find(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_row.name = row.name;
		_row.lastname = row.lastname;
		_row.username = row.username;
		_row.password = row.password;
		_row.email = row.email;
		_row.created_dt = row.created_dt;
		_row.status = row.status;

		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public Result Delete(int id)
	{
		var _row = _repo.Ctusers.Find(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_repo.Ctusers.Remove(_row);
		_repo.SaveChanges();
		return Result.Ok();
	}
    public Result Login(Login login)
    {
        var user = _repo.Ctusers.Where(x=>x.username==login.username).FirstOrDefault();
        if (user == null)
            return Result.Fail(new Exception("Username not found").Message);
        if (user.password != login.password)
            return Result.Fail(new Exception("Wrong password").Message);

        return Result.Ok();        
    }
    public async Task<Result<IEnumerable<CatUsersEntity>>> GetAllAsync()
	{
		IEnumerable<CatUsersEntity> rows = await _repo.Ctusers.ToListAsync();
		return Result.Ok(rows);
	}
	public async Task<Result<CatUsersEntity>> GetByIdAsync(int id)
	{
		var row = await _repo.Ctusers.FindAsync(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		return Result.Ok(row);
	}
	public async Task<Result<CatUsersEntity>> AddAsync(CatUsersEntity row)
	{
		await _repo.Ctusers.AddAsync(row);
		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public async Task<Result<CatUsersEntity>> UpdateAsync(int id, CatUsersEntity row)
	{
		if (id != row.id)
			return Result.Fail(new Exception("The primary key and the row don't match").Message);
		var _row = await _repo.Ctusers.FindAsync(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_row.name = row.name;
		_row.lastname = row.lastname;
		_row.username = row.username;
		_row.password = row.password;
		_row.email = row.email;
		_row.created_dt = row.created_dt;
		_row.status = row.status;

		await _repo.SaveChangesAsync();
		return Result.Ok(row);
	}
	public async Task<Result> DeleteAsync(int id)
	{
		var row = await _repo.Ctusers.FindAsync(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_repo.Ctusers.Remove(row);
		await _repo.SaveChangesAsync();
		return Result.Ok();
	}
    public async Task<Result<Token>> LoginAsync(Login login)
    {
        var user = await _repo.Ctusers.Where(x => x.username == login.username).FirstOrDefaultAsync();
        if (user == null)
            return Result.Fail(new Exception("Username not found").Message);

		if (user.status != "A")
            return Result.Fail(new Exception("Blocked user").Message);

        if (user.password != login.password)
		{
			var sessionLog = new SessionslogService();

			var sLog = new SessionslogEntity()
			{
				id = 0,
				userid = user.id,
				description = "Wrong password",
				event_dt = DateTime.Now,
				successerror = "E"
			};
            _repo.Sessionslog.Add(sLog);
            await _repo.SaveChangesAsync();
            var failedTries = await _repo.Sessionslog.Where(x => x.userid == user.id && x.successerror == "E").CountAsync();
			if (failedTries > 3) {
				user.status = "I";
                await _repo.SaveChangesAsync();
            }
            return Result.Fail(new Exception(sLog.description).Message);
		}
		var token = new Token() { 
			AccessToken = CommonHelper.GenerateJwtToken(login.username)
		};

        return Result.Ok(token);
    }
}
