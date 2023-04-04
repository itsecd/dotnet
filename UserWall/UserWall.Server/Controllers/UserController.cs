using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using UserWall.Dto;

namespace UserWall.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public UserController(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a list of all users.
    /// </summary>
    /// <returns>The list of all users.</returns>
    [HttpGet]
    public IEnumerable<UserDto> Get()
    {
        return _mapper.Map<IEnumerable<UserDto>>(_context.Users);
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
        var user = _context.Users
            .Where(user => user.Id == id)
            .SingleOrDefault();

        if (user is null)
            return NotFound();

        var userDto = _mapper.Map<UserDto>(user);

        return Ok(userDto);
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="userPostDto">New user.</param>
    /// <returns>New user id.</returns>
    [HttpPost]
    public int Post(UserPostDto userPostDto)
    {
        var user = _mapper.Map<User>(userPostDto);

        user.Id = _context.Users
            .Select(user => user.Id)
            .DefaultIfEmpty()
            .Max() + 1;

        _context.Users.Add(user);

        return user.Id;
    }

    /// <summary>
    /// Updates the existing user data.
    /// </summary>
    /// <param name="userDto">New user data.</param>
    /// <returns>OK or NotFound.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(UserDto userDto)
    {
        var user = _context.Users
            .Where(user => user.Id == userDto.Id)
            .SingleOrDefault();

        if (user is null)
            return NotFound();

        _mapper.Map(userDto, user);

        return Ok();
    }

    /// <summary>
    /// Deletes a user by id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <returns>OK or NotFound.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var user = _context.Users
            .Where(user => user.Id == id)
            .SingleOrDefault();

        if (user is null)
            return NotFound();

        _context.Posts.RemoveAll(post => post.UserId == id);
        _context.Users.Remove(user);

        return Ok();
    }
}
