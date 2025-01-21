
namespace CatUsersModels.Entities;
/// <summary>
/// Sessionslog class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
public class SessionslogEntity
{
	public int id { get; set; }

	public DateTime event_dt { get; set; }

	public int userid { get; set; }

	public string description { get; set; }

	public string successerror { get; set; }

}
