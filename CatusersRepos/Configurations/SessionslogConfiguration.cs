using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CatUsersModels.Entities;

namespace CatUsersRepos.Configurations;
/// <summary>
/// SessionslogConfiguration class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
public class SessionslogConfiguration : IEntityTypeConfiguration<SessionslogEntity>
{
	public void Configure(EntityTypeBuilder<SessionslogEntity> builder)
	{
		builder.ToTable("Sessionslog");
		builder.HasKey(c => c.id);
		builder.Property(c => c.id)
			.IsRequired();
		builder.Property(c => c.event_dt)
			.IsRequired();
		builder.Property(c => c.userid)
			.IsRequired();
		builder.Property(c => c.description)
			.IsRequired()
			.HasMaxLength(250);
		builder.Property(c => c.successerror)
			.IsRequired()
			.HasMaxLength(1);
	}
}
