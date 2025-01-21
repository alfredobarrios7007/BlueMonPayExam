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
public class SessionslogControllerTest
{
	private readonly Mock<CatUsersDbContext> _mockContext;
	private readonly Mock<DbSet<Sessionslog>> _mockDbSet;
	private readonly SessionslogController _controller;

	public SessionslogControllerTest()
	{
		_mockContext = new Mock<CatUsersDbContext>();
		_mockDbSet = new Mock<DbSet<Sessionslog>>();
		_controller = new SessionslogController(_mockContext.Object);

	}

	[Fact]
	public async Task SessionslogController_GetAll_ResturnListSessionslog()
	{
		// Arrange
		var lRows = TestDataHelper.GetFakeSessionslogList();
		
		_mockDbSet.As<IQueryable<Sessionslog>>().Setup(m => m.Provider).Returns(lRows.Provider);
		_mockDbSet.As<IQueryable<Sessionslog>>().Setup(m => m.Expression).Returns(lRows.Expression);
		_mockDbSet.As<IQueryable<Sessionslog>>().Setup(m => m.ElementType).Returns(lRows.ElementType);
		_mockDbSet.As<IQueryable<Sessionslog>>().Setup(m => m.GetEnumerator()).Returns(lRows.GetEnumerator());
		
		_mockContext.Setup(c => c.Sessionslog).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.GetAll();
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<IEnumerable<Sessionslog>>>(result);
		var model = Assert.IsAssignableFrom<IEnumerable<Sessionslog>>(actionResult.Value);
		Assert.Equal(1, model.Count());
		
	}

	[Fact]
	public async Task SessionslogController_Get_ResturnSessionslog()
	{
		// Arrange
			var row = new Sessionslog { 
			id = 1,
			event_dt = DateTime.Now,
			userid = 1,
			description = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			successerror = "x",
		};
		_mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(row);
		_mockContext.Setup(c => c.Sessionslog).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Get(1);
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<Sessionslog>>(result);
		var model = Assert.IsType<Sessionslog>(actionResult.Value);
		Assert.Equal(row.id, model.id);
		
	}

	[Fact]
	public async Task Create()
	{
		
		// Arrange
		var row = new SessionslogEntity() { 
			id = 1,
			event_dt = DateTime.Now,
			userid = 1,
			description = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			successerror = "x",
		};
		_mockContext.Setup(c => c.Sessionslog).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Create(row);
		
		
		// Assert
		var actionResult = Assert.IsType<ActionResult<Sessionslog>>(result);
		var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
		Assert.Equal(nameof(SessionslogController.Get), createdAtActionResult.ActionName);
		
	}

	[Fact]
	public async Task Update()
	{
		
		// Arrange
		var _id = 1;
		var row = new SessionslogEntity() { 
			id = 1,
			event_dt = DateTime.Now,
			userid = 1,
			description = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
			successerror = "x",
		};
		_mockContext.Setup(c => c.Sessionslog).Returns(_mockDbSet.Object);
		
		
		// Act
		var result = await _controller.Update(_id, row);
		
		
		// Assert
		Assert.IsType<NoContentResult>(result);
		
	}

	[Fact]
	public async Task Delete()
	{
		
		// Arrange
		_mockDbSet.Setup(m => m.FindAsync(2)).ReturnsAsync((Sessionslog)null);
		_mockContext.Setup(c => c.Sessionslog).Returns(_mockDbSet.Object);
		
		// Act
		var result = await _controller.Delete(2);
		
		// Assert
		Assert.IsType<NotFoundResult>(result);
		
	}

}
