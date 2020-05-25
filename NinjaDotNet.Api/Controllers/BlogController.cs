using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NinjaDotNet.Api.Contracts;
using NinjaDotNet.Api.Data.Models;
using NinjaDotNet.Api.DTOs;

namespace NinjaDotNet.Api.Controllers
{
    /// <summary>
    /// API For Blog Actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepo;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogController(IBlogRepository blogRepo, ILoggerService logger, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _blogRepo = blogRepo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        /// <summary>
        /// Returns All Blogs For the Current User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {
                var results = await _blogRepo.FindAll();
                var response = _mapper.Map<IList<BlogDTO>>(results);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }
        /// <summary>
        /// Returns A Single Blog By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSingleBlog(int id)
        {
            try
            {
                var result = await _blogRepo.FindById(id);
                if (result == null)
                {
                    return NotFound("A Blog was not found by that id");
                }
                var response = _mapper.Map<BlogDTO>(result);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Creates a New Blog Record
        /// </summary>
        /// <param name="blog">BlogCreateDTO</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] BlogCreateDTO blog)
        {
            try
            {
                if (blog == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var b = _mapper.Map<Blog>(blog);
                b.CreatedDate = DateTime.Now;

              


                ClaimsPrincipal currentUser = this.User;
                var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                IdentityUser user = await _userManager.FindByNameAsync(currentUserName);

                b.Author = Guid.Parse(user.Id);

                var result = await _blogRepo.Create(b);
                return !result ? StatusCode(500, "Failed To Save") : Created("Successfully Created", new { Blog = b });
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Updates Blog Record
        /// </summary>
        /// <param name="id">Id of The Blog</param>
        /// <param name="blog">BlogCreateDTO</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id,[FromBody] BlogUpdateDTO blog)
        {
            try
            {
                if (blog == null || blog.BlogId != id || id < 1)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!await _blogRepo.Exists(id))
                {
                    return BadRequest();
                }

                var b = _mapper.Map<Blog>(blog);
                var result = await _blogRepo.Update(b);
                return !result ? StatusCode(500, "Failed To Save") : Ok(b);
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Updates Blog Record
        /// </summary>
        /// <param name="id">Id of The Blog</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest(ModelState);

                var blog = await _blogRepo.FindById(id);

                if (blog == null)
                {
                    return NotFound();
                }

                var isSuccess = await _blogRepo.Delete(blog);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }


    }
}
