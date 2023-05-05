using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [HttpGet(Name = "GetUsers")]
    public async Task<IEnumerable<UserDto>> Get()
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();

        var users = await ctx.Users.ToArrayAsync();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    /// <summary>
    /// Returns a user by id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <returns>Ok or NotFound.</returns>
    [HttpGet("{id:int}", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();

        var user = await ctx.FindAsync<User>(id);
        if (user is null)
            return NotFound();

        return _mapper.Map<UserDto>(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">New user data.</param>
    /// <returns>The ID assigned to the new user.</returns>
    [HttpPost(Name = "CreateUser")]
    public async Task<int> Post(UserPostDto user)
    {
        var userEntity = _mapper.Map<User>(user);

        await using (var ctx = await _contextFactory.CreateDbContextAsync())
        {
            ctx.Add(userEntity);
            await ctx.SaveChangesAsync();
        }

        return userEntity.Id;
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">New user data.</param>
    /// <returns>Ok or NotFound.</returns>
    [HttpPut(Name = "UpdateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(UserDto user)
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();

        var userEntity = await ctx.FindAsync<User>(user.Id);
        if (userEntity is null)
            return NotFound();

        _mapper.Map(user, userEntity);

        await ctx.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Deletes a user by id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <returns>Ok or NotFound.</returns>
    [HttpDelete("{id:int}", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();

        var user = await ctx.FindAsync<User>(id);
        if (user is null)
            return NotFound();

        ctx.Remove(user);

        await ctx.SaveChangesAsync();

        return Ok();
    }
}
