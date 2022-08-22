using AcmeWebAPI.Models;

namespace AcmeWebAPI.DataRepository
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPost>> GetAllBlogPosts();
        Task<BlogPost> GetPostByIDAsync(int blogPostId);
        Task<int> AddPostAsync(BlogPost newPost);
        Task<int> EditPostAsync(BlogPost editPost);
        Task<int> DeletePostAsync(int id);
    }
}
