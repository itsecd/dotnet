using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using UserWall.Dto;

namespace UserWall.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public PostController(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns a list of all posts.
    /// </summary>
    /// <returns>The list of all posts.</returns>
    [HttpGet]
    public IEnumerable<PostDto> Get()
    {
        return _mapper.Map<IEnumerable<PostDto>>(_context.Posts);
    }

    /// <summary>
    /// Returns a post by id.
    /// </summary>
    /// <param name="id">The post id.</param>
    /// <returns>OK (the post found by the specified id) or NotFound.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PostDto> Get(int id)
    {
        var post = _context.Posts
            .Where(post => post.Id == id)
            .SingleOrDefault();

        if (post is null)
            return NotFound();

        var postDto = _mapper.Map<PostDto>(post);

        return Ok(postDto);
    }

    /// <summary>
    /// Create a new post.
    /// </summary>
    /// <param name="postPostDto">New post.</param>
    /// <returns>OK (new user id) or BadRequest (invalid user id).</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<int> Post(PostPostDto postPostDto)
    {
        var post = _mapper.Map<Post>(postPostDto);

        if (!_context.Users.Any(user => user.Id == post.UserId))
            return BadRequest("Invalid user id.");

        post.Id = _context.Posts
            .Select(post => post.Id)
            .DefaultIfEmpty()
            .Max() + 1;

        _context.Posts.Add(post);

        return Ok(post.Id);
    }

    /// <summary>
    /// Updates the existing post data.
    /// </summary>
    /// <param name="postDto">New post data.</param>
    /// <returns>OK, BadRequest (invalid user id) or NotFound (invalid post id).</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(PostDto postDto)
    {
        var post = _context.Posts
            .Where(post => post.Id == postDto.Id)
            .SingleOrDefault();

        if (post is null)
            return NotFound();

        if (!_context.Users.Any(user => user.Id == post.UserId))
            return BadRequest("Invalid user id.");

        _mapper.Map(postDto, post);

        return Ok();
    }

    /// <summary>
    /// Deletes a post by id.
    /// </summary>
    /// <param name="id">The post id.</param>
    /// <returns>OK or NotFound.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var post = _context.Posts
            .Where(post => post.Id == id)
            .SingleOrDefault();

        if (post is null)
            return NotFound();

        _context.Posts.Remove(post);

        return Ok();
    }
}
