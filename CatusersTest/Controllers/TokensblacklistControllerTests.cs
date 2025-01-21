using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using CatUsersApi.Controllers;
using CatUsersServices;
using CatUsersServices.DTOs;
using CatUsersDao;
using CatUsersTests.Helpers;

namespace CatUsersTests.Controllers;
public class TokensblacklistControllerTest
{
	private readonly Mock<CatUsersDbContext> _mockContext;
	private readonly Mock<DbSet<Tokensblacklist>> _mockDbSet;
	private readonly TokensblacklistController _controller;

	public TokensblacklistControllerTest()
	{
		_mockContext = new Mock<CatUsersDbContext>();
		_mockDbSet = new Mock<DbSet<Tokensblacklist>>();
		_controller = new TokensblacklistController(_mockContext.Object);

	}

	[Fact]
	public async Task TokensblacklistController_GetAll_ResturnListTokensblacklist()
	{
		// Arrange
		var lRows = TestDataHelper.GetFakeTokensblacklistList();
		
		_mockDbSet.As<IQueryable<Tokensblacklist>>().Setup(m => m.Provider).Returns(lRows.Provider);
		_mockDbSet.As<IQueryable<Tokensblacklist>>().Setup(m => m.Expression).Returns(lRows.Expression);
		_mockDbSet.As<IQueryable<Tokensblacklist>>().Setup(m => m.ElementType).Returns(lRows.ElementType);
		_mockDbSet.As<IQueryable<Tokensblacklist>>().Setup(m => m.GetEnumerator()).Returns(lRows.GetEnumerator());
		
		_mockContext.Setup(c => c.Tokensblacklist).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.GetAll();
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<IEnumerable<Tokensblacklist>>>(result);
		var model = Assert.IsAssignableFrom<IEnumerable<Tokensblacklist>>(actionResult.Value);
		Assert.Equal(1, model.Count());
		
	}

	[Fact]
	public async Task TokensblacklistController_Get_ResturnTokensblacklist()
	{
		// Arrange
			var row = new Tokensblacklist { 
			id = 1,
			created_dt = DateTime.Now,
			token = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
		};
		_mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(row);
		_mockContext.Setup(c => c.Tokensblacklist).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Get(1);
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<Tokensblacklist>>(result);
		var model = Assert.IsType<Tokensblacklist>(actionResult.Value);
		Assert.Equal(row.id, model.id);
		
	}

	[Fact]
	public async Task Create()
	{
		
		// Arrange
		var row = new TokensblacklistEntity() { 
			id = 1,
			created_dt = DateTime.Now,
			token = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
		};
		_mockContext.Setup(c => c.Tokensblacklist).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Create(row);
		
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<Tokensblacklist>>(result);
		var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
		Assert.Equal(nameof(TokensblacklistController.Get), createdAtActionResult.ActionName);
		
	}

	[Fact]
	public async Task Update()
	{
		
		// Arrange
		var _id = 1;
		var row = new TokensblacklistEntity() { 
			id = 1,
			created_dt = DateTime.Now,
			token = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
		};
		_mockContext.Setup(c => c.Tokensblacklist).Returns(_mockDbSet.Object);
		
		
		// Act
		var result = await _controller.Update(_id, row);
		
		
		// Assert
		Assert.IsType<NoContentResult>(result);
		
	}

	[Fact]
	public async Task Delete()
	{
		
		// Arrange
		_mockDbSet.Setup(m => m.FindAsync(2)).ReturnsAsync((Tokensblacklist)null);
		_mockContext.Setup(c => c.Tokensblacklist).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Delete(2);
		
		// Assert
		Assert.IsType<NotFoundResult>(result);
		
	}

}
