using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void DeletePost(int id);
        List<Post> GetAllPublishedPosts();
        List<Post> GetUserPosts(int id);
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
    }
}