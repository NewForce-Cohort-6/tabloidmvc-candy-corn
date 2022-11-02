using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Repositories;
using TabloidMVC.Controllers;


namespace TabloidMVC.Models.ViewModels
{
    public class PostIndexViewModel
    {
        public Post Post { get; set; }
        public List<Post> GetPosts { get; set; }
        public List<UserProfile> GetUsers { get; set; }
    }
}
