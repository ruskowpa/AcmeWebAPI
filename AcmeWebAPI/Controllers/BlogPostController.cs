using AcmeWebAPI.DataRepository;
using AcmeWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AcmeWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly BlogPostRepository _repo;

        public BlogPostController(IBlogPostRepository repo)
        {
            _repo = (BlogPostRepository?)repo;
        }

        /// <summary>
        /// Get a blog post by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Blog Post Object.</returns>
        [HttpGet]
        [Route("GetBlogPostByID/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogPost))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBlogPostByIDAsync(int id)
        {
            if (id == 0)
            {
                return NoContent();
            }

            BlogPost response = await _repo.GetPostByIDAsync(id);

            if (response == null || response?.Id == 0)
            {
                return NoContent();
            }

            return Ok(response);
        }

        /// <summary>
        /// Get all existing blog posts.
        /// </summary>
        /// <returns>List of BlogPostObjects</returns>
        [HttpGet]
        [Route("GetAllBlogPosts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BlogPost>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetBlogPostAsync()
        {
            List<BlogPost> response = await _repo.GetAllBlogPosts();

            if (response == null || response.Count == 0)
            {
                return NoContent();
            }

            return Ok(response);
        }

        /// <summary>
        /// Create new Blog Post
        /// </summary>
        /// <param name="newBlogPost">New Blog Post Object</param>
        /// <returns>IActionResult Success/fail</returns>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBlogPostAsync([FromBody] BlogPost newBlogPost)
        {

            if (string.IsNullOrWhiteSpace(newBlogPost.Title) || string.IsNullOrWhiteSpace(newBlogPost.Content))
            {
                return BadRequest();
            }
            try
            {
                int response = await _repo.AddPostAsync(newBlogPost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }


            return Ok();
        }

        /// <summary>
        /// Edit an existing Blog Post
        /// </summary>
        /// <param name="editBlogPost">incoming existing blog post object</param>
        /// <returns>IActionResult Success/fail</returns>
        [HttpPut]
        [Route("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditBlogPostAsync([FromBody] BlogPost editBlogPost)
        {

            if (string.IsNullOrWhiteSpace(editBlogPost.Title) || string.IsNullOrWhiteSpace(editBlogPost.Content))
            {
                return BadRequest();
            }
            try
            {
                int response = await _repo.EditPostAsync(editBlogPost);

                if (response < 1)
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }

            return Ok();
        }

        /// <summary>
        /// Delete existing BlogPost Object
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult Success/fail.</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBlogPostAsync(int id)
        {

            if (id == 0)
            {
                return NoContent();
            }

            try
            {
                int response = await _repo.DeletePostAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }


            return Ok();
        }
    }
}