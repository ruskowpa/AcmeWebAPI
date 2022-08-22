using AcmeWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeWebAPI.DataRepository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly DatabaseContext _dbContext;
        public BlogPostRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> GetPostByIDAsync(int blogPostId)
        {
            BlogPost retVal = new BlogPost();

            if (blogPostId == 0)
            {
                return retVal;
            }

            retVal = await _dbContext.BlogPosts.Where(x => x.Id == blogPostId)?.FirstOrDefaultAsync() ?? new BlogPost();

            return retVal;
        }

        public async Task<int> AddPostAsync(BlogPost newPost)
        {
            _dbContext.BlogPosts.Add(newPost);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> EditPostAsync(BlogPost editPost)
        {
            if (editPost.Id == 0)
            {
                return 0;
            }

            BlogPost postToEdit = await _dbContext.BlogPosts.Where(x => x.Id == editPost.Id)?.FirstOrDefaultAsync() ?? new BlogPost();

            if (postToEdit == null)
            {
                return 0;
            }

            postToEdit.Title = editPost.Title;
            postToEdit.Content = editPost.Content;

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BlogPost>> GetAllBlogPosts()
        {
            return await _dbContext.BlogPosts.ToListAsync() ?? new List<BlogPost>();
        }

        public async Task<int> DeletePostAsync(int id)
        {
            BlogPost? postToDelete = await GetPostByIDAsync(id);

            if (postToDelete != null)
            {
                _dbContext.BlogPosts.Remove(postToDelete);
                return await _dbContext.SaveChangesAsync();
            }

            return -1;
        }
    }
}
