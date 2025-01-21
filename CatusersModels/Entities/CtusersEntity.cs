
namespace CatUsersModels.Entities;
/// <summary>
/// Ctusers class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
public class CatUsersEntity
{
	public int id { get; set; }

	public string name { get; set; }

	public string lastname { get; set; }

	public string username { get; set; }

	public string password { get; set; }

	public string email { get; set; }

	public DateTime created_dt { get; set; }

	public string status { get; set; }

}
