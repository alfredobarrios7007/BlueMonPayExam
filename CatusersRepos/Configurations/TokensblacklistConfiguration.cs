using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CatUsersModels.Entities;

namespace CatUsersRepos.Configurations;
/// <summary>
/// TokensblacklistConfiguration class
/// 
/// 
/// Author: Alfredo Barrios
/// Date: 2025-Jan-21 2:30:27 AM
/// </summary>
public class TokensblacklistConfiguration : IEntityTypeConfiguration<TokensblacklistEntity>
{
	public void Configure(EntityTypeBuilder<TokensblacklistEntity> builder)
	{
		builder.ToTable("Tokensblacklist");
		builder.HasKey(c => c.id);
		builder.Property(c => c.id)
			.IsRequired();
		builder.Property(c => c.created_dt)
			.IsRequired();
		builder.Property(c => c.token)
			.IsRequired()
			.HasMaxLength(250);
	}
}
