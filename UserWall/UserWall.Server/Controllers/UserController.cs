using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using UserWall.Dto;

namespace UserWall.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IDbContextFactory<UserWallContext> _contextFactory;
    private readonly IMapper _mapper;

    public UserController(IDbContextFactory<UserWallContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a list of all users.
    /// </summary>
    /// <returns>The list of all users.</returns>
    [HttpGet]
    public IEnumerable<UserDto> Get()
    {
        using var ctx = _contextFactory.CreateDbContext();
        return _mapper.Map<IEnumerable<UserDto>>(ctx.Users);
    }

    /// <summary>
    /// Returns a user by id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <returns>OK (the user found by the specified id) or NotFound.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserDto> Get(int id)
    {
        using var ctx = _contextFactory.CreateDbContext();
        var user = ctx.Find<User>(id);
        if (user is null)
            return NotFound();
        return _mapper.Map<UserDto>(user);
    }
}
