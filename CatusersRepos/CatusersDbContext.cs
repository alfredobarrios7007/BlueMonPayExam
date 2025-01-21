using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using CatUsersRepos.Configurations;
using CatUsersModels.Entities;

namespace CatUsersRepos;
/// <summary>
/// CatUsersDbContext class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:28 AM
/// </summary>
public class CatUsersDbContext : DbContext
{
	public CatUsersDbContext (DbContextOptions<CatUsersDbContext> options) : base(options) {}
	public CatUsersDbContext ()  {}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
		optionsBuilder.LogTo(message => Debug.WriteLine(message));

	public virtual DbSet<CatUsersEntity> Ctusers { get; set; }
	public virtual DbSet<SessionslogEntity> Sessionslog { get; set; }
	public virtual DbSet<TokensblacklistEntity> Tokensblacklist { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new CtUsersConfiguration());
		modelBuilder.ApplyConfiguration(new SessionslogConfiguration());
		modelBuilder.ApplyConfiguration(new TokensblacklistConfiguration());
	}
}
