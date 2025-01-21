using FluentResults;

namespace CatUsersServices;
public interface IServicePkStr<T>
{
	/// <summary>
	/// GetAll method
	/// Get all rows, synchronous method
	/// arguments:
	/// None
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>List&lt;T&gt;</returns>
	Result<IEnumerable<T>> GetAll();
	/// <summary>
	/// GetById method
	/// Find a row, synchronous method
	/// arguments:
	/// string id
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>T</returns>
	Result<T> GetById(string id);
	
	/// <summary>
	/// Add method
	/// Add a new row, synchronous method
	/// arguments:
	/// T arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>T</returns>
	Result<T> Add(T row);
	/// <summary>
	/// Update method
	/// Update a row, synchronous method
	/// arguments:
	/// string id
	/// T arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>T</returns>
	Result<T> Update(string id, T row);
	/// <summary>
	/// Delete method
	/// Delete a row, synchronous method
	/// arguments:
	/// string id
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>void</returns>
	Result Delete(string id);
	/// <summary>
	/// GetAllAsync method
	/// Get all rows, asynchronous method
	/// arguments:
	/// None
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;List&lt;T&gt;&gt;</returns>
	Task<Result<IEnumerable<T>>> GetAllAsync();
	/// <summary>
	/// GetByIdAsync method
	/// Find a row, asynchronous method
	/// arguments:
	/// string id
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;T&gt;</returns>
	Task<Result<T>> GetByIdAsync(string id);
	
	/// <summary>
	/// AddAsync method
	/// Add a new row, asynchronous method
	/// arguments:
	/// T arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;T&gt;</returns>
	Task<Result<T>> AddAsync(T row);
	/// <summary>
	/// UpdateAsync method
	/// Update a row, asynchronous method
	/// arguments:
	/// string id
	/// T arg
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task&lt;T&gt;</returns>
	Task<Result<T>> UpdateAsync(string id, T row);
	/// <summary>
	/// DeleteAsync method
	/// Delete a row, asynchronous method
	/// arguments:
	/// string id
	/// 
	/// Author: Alfredo Barrios
	/// Date: 2025-Jan-21 2:30:27 AM
	/// </summary>
	/// <returns>Task</returns>
	Task<Result> DeleteAsync(string id);
}
