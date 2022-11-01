using System.Collections.Generic;
using System.Security.Claims;


namespace TabloidMVC.Models.ViewModels
{
    public class PostIndexViewModel
    {
        public List<Post> GetAllPublishedPosts { get;}
        public List<UserProfile> GetAllUsers { get;}
    }
}
