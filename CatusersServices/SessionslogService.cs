using FluentResults;
using Microsoft.EntityFrameworkCore;
using CatUsersModels.Entities;
using CatUsersRepos;

namespace CatUsersServices;
/// <summary>
/// SessionslogService class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
public class SessionslogService : IServicePkInt<SessionslogEntity>
{
	CatUsersDbContext _repo;
	public SessionslogService(){ }
	public SessionslogService(CatUsersDbContext context)
	{
		_repo = context;
	}
	public Result<IEnumerable<SessionslogEntity>> GetAll()
	{
		IEnumerable<SessionslogEntity> rows = _repo.Sessionslog.ToList();
		return Result.Ok(rows);
	}
	public Result<SessionslogEntity> GetById(int id)
	{
		var row = _repo.Sessionslog.Find(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		return Result.Ok(row);
	}
	public Result<SessionslogEntity> Add(SessionslogEntity row)
	{
		_repo.Sessionslog.Add(row);
		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public Result<SessionslogEntity> Update(int id, SessionslogEntity row)
	{
		if (id != row.id)
			return Result.Fail(new Exception("The primary key and the row don't match").Message);
		var _row = _repo.Sessionslog.Find(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_row.event_dt = row.event_dt;
		_row.userid = row.userid;
		_row.description = row.description;
		_row.successerror = row.successerror;

		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public Result Delete(int id)
	{
		var _row = _repo.Sessionslog.Find(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_repo.Sessionslog.Remove(_row);
		_repo.SaveChanges();
		return Result.Ok();
	}
	public async Task<Result<IEnumerable<SessionslogEntity>>> GetAllAsync()
	{
		IEnumerable<SessionslogEntity> rows = await _repo.Sessionslog.ToListAsync();
		return Result.Ok(rows);
	}
	public async Task<Result<SessionslogEntity>> GetByIdAsync(int id)
	{
		var row = await _repo.Sessionslog.FindAsync(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		return Result.Ok(row);
	}
	public async Task<Result<SessionslogEntity>> AddAsync(SessionslogEntity row)
	{
		await _repo.Sessionslog.AddAsync(row);
		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public async Task<Result<SessionslogEntity>> UpdateAsync(int id, SessionslogEntity row)
	{
		if (id != row.id)
			return Result.Fail(new Exception("The primary key and the row don't match").Message);
		var _row = await _repo.Sessionslog.FindAsync(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_row.event_dt = row.event_dt;
		_row.userid = row.userid;
		_row.description = row.description;
		_row.successerror = row.successerror;

		await _repo.SaveChangesAsync();
		return Result.Ok(row);
	}
	public async Task<Result> DeleteAsync(int id)
	{
		var row = await _repo.Sessionslog.FindAsync(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_repo.Sessionslog.Remove(row);
		await _repo.SaveChangesAsync();
		return Result.Ok();
	}
}
