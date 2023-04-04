using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserWall.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly Context _context;

    public AnalyticsController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns average post count per user.
    /// </summary>
    /// <returns>Average post count per user.</returns>
    [HttpGet("average_post_count")]
    public float GetAveragePostCount()
    {
        return (float)_context.Posts
            .GroupBy(post => post.UserId)
            .Select(userPosts => userPosts.Count())
            .DefaultIfEmpty()
            .Average();
    }

    /// <summary>
    /// Returns user post count in given year.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="year">The year.</param>
    /// <returns>OK (user post count in given year) or NotFound.</returns>
    [HttpGet("user_post_count_in_given_year")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<int> UserPostCountInGivenYear(int userId, int year)
    {
        if (!_context.Users.Any(user => user.Id == userId))
            return NotFound();

        var count = _context.Posts.Where(post => post.UserId == userId && post.PublicationTime.Year == year).Count();
        return Ok(count);
    }
}
