using FluentResults;

namespace CatUsersApi.Errors;
internal class RecordNotFoundError : Error
{
	public RecordNotFoundError(string message) : base(message){}
}
