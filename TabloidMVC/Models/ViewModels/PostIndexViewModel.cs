using System.Collections.Generic;
using System.Security.Claims;


namespace TabloidMVC.Models.ViewModels
{
    public class PostIndexViewModel
    {
        public Post Post { get; set; }
        public List<Post> GetAllPublishedPosts { get; set; }
        public List<UserProfile> GetAllUsers { get; set; }
    }
}
