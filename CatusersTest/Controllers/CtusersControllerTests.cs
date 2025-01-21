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
public class CtusersControllerTest
{
	private readonly Mock<CatUsersDbContext> _mockContext;
	private readonly Mock<DbSet<Ctusers>> _mockDbSet;
	private readonly CtusersController _controller;

	public CtusersControllerTest()
	{
		_mockContext = new Mock<CatUsersDbContext>();
		_mockDbSet = new Mock<DbSet<Ctusers>>();
		_controller = new CtusersController(_mockContext.Object);

	}

	[Fact]
	public async Task CtusersController_GetAll_ResturnListCtusers()
	{
		// Arrange
		var lRows = TestDataHelper.GetFakeCtusersList();
		
		_mockDbSet.As<IQueryable<Ctusers>>().Setup(m => m.Provider).Returns(lRows.Provider);
		_mockDbSet.As<IQueryable<Ctusers>>().Setup(m => m.Expression).Returns(lRows.Expression);
		_mockDbSet.As<IQueryable<Ctusers>>().Setup(m => m.ElementType).Returns(lRows.ElementType);
		_mockDbSet.As<IQueryable<Ctusers>>().Setup(m => m.GetEnumerator()).Returns(lRows.GetEnumerator());
		
		_mockContext.Setup(c => c.Ctusers).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.GetAll();
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<IEnumerable<Ctusers>>>(result);
		var model = Assert.IsAssignableFrom<IEnumerable<Ctusers>>(actionResult.Value);
		Assert.Equal(1, model.Count());
		
	}

	[Fact]
	public async Task CtusersController_Get_ResturnCtusers()
	{
		// Arrange
			var row = new Ctusers { 
			id = 1,
			name = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			lastname = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			username = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			password = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			email = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			created_dt = DateTime.Now,
			status = "x",
		};
		_mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(row);
		_mockContext.Setup(c => c.Ctusers).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Get(1);
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<Ctusers>>(result);
		var model = Assert.IsType<Ctusers>(actionResult.Value);
		Assert.Equal(row.id, model.id);
		
	}

	[Fact]
	public async Task Create()
	{
		
		// Arrange
		var row = new CatUsersEntity() { 
			id = 1,
			name = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			lastname = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			username = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			password = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			email = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			created_dt = DateTime.Now,
			status = "x",
		};
		_mockContext.Setup(c => c.Ctusers).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Create(row);
		
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<Ctusers>>(result);
		var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
		Assert.Equal(nameof(CtusersController.Get), createdAtActionResult.ActionName);
		
	}

	[Fact]
	public async Task Update()
	{
		
		// Arrange
		var _id = 1;
		var row = new CatUsersEntity() { 
			id = 1,
			name = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			lastname = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			username = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			password = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			email = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			created_dt = DateTime.Now,
			status = "x",
		};
		_mockContext.Setup(c => c.Ctusers).Returns(_mockDbSet.Object);
		
		
		// Act
		var result = await _controller.Update(_id, row);
		
		
		// Assert
		Assert.IsType<NoContentResult>(result);
		
	}

	[Fact]
	public async Task Delete()
	{
		
		// Arrange
		_mockDbSet.Setup(m => m.FindAsync(2)).ReturnsAsync((Ctusers)null);
		_mockContext.Setup(c => c.Ctusers).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Delete(2);
		
		// Assert
		Assert.IsType<NotFoundResult>(result);
		
	}

}
