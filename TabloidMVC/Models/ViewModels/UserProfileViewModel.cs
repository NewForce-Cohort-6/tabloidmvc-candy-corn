using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<UserProfile> UserProfiles { get; set; } 
        public List<UserType> UserTypes { get; set; }
    }
}
