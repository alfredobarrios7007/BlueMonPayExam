using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CatUsersModels.Entities;

namespace CatUsersRepos.Configurations;
/// <summary>
/// CtusersConfiguration class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
public class CtUsersConfiguration : IEntityTypeConfiguration<CatUsersEntity>
{
	public void Configure(EntityTypeBuilder<CatUsersEntity> builder)
	{
		builder.ToTable("CtUsers");
		builder.HasKey(c => c.id);
		builder.Property(c => c.id)
			.IsRequired();
		builder.Property(c => c.name)
			.IsRequired()
			.HasMaxLength(250);
		builder.Property(c => c.lastname)
			.IsRequired()
			.HasMaxLength(250);
		builder.Property(c => c.username)
			.IsRequired()
			.HasMaxLength(50);
		builder.Property(c => c.password)
			.IsRequired()
			.HasMaxLength(250);
		builder.Property(c => c.email)
			.HasMaxLength(250);
		builder.Property(c => c.created_dt)
			.IsRequired();
		builder.Property(c => c.status)
			.IsRequired()
			.HasMaxLength(1);
	}
}
