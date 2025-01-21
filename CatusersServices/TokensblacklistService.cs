using FluentResults;
using Microsoft.EntityFrameworkCore;
using CatUsersModels.Entities;
using CatUsersRepos;

namespace CatUsersServices;
/// <summary>
/// TokensblacklistService class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
public class TokensblacklistService : IServicePkInt<TokensblacklistEntity>
{
	CatUsersDbContext _repo;
	public TokensblacklistService(){ }
	public TokensblacklistService(CatUsersDbContext context)
	{
		_repo = context;
	}
	public Result<IEnumerable<TokensblacklistEntity>> GetAll()
	{
		IEnumerable<TokensblacklistEntity> rows = _repo.Tokensblacklist.ToList();
		return Result.Ok(rows);
	}
	public Result<TokensblacklistEntity> GetById(int id)
	{
		var row = _repo.Tokensblacklist.Find(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		return Result.Ok(row);
	}
	public Result<TokensblacklistEntity> Add(TokensblacklistEntity row)
	{
		_repo.Tokensblacklist.Add(row);
		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public Result<TokensblacklistEntity> Update(int id, TokensblacklistEntity row)
	{
		if (id != row.id)
			return Result.Fail(new Exception("The primary key and the row don't match").Message);
		var _row = _repo.Tokensblacklist.Find(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_row.created_dt = row.created_dt;
		_row.token = row.token;

		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public Result Delete(int id)
	{
		var _row = _repo.Tokensblacklist.Find(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_repo.Tokensblacklist.Remove(_row);
		_repo.SaveChanges();
		return Result.Ok();
	}
	public async Task<Result<IEnumerable<TokensblacklistEntity>>> GetAllAsync()
	{
		IEnumerable<TokensblacklistEntity> rows = await _repo.Tokensblacklist.ToListAsync();
		return Result.Ok(rows);
	}
	public async Task<Result<TokensblacklistEntity>> GetByIdAsync(int id)
	{
		var row = await _repo.Tokensblacklist.FindAsync(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		return Result.Ok(row);
	}
	public async Task<Result<TokensblacklistEntity>> AddAsync(TokensblacklistEntity row)
	{
		await _repo.Tokensblacklist.AddAsync(row);
		_repo.SaveChanges();
		return Result.Ok(row);
	}
	public async Task<Result<TokensblacklistEntity>> UpdateAsync(int id, TokensblacklistEntity row)
	{
		if (id != row.id)
			return Result.Fail(new Exception("The primary key and the row don't match").Message);
		var _row = await _repo.Tokensblacklist.FindAsync(id);
		if (_row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_row.created_dt = row.created_dt;
		_row.token = row.token;

		await _repo.SaveChangesAsync();
		return Result.Ok(row);
	}
	public async Task<Result> DeleteAsync(int id)
	{
		var row = await _repo.Tokensblacklist.FindAsync(id);
		if (row == null)
			return Result.Fail(new Exception("The row with the id {id} not found".Replace("{id}",id.ToString())).Message);
		
		_repo.Tokensblacklist.Remove(row);
		await _repo.SaveChangesAsync();
		return Result.Ok();
	}
}
